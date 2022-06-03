using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class grabbinghook : ability
    {

        public bool connected;
        public GameObject objectconnectedto;
        public GameObject hooktrail;
        public bool remove;


        [SerializeField] protected float hooklenght;
        [SerializeField] protected float minhooklenght;
        [SerializeField] protected float hookreelspeed;
        [SerializeField] protected float vecticalforce;
        [SerializeField] protected float horizontalforce;


        private float distancefromhookedobject;
        private bool candrawline;

        float gravitycontrol;
        protected override void initialization()
        {
            base.initialization();
            hooktrail.SetActive(false);
            Invoke("maybedisable", .1f);
            if (Weapon.currentweapon != null&&Weapon.currentweapon.projectile.tag!="grabbinghook")
            {

                enabled = false;
            }
            gravitycontrol = rb.gravityScale;
        }
        protected virtual void FixedUpdate()
        {
            grabbledfire();
            removegrabble();
            
        }
        protected virtual void grabbledfire()
        {

            if (input.Weaponfireheld() && Weapon.currentprojectile != null && Weapon.currentprojectile.GetComponent<projectile>().fired && Weapon.currentprojectile.tag == "grabbinghook")
            {


                distancefromhookedobject = Vector2.Distance(Weapon.gunbarrel.position, Weapon.currentprojectile.transform.position);
                candrawline = true;
                Invoke("drawline", .1f);
              
            }
            else
            {
                candrawline = false;
                drawline();
            }
            if (connected)
            {
                grabblehanging();
                input.grabup = true;//new

                rb.gravityScale = 50;  //new

            }
        }

        protected virtual void drawline()
        {

            if (candrawline)
            {

                hooktrail.SetActive(true);
                hooktrail.transform.position = Weapon.gunbarrel.position;
                hooktrail.transform.rotation = Weapon.gunrotation.rotation;
                //hooktrail.GetComponent<SpriteRenderer>().size = new Vector2(distancefromhookedobject, .64f);
                hooktrail.GetComponent<SpriteRenderer>().size = new Vector2(distancefromhookedobject, hooktrail.GetComponent<SpriteRenderer>().size.y);//new

            }
            else
            {
                distancefromhookedobject = 0;

                //hooktrail.GetComponent<SpriteRenderer>().size = new Vector2(0, .64f);
                hooktrail.GetComponent<SpriteRenderer>().size = new Vector2(distancefromhookedobject, hooktrail.GetComponent<SpriteRenderer>().size.y); //new
                hooktrail.SetActive(false);
                if (Weapon.currentprojectile != null && Weapon.currentprojectile.tag == "grabbinghook")
                {
                    Weapon.currentprojectile.GetComponent<projectile>().destroyprojectile();


                }

            }
        }
        protected virtual void grabblehanging()
        {
            rb.freezeRotation = false;
            anim.SetBool("grabbinghook", true);
            float step = hookreelspeed * Time.deltaTime;
            aimmanger.wheretoaim.transform.position = objectconnectedto.transform.position;
            aimmanger.aiminggun.transform.GetChild(0).position = aimmanger.wheretoaim.position;
            aimmanger.aiminglefthand.transform.GetChild(0).position = aimmanger.wheretoplacehand.position;
            Weapon.currentprojectile.GetComponent<projectile>().projectilelifetime = Weapon.currentweapon.lifetime;
            Weapon.currentprojectile.transform.position = objectconnectedto.transform.position;
            distancefromhookedobject = Vector2.Distance(Weapon.gunbarrel.position, objectconnectedto.transform.position);

            if (input.Upheld() && distancefromhookedobject >= minhooklenght)
            {

                transform.position = Vector2.MoveTowards(transform.position, objectconnectedto.transform.position,step);
            }

            if (input.Downheld() && distancefromhookedobject < hooklenght-.5f)
            {

                transform.position = Vector2.MoveTowards(transform.position, objectconnectedto.transform.position,-1* step);
            }

        }
        public virtual void removegrabble()
        {

            if (!input.Weaponfireheld() || Weapon.currenttimetillchangearms <= 0 || remove)
            {

                remove = true;
                input.grabup = false; //new
                if (connected)
                {
                    rb.gravityScale= gravitycontrol;
                    anim.SetBool("grabbinghook", false);
                    objectconnectedto.GetComponent<HingeJoint2D>().enabled = false;



                    objectconnectedto = null;
                    rb.AddForce(Vector2.up * vecticalforce);
                    if (!character.isfaceleft)
                    {
                        rb.AddForce(Vector2.right * horizontalforce);
                    }
                    else
                    {
                        rb.AddForce(Vector2.left * horizontalforce);
                    }
                    StartCoroutine(disablemovement());
                }
                if (Weapon.currentprojectile != null)
                {

                    Weapon.currentprojectile.transform.position = Weapon.gunbarrel.position;
                    Weapon.currentprojectile.GetComponent<projectile>().destroyprojectile();

                }
                returnhook();
            
            


            }

        }
      
        protected virtual void maybedisable()
        {

            if (distancefromhookedobject > hooklenght)
            {

                candrawline = false;
                drawline();
                Weapon.currentprojectile.GetComponent<projectile>().destroyprojectile();

            }

        }
        protected virtual void returnhook()
        {
            candrawline = false;
            connected = false;
            
            drawline();
        }

       protected virtual IEnumerator disablemovement()
        {

            movement.enabled = false;
          
            yield return new WaitForSeconds(.2f);
            movement.enabled = true;
            //gameObject.GetComponent<grabbinghook>().enabled = false;
            //Invoke("onhook", .5f);

        }
       protected virtual void onhook()
        {
            gameObject.GetComponent<grabbinghook>().enabled = true;

        }
      
    }
}
