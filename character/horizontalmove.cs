using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania 
{
    public class horizontalmove :ability
    {
        [SerializeField]
        protected float speedmultiplayer;
        [SerializeField]
        protected float maxspeed;
        [SerializeField]
        protected float speed;
        float runtime;
        public List<Vector3> deltapositions = new List<Vector3>();
        public Vector3 bestdeltaposition;
        public float direction; //mobile input kaga public
        float currentspeed;
        float acceleration;
        private float deltapositioncountdown = 1;
        private float deltapositioncountdowncurrent = 0;
        [SerializeField]
        protected float tilspeed;
        //crouch
        [SerializeField]
        protected float crouchspeedmultiplayer;
        [SerializeField]
        protected float hookspeedmultiplayer;
        [SerializeField]
        protected float ladderspeed;

        protected bool above;
      
      

        [HideInInspector] public GameObject currentladder;


        protected virtual void Update()
        {
            movementpress();
          
        }
        protected virtual void FixedUpdate()
        {
            moveplayer();
            removefromgrabble();
            laddermovement();
            previousgroundedposition();
        }
       public virtual bool movementpress()
        {
            if (mobinput.mobmoveleft)
            {
                direction = -1f;
                return true;
            }
             if (mobinput.mobmoveright)
            {
                direction = +1f;
                return true;
            }
            if (Input.GetAxis("Horizontal")!=0)
            {
               
                
                    direction = Input.GetAxis("Horizontal");
                
                return true;
            }
            else
            {
                return false;
            }
           
           
           
        }
     


        protected virtual void moveplayer()
        {
            if (Gamemanager.gamepause)
            {
                return;
            }
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, Gamemanager.xmin, Gamemanager.xmax), Mathf.Clamp(transform.position.y, Gamemanager.ymin, Gamemanager.ymax));


            if (movementpress())
            {
                anim.SetBool("walking", true);
                acceleration = maxspeed / tilspeed;



                runtime += Time.deltaTime;
                currentspeed = direction * acceleration*runtime;
               

                checkspeed();
                
            }
            else
            {
                anim.SetBool("walking", false);
                acceleration = 0;
                runtime = 0;
                currentspeed = 0;
               
            }
            speedmulti();
            anim.SetFloat("move", currentspeed);
            rb.velocity = new Vector2(currentspeed, rb.velocity.y);
            
        }
        protected virtual void removefromgrabble()
        {
            if (grabbinghook.remove)
            {

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 500);
                if (transform.rotation == Quaternion.identity)
                {

                    grabbinghook.remove = false;
                    rb.freezeRotation = true;
                }
            }

        }

        protected virtual void laddermovement()
        {

            if (character.isonladder && currentladder != null)
            {

                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                if (col.bounds.min.y >= (currentladder.GetComponent<lader>().topofladder.y - col.bounds.extents.y))
                {

                    anim.SetBool("onladder", false);
                    above = true;
                }
                else
                {
                    anim.SetBool("onladder", true);
                    above = false;

                }
                if (input.Upheld())
                {

                    anim.SetBool("climbingladder", true);
                    transform.position = Vector2.MoveTowards(transform.position, currentladder.GetComponent<lader>().topofladder, ladderspeed * Time.deltaTime);
                    //transform.position = Vector2.MoveTowards(new Vector2( transform.position.x,transform.position.y), new Vector2( currentladder.GetComponent<lader>().topofladder.x, currentladder.GetComponent<lader>().topofladder.y), ladderspeed * Time.deltaTime);
                    if (above)
                    {
                        anim.SetBool("climbingladder", false);
                    }
                    return;
                }
                else
                {
                    anim.SetBool("climbingladder", false);

                }
                if (input.Downheld())
                {
                    anim.SetBool("climbingladder", true);
                    transform.position = Vector2.MoveTowards(transform.position, currentladder.GetComponent<lader>().bottomofladder, ladderspeed * Time.deltaTime);
                    return;
                }
            }
            else
            {
                anim.SetBool("onladder", false);
                 
                {
                    rb.bodyType = RigidbodyType2D.Dynamic; 
                }
            }
        }
        protected virtual void previousgroundedposition()
        {
            if (character.isground && movementpress())
            {
                deltapositioncountdowncurrent -= Time.deltaTime;
                if (deltapositioncountdowncurrent < 0)
                {

                    if (deltapositions.Count == 10)
                    {

                        deltapositions.RemoveAt(0);
                    }
                    deltapositions.Add(transform.position);
                    deltapositioncountdowncurrent = deltapositioncountdown;
                    bestdeltaposition = deltapositions[0];
                }
            }

        }
        public virtual void checkspeed()
        {
            if (currentspeed > 0)
            {
                if (character.isfaceleft)
                {

                    character.isfaceleft = false;
                    flip();
                }

                if (currentspeed > maxspeed)
                {
                    currentspeed = maxspeed;


                }
            }
            if (currentspeed < 0)
            {
                if (!character.isfaceleft)
                {

                    character.isfaceleft = true;
                    flip();
                }

                if (currentspeed < -maxspeed)
                {

                    currentspeed = -maxspeed;

                }
            }

           
        }
        protected virtual void speedmulti()
        {
            if (input.sprintheld())
            {
                //anim.SetBool("sprinting", true);
                currentspeed *= speedmultiplayer;
                //anim.SetFloat("move", currentspeed);
            }
            if (character.iscrouching)
            {
                currentspeed *= crouchspeedmultiplayer;
            }
            if (character.iswallsliding)
            {
                currentspeed = 0;
            }
            if (grabbinghook.connected)
            {
               
                if (input.Upheld()||input.Downheld()||collisioncheck(Vector2.right,.1f,jumpscript.ground)||collisioncheck(Vector2.left,.1f,jumpscript.ground)|| collisioncheck(Vector2.up, .1f, jumpscript.ground) || character.isground)
                {

                    return;
                }
                currentspeed *= hookspeedmultiplayer;
                if (grabbinghook.hooktrail.transform.position.y > grabbinghook.objectconnectedto.transform.position.y)        // change grabbing
                {

                    currentspeed *= -hookspeedmultiplayer;
                   

                }
               





                rb.rotation -=currentspeed;//original
            }
            if (character.iswallsliding)
            {
                currentspeed = .01f;

            }
            if (currentflateform!=null&& (!currentflateform.GetComponent<OneWayPlateform>() || !currentflateform.GetComponent<lader>())) 
            {
                if (!character.isfaceleft && collisioncheck(Vector2.right, .05f, jumpscript.ground) || character.isfaceleft && collisioncheck(Vector2.left, .05f, jumpscript.ground))
                {
                    currentspeed = .01f;
                } 
            }
          
        }
    }
}
