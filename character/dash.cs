using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class dash : ability
    {
        [SerializeField] protected float dashforce;
        [SerializeField] protected float dashcooldowntime;
        [SerializeField] protected float dashamounttime;
        [SerializeField] protected LayerMask dashinglayer;
        [SerializeField] protected LayerMask enemydashinglayer;
        //audio
        [SerializeField] protected AudioSource nakku;
        private bool candash;
        private float dashcountdown;
        private CapsuleCollider2D capsulecollider;

        private Vector2 deltaposition;

        

        // Start is called before the first frame update

        // Update is called once per frame
        protected override void initialization()
        {
            base.initialization();
            capsulecollider = GetComponent<CapsuleCollider2D>();
        }
        protected virtual void Update()
        {

            //dashpress();
            dashing();
        }
        //protected virtual bool dashpress()
        //{
        //    if (Input.GetKeyDown(KeyCode.Z) && candash)
        //    {
        //        dashing();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        protected virtual void dashing()
        {
            
            if (input.dashpress() && candash && !character.iscrouching && !Gamemanager.gamepause)
            {
                //grabbinghook.enabled = false;//edith
                deltaposition = transform.position;
                dashcountdown = dashcooldowntime;
                character.isdashing = true;
                capsulecollider.direction = CapsuleDirection2D.Horizontal;
                capsulecollider.size = new Vector2(capsulecollider.size.y, capsulecollider.size.x);
                anim.SetBool("dashing", true);
                //audio
                nakku.Play();
                StartCoroutine(finishdash());
            }
        }
        protected virtual void FixedUpdate()
        {
            resetdashcount();
            dashmode();
        }
        protected virtual void dashmode()
        {
            if (character.isdashing)
            {

                fallspeed(0);
                movement.enabled = false;
                if (!character.isfaceleft)
                {
                    dashcollison(Vector2.right, .5f, dashinglayer,enemydashinglayer);
                    rb.AddForce(Vector2.right * dashforce);
                }
                else
                {
                    dashcollison(Vector2.left, .5f, dashinglayer,enemydashinglayer);
                    rb.AddForce(Vector2.left * dashforce);
                }
            }
        }
        protected virtual void dashcollison(Vector2 direction, float distance, LayerMask collison,LayerMask enemycollision)
        {
            RaycastHit2D[] rayhits = new RaycastHit2D[10];
            int numhit = col.Cast(direction, rayhits, distance);

            for (int i = 0; i < numhit; i++)
            {
                if ((1 << rayhits[i].collider.gameObject.layer & collison) != 0)
                {

                    rayhits[i].collider.enabled = false;
                    StartCoroutine(turncolliderbackon(rayhits[i].collider.gameObject));
                }
                if ((1 << rayhits[i].collider.gameObject.layer & enemycollision) != 0)
                {

                    col.isTrigger = true;
                }

            }        
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<EnemeyCharacter>())
            {
                col.isTrigger = false;
            }
            if (collision.gameObject.GetComponent<beanenemy>())
            {
                col.isTrigger = false;
            }
        }
        protected virtual void resetdashcount()
        {
            if (dashcountdown > 0)
            {
                candash = false;
                dashcountdown -= Time.deltaTime;
                //mobile input
                input.dashmo = false;
            }
            else
            {
                candash = true;
            }
        }
        protected virtual IEnumerator finishdash()
        {
            yield return new WaitForSeconds(dashamounttime);
            capsulecollider.direction = CapsuleDirection2D.Vertical;
            capsulecollider.size = new Vector2(capsulecollider.size.y, capsulecollider.size.x);
            anim.SetBool("dashing", false);
            character.isdashing = false;
            fallspeed(1);
           
            movement.enabled = true;
            //grabbinghook.enabled = true;//edith
            rb.velocity = new Vector2(0, rb.velocity.y);
            RaycastHit2D[] hits = new RaycastHit2D[10];
            yield return new WaitForSeconds(.1f);
            hits = Physics2D.CapsuleCastAll(new Vector2(col.bounds.center.x, col.bounds.center.y + .05f), new Vector2(col.bounds.size.x, col.bounds.size.y -.1f), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, jumpscript.ground);
            if (hits.Length > 0)
            {
                transform.position = deltaposition;
            }
        }
        protected virtual IEnumerator turncolliderbackon(GameObject obj)
        {
            yield return new WaitForSeconds(dashamounttime);
            obj.GetComponent<Collider2D>().enabled = true;
        }
       
    }
}
