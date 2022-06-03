using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class beanenemy : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Collider2D col;
        protected Animator anim;
        //
        [SerializeField] protected enum detectiontype { rectangle, circle }
        [SerializeField] protected detectiontype type;
        [SerializeField]protected bool inspectorfollowplayerisfound;
        
        [SerializeField] protected float radius;
        [SerializeField] protected float distance;
        [SerializeField] protected Vector2 detectionoffset;
        [SerializeField] protected LayerMask layer;
        [SerializeField] protected float mindistance;
        //
         protected bool followplayerisfound;
        //
        [SerializeField] protected bool isground;
        [SerializeField] protected bool spawnleft;
        [SerializeField] protected float maxspeed;
        [SerializeField] protected float tillmaxspeed;
        [SerializeField] protected float waitforsec;
        [SerializeField] protected LayerMask ground;
        protected float acceleration;
        protected float direction;
        protected float runtime;
        protected float currentspeed;
        [HideInInspector]public bool facingleft;
        protected bool canflip;
        protected bool forwardray;
        protected GameObject player;
        protected bool playerfnotfollow;

        [HideInInspector] public bool playerisclose;
        //
        protected float timetilldoaction;
        [SerializeField] protected float originaltimetilldoaction;


        protected virtual  void Start()
        {

            Initialization();
        }
        protected virtual void Initialization()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            anim = GetComponent<Animator>();
            player = FindObjectOfType<character>().gameObject;
            if (spawnleft)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                facingleft = true;
                Invoke("cancelspawn", .1f);
            }

        }

        // Update is called once per frame
       protected virtual void FixedUpdate()
        {
          
            checkground();
            movement();
            checkforwardray();
            turnon();
            checkforplayer();
            Followplayer();
          

        }
        protected virtual void cancelspawn()
        {
            spawnleft = false;
        }
        protected virtual void checkground()
        {
            if (!facingleft)
            {
               isground= Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.min.y), Vector2.down, .5f,ground);
            }
            else
            {

                isground = Physics2D.Raycast(new Vector2(col.bounds.min.x, col.bounds.min.y), Vector2.down, .5f,ground);
            }
        }
        protected virtual void movement()
        {
            if (isground)
            {
                if (!facingleft)
                {
                    direction = 1;
                    if (!forwardray)
                    {
                        if ((followplayerisfound&&transform.position.x>player.transform.position.x))
                        {

                            transform.localScale = (new Vector2(transform.localScale.x * -1f, transform.localScale.y));
                            facingleft = true;
                        }
                    }

                    }
                else
                {
                    direction = -1;
                    if (!forwardray)
                    {
                        if ((followplayerisfound && transform.position.x < player.transform.position.x))
                        {
                            transform.localScale = (new Vector2(transform.localScale.x * -1f, transform.localScale.y));
                            facingleft = false;
                        }
                    }

                }
                acceleration = maxspeed / tillmaxspeed;
                runtime += Time.deltaTime;
                currentspeed = direction * runtime * acceleration;
                checkspeed();
                rb.velocity = new Vector2(currentspeed, rb.velocity.y);
                anim.SetBool("run", true);
                
            }
            else
            {
                currentspeed = 0;
                runtime = 0;
                acceleration = 0;
                //rb.velocity = Vector2.zero;
                anim.SetBool("run", false);
                canflip = true;
                Invoke("flip", waitforsec);
               

            }
        }
        
        protected virtual void flip()
        {

          
           
            if (canflip)
            {
                
                //facingleft = !facingleft;
                //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            
                if (facingleft)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    facingleft = false;
                }
                else
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    facingleft = true;
                }
                canflip = false;

            }

        }
        protected virtual void checkspeed()
        {

            if (currentspeed < -maxspeed)
            {
                currentspeed = -maxspeed;
            }
            if(currentspeed>maxspeed)
            {

                currentspeed = maxspeed;
            }
        }
        protected virtual void checkforwardray()
        {
            if (!facingleft)
            {
                forwardray = Physics2D.Raycast(new Vector2(col.bounds.max.x, col.bounds.max.y-.2f), Vector2.right, .5f, ground);
            }
            else
            {

                forwardray = Physics2D.Raycast(new Vector2(col.bounds.min.x, col.bounds.max.y-.2f), Vector2.left, .5f, ground);
            }

        }
        protected virtual void turnon()
        {
            if (forwardray)
            {
                if (facingleft)
                {
               
                    transform.localScale=(new Vector2(transform.localScale.x * -1f, transform.localScale.y));
                    facingleft = false;
                }
                else if (!facingleft)
                {
                    transform.localScale = (new Vector2(transform.localScale.x * -1f, transform.localScale.y));
                    facingleft = true;
                }
                playerfnotfollow = true;
                Invoke("waitforplayerdet", 5f);
            }
            

        }
        protected virtual void waitforplayerdet()
        {
            playerfnotfollow = false;

        }
        protected virtual void checkforplayer()
        {

            RaycastHit2D hit;
            if (type == detectiontype.rectangle)
            {

                if (!facingleft)
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x + col.bounds.extents.x + detectionoffset.x + (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, layer);


                }
                else
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x - col.bounds.extents.x - detectionoffset.x - (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, layer);

                }
                if (hit&&isground&&!forwardray&& !playerfnotfollow)
                {

                    followplayerisfound = true;
                    playerisclose = true;
                   
                }
                else
                {
                    followplayerisfound = false;
                    playerisclose = false;

                }
            }
            if (type == detectiontype.circle)
            {
                hit = Physics2D.CircleCast(col.bounds.center, radius, Vector2.zero, 0, layer);
                if (hit)
                {

                    if (isground&&!forwardray&&!playerfnotfollow)
                    {
                        
                            followplayerisfound = true;

                        playerisclose = true;
                    }
                    else
                    {
                        followplayerisfound = false;
                        playerisclose = false;
                    }
                  
                }
                else
                {
                    followplayerisfound = false;
                    playerisclose = false;


                }

            }
        }
        

    

        protected virtual void Followplayer()
        {
            if (followplayerisfound&&inspectorfollowplayerisfound)
            {
                
                bool tooclose = new bool();
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < mindistance)
                {
                    tooclose = true;

                }
                else
                {
                    tooclose = false;

                }
                if (!facingleft)
                {

                
                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                    
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), currentspeed * Time.deltaTime));
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);
                    }
                   
                }
                else
                {
              
                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                   
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), -currentspeed * Time.deltaTime));
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);
                    }
                  

                }
            }

        }
        private void OnDrawGizmos()
        {
            col = GetComponent<Collider2D>();
            if (type == detectiontype.rectangle)
            {
                Gizmos.color = Color.red;
                if (transform.localScale.x > 0)
                {
                    Gizmos.DrawWireCube(new Vector2(transform.position.x + col.bounds.extents.x + detectionoffset.x + (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y));
                }
                else
                {
                    Gizmos.DrawWireCube(new Vector2(transform.position.x - col.bounds.extents.x - detectionoffset.x - (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y));
                }
            }
            if (type == detectiontype.circle)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(col.bounds.center, radius);

            }
        }
    }
}
