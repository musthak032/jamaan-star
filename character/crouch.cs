using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
[RequireComponent(typeof(CapsuleCollider2D))]

    public class crouch :ability
    {
        //crouchability basic
        private CapsuleCollider2D Playercollider;
        private Vector2 originalcollider;
        private Vector2 crouchingcollidersize;
        private Vector2 originaloffset;
        private Vector2 crouchingoffset;
        [SerializeField] [Range(0, 1)] protected float collidermultiplayer;
        [SerializeField] protected LayerMask layer;

        protected override void initialization()
        {
            base.initialization();
            Playercollider = GetComponent<CapsuleCollider2D>();
            originalcollider = Playercollider.size;
            crouchingcollidersize = new Vector2(Playercollider.size.x, (Playercollider.size.y * collidermultiplayer));
            originaloffset = Playercollider.offset;
            crouchingoffset = new Vector2(Playercollider.offset.x, (Playercollider.offset.y * collidermultiplayer));

        }
     




        protected virtual void FixedUpdate()
        {

            crouching();
        }
        //protected virtual bool  crouchheld()
        //{
        //    if (Input.GetKey(KeyCode.X))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        protected virtual void crouching()
        {
            if ((input.crouchheld()&&character.isground)&&!character.grabbingLedge) 
            {
               character.iscrouching = true;
                anim.SetBool("crouching", true);
                Playercollider.size = crouchingcollidersize;
                Playercollider.offset = crouchingoffset;
            }
            else
            {
                if (character.iscrouching)
                {
                    if (collisioncheck(Vector2.up, Playercollider.size.y*.25f, layer))
                    {
                        return;
                    }
                    StartCoroutine(crouchdisable());
                }
            }
        }
        protected virtual IEnumerator crouchdisable()
        {
            Playercollider.offset= originaloffset;
            yield return new WaitForSeconds(.01f);
            Playercollider.size = originalcollider;
            yield return new WaitForSeconds(.15f);
            character.iscrouching = false;
            anim.SetBool("crouching", false);
        }
    }
}
