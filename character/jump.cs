using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class jump : ability
    {
        ///check player is in ground
        
        public LayerMask ground;


        //check player layerdistance

        [SerializeField]
        protected float distanceofcol;

        //check playerjump in this script

       //private bool isjump;

        ///check player jump force
        
        public float jumpforce;

        /// multiplejump
        [SerializeField]
        protected int maxjump;

       private int numberofjumpleft;
        //limited jump fall speed
        [SerializeField] protected bool limitairjump;
        [SerializeField] protected float maxjumpspeed;
        [SerializeField] protected float maxfallspeed;
        [SerializeField] protected float acceptspeed;
        //jumpmore height
        [SerializeField] protected float holdforce;
             private float jumpcountdown;
        [SerializeField] protected float buttonholdtime;
        //glide
        [SerializeField] protected float glidetime;
        [SerializeField] [Range(2, -2)] protected float gravity;
        private float fallcountdown;
        /// wall jump
        /// 

        private bool iswalljump;
        [SerializeField] protected float horizontalwalljumpforce;
        [SerializeField] protected float verticalwalljumpforce;
        [SerializeField] protected float walljumptime;
        //wall slide
        private bool flipped;
        //help handle control movemnt while wallljump
        private bool justwalljumpped;
        private float walljumpcountdown;
        protected override void initialization()
        {
            base.initialization();
            numberofjumpleft = maxjump;
            jumpcountdown = buttonholdtime;
            fallcountdown = glidetime;
            walljumpcountdown = walljumptime;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            //jumppressed();
            //jumpheld();
            checkforjump();
        }
        protected virtual void FixedUpdate()
        {
           
            allowjump();
            groundcheck();
            gliding();
            wallsliding();
            walljump();
        }
        //for get input to use bool 
        protected virtual bool checkforjump()
        {
            if (Gamemanager.gamepause)
            {
                return false;
            }
            if (character.grabbingLedge) //new
            {
                return false;
            }
            if (input.jumppressed())
            {
                if(currentflateform != null && currentflateform.GetComponent<OneWayPlateform>() && input.Downheld())
                {
                    character.isjumpingthroughplateform = true;
                    Invoke("notjumpingthroughplateform", .1f);
                    return false;

                }
                //check player to see fell of jumpand then trie to jump
                if (!character.isground && numberofjumpleft == maxjump)
                {
                   character.isjump = false;
                    input.jumpmo = false; //mobile
                    return false;
                  


                }
                //check to see if player don jump when its falling
                if (limitairjump && character.falling(acceptspeed))
                {
                   character.isjump = false;
                    input.jumpmo = false; //mobile

                    return false;
                }
                // check to see player in wall to perform wall jump
                if (character.iswallsliding&& walljumpability)
                {
                    walljumptime = walljumpcountdown;
                    iswalljump = true;
                    input.jumpmo = false; //mobile


                    return false;
                }
                //none of statement take place perform this flow
                numberofjumpleft--;
                if (numberofjumpleft >= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    jumpcountdown = buttonholdtime;
                   character.isjump = true;
                    input.jumpmo = false; //mobile

                    fallcountdown = glidetime;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        // this method help for glid in air
        protected virtual void gliding()
        {                                         
            if (character.falling(0)&&input.jumpheld())       //falling() from charater to maintan gravity for gliding
            {
                fallcountdown -= Time.deltaTime;    // it help to maintain time for gliding time
                if (fallcountdown > 0&&rb.velocity.y>acceptspeed)
                {
                    anim.SetBool("gliding", true);
                   
                    fallspeed(gravity);
                    return;
                }
            }
            anim.SetBool("gliding", false);
    

        }
       
        // this methoed allow player jump not hold and check additional air for hold force jump
        protected virtual void allowjump()
        {
           
            if (character.isjump)
            {
                
                rb.AddForce(Vector2.up * jumpforce);
                additionalair();
            }
            if (rb.velocity.y > maxjumpspeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxjumpspeed);
                
            }
           
        }
        // this function make double jump in some height whent it reach make perform
        protected virtual void additionalair()
        {
            if (input.jumpheld()||input.jumpholdmo)
            {
               
                jumpcountdown -= Time.deltaTime;
                if (jumpcountdown <= 0)
                {
                    jumpcountdown = 0;
                   character.isjump = false;
                }
                else
                {
                    rb.AddForce(Vector2.up * holdforce);
                }
            }
            else
            {
               character.isjump = false;
            }
        }
        protected virtual void groundcheck()
        {
            if (collisioncheck(Vector2.down, distanceofcol, ground)&&!character.isjump)
            {
                anim.SetBool("jumpb",false);
                character.isground = true;
                numberofjumpleft = maxjump;
                fallcountdown = glidetime;
                justwalljumpped = false;
                
            }
            else
            {
                anim.SetBool("jumpb", true);
                character.isground = false;

                if (character.falling(0) && rb.velocity.y < maxfallspeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, maxfallspeed);
                   
                }
               
            }

            anim.SetFloat("jump",rb.velocity.y);
        }
       //ccheck player walljump when not use actual jump and check wall for wall sliding
        protected virtual bool wallcheck()
        {
            if ((!character.isfaceleft && collisioncheck(Vector2.right, distanceofcol, ground) || character.isfaceleft && collisioncheck(Vector2.left, distanceofcol, ground)) && movement.movementpress() && !character.isground)
            {
                if (justwalljumpped)
                {

                    walljumptime = 0;
                    justwalljumpped = false;
                    iswalljump = false;


                    movement.enabled = true;
                }
                if (currentflateform.GetComponent<OneWayPlateform>() || currentflateform.GetComponent<lader>())
                {
                    return false;
                }
                return true;
            }

          
                return false;
            
        }
        //check player isfaceleft when heis collison on wall
        protected virtual bool wallsliding()
        {
           if(wallcheck())
            {
                if (!flipped)
                {
                    // charcter script flip method
                    flip();
                    flipped = true;
                }
                fallspeed(gravity);
                character.iswallsliding = true;
                anim.SetBool("wallsliding", true);
               
                return true;
            }
            else
            {
                character.iswallsliding = false;
                anim.SetBool("wallsliding", false);
                if (flipped && !iswalljump)
                {
                    flip();
                    flipped = false;
                }
                return false;
            }
        }
        // wall jump force occur on this method it contain moemtscript disable
        protected virtual void walljump()
        {

            if (iswalljump)
            {
                rb.AddForce(Vector2.up * verticalwalljumpforce);
                if (!character.isfaceleft)
                {
                    rb.AddForce(Vector2.left * horizontalwalljumpforce);
                }
                if (character.isfaceleft)
                {
                    rb.AddForce(Vector2.right * horizontalwalljumpforce);
                }
                movement.enabled = false;
                Invoke("justwalljump",.05f);
                //StartCoroutine(walljumped());
            }
            if (walljumptime > 0)
            {
                walljumptime -= Time.deltaTime;
                if (walljumptime <= 0)
                {
                    movement.enabled = true;
                    iswalljump = false;
                    walljumptime = 0;
                }
            }
        }
        protected virtual void notjumpingthroughplateform()
        {

            character.isjumpingthroughplateform = false;
        }
        protected virtual void justwalljump()
        {
            justwalljumpped = true;
        }
       





    }
}
