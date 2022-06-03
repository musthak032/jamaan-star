using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class EnemeyMovement :AiManager
    {
        [HideInInspector] public bool turn;

        [SerializeField] protected enum movementtype { normal,hugwall,flyenemy}

        [SerializeField] protected movementtype type;
        [SerializeField] protected float timetillmaxspeed;
        [SerializeField] protected float maxspeed;
        [SerializeField] protected float jumpverticalforce;
        [SerializeField] protected float jumphorzontalforce;
        [SerializeField] protected float mindistance;
        [SerializeField] protected bool spawnfacingleft;
        [SerializeField] protected bool turnaroundoncollision;
        [SerializeField] protected bool avoidfalling;
        [SerializeField] protected bool jump;
        public bool standstill;

        [SerializeField] protected LayerMask collidertoturnaroundon;

        private float acceleration;
        private float runtime;
        private float direction;
        private bool spawning = true;



        protected float currentspeed;   
        protected float originalwaittime=.1f;
        protected float currentwaittime;
        protected bool wait;
        protected bool wasjumping;

        protected Animator anim;

        protected override void Initialization()
        {
            base.Initialization();
            anim = GetComponent<Animator>();
            if (spawnfacingleft)
            {
                enemycharacter.faceleft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
            currentwaittime = originalwaittime;
            timetilldoaction = originaltimetilldoaction;
            Invoke("Spawning", .01f);
        }
        protected virtual void FixedUpdate()
        {

            Movement();
            CheckGround();
           
            Hugwall();
            handlewait();
            edgeoffloor();
            jumping();
            Followplayer();
        }
        protected virtual void Movement()
        {
            if(type== movementtype.flyenemy)
            {
                rb.gravityScale = 0;
            }
            if (!enemycharacter.faceleft)
            {

                direction = 1;
                if (collisioncheck(Vector2.right, .5f, collidertoturnaroundon) && turnaroundoncollision && !wasjumping &&!spawning ||(enemycharacter.followplayer&&player.transform.position.x<transform.position.x))
                {

                    enemycharacter.faceleft = true;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    if (standstill)
                    {
                        rb.velocity = new Vector2(-jumphorzontalforce, rb.velocity.y);
                    }
                }
            }
            else
            {

                direction = -1;
                if (collisioncheck(Vector2.left, .5f, collidertoturnaroundon) && turnaroundoncollision && !wasjumping && !spawning || (enemycharacter.followplayer && player.transform.position.x > transform.position.x))
                {

                    enemycharacter.faceleft = false;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    if (standstill)
                    {
                        rb.velocity = new Vector2(jumphorzontalforce, rb.velocity.y);
                    }
                }
            }
            if (rb.velocity.x==0)
            {
                anim.SetBool("run", false);
            }
            else
            {
                anim.SetBool("run",true);
            }
          
                acceleration = maxspeed / timetillmaxspeed;

                runtime += Time.deltaTime;
                currentspeed = direction * acceleration * runtime;
                checkspeed();
            
            if (!standstill && !enemycharacter.followplayer)
            {
                rb.velocity = new Vector2(currentspeed, rb.velocity.y);
            }
        }
        protected virtual void Followplayer()
        {
            if (enemycharacter.followplayer)
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
                if (!enemycharacter.faceleft)
                {
                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                    //transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, currentspeed * Time.deltaTime);
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), currentspeed * Time.deltaTime);
                    //rb.MovePosition(Vector2.MoveTowards(transform.position, distancetoplayer, currentspeed * Time.deltaTime)); //new
                    //rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), currentspeed * Time.deltaTime)); //new
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);//new
                    }
                }
                else
                {
                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                    //transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, -currentspeed * Time.deltaTime);
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y),-currentspeed * Time.deltaTime);
                    //rb.MovePosition(transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, -currentspeed * Time.deltaTime)); //ne
                    //rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), -currentspeed * Time.deltaTime));
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);//new
                    }

                }
            }

        }
        protected virtual void checkspeed()
        {
            if (currentspeed > maxspeed)
            {
                currentspeed = maxspeed;
            }
            if (currentspeed < -maxspeed)
            {

                currentspeed = -maxspeed;
            }

        }
        protected virtual void jumping()
        {

            if (type == movementtype.normal)
            {
                if (rayhitnumber > 0 && jump)
                {
                    timetilldoaction -= Time.deltaTime;
                    if (timetilldoaction <= 0)
                    {
                        rb.AddForce(Vector2.up * jumpverticalforce);
                        if (!enemycharacter.faceleft)
                        {
                            rb.velocity = new Vector2(jumphorzontalforce, rb.velocity.y);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-jumphorzontalforce, rb.velocity.y);

                        }
                    }
                    if (rayhitnumber > 0 && rb.velocity.y < 0)
                    {

                        wasjumping = true;
                        if (standstill)
                        {
                            rb.velocity = new Vector2(0, rb.velocity.y);
                        }
                        timetilldoaction = originaltimetilldoaction;
                        anim.SetBool("jump", true); //new
                        Invoke("nolongerintheair", .5f);
                        Invoke("turnoffjumpanim",5f);
                    }

                }
            }
        }
        protected virtual void turnoffjumpanim()
        {
            anim.SetBool("jump", false);
        }
        protected virtual void nolongerintheair()
        {
            //anim.SetBool("jump", false); //new

            wasjumping = false;
        }

        protected virtual void edgeoffloor()
        {

            if (rayhitnumber == 1 && avoidfalling && !wait && type==movementtype.normal)
            {
                currentwaittime = originalwaittime;
                wait = true;
                enemycharacter.faceleft = !enemycharacter.faceleft;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
        protected virtual void Spawning()
        {

            spawning = false;
        }
        protected virtual void Hugwall()
        {

            if (type == movementtype.hugwall)
            {

                turnaroundoncollision = false;
                float newzvalue = transform.localEulerAngles.z;
                rb.gravityScale = 0;
                if (rayhitnumber == 1 && !wait)
                {
                    wait = true;
                    currentwaittime = originalwaittime;
                    rb.velocity = Vector2.zero;
                    if (!enemycharacter.faceleft)
                    {
                        transform.localEulerAngles = new Vector3(0, 0,newzvalue-90);
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, newzvalue + 90);
                    }
                }
                if (turn && !wait)
                {

                    wait = true;
                    currentwaittime = originalwaittime;
                    rb.velocity = Vector2.zero;
                    if (!enemycharacter.faceleft)
                    {
                        transform.localEulerAngles = new Vector3(0, 0, newzvalue + 90);
                        if (Mathf.Round(transform.eulerAngles.z) == 0)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y - (transform.localScale.x * .5f));
                        }
                        if(Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y + (transform.localScale.x * .5f));
                        }
                        if (Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            transform.position = new Vector2(transform.position.x+(transform.localScale.x*.5f), transform.position.y);
                        }
                        if (Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            transform.position = new Vector2(transform.position.x - (transform.localScale.x * .5f), transform.position.y);
                        }
                    }   
                   else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, newzvalue - 90);
                        if (Mathf.Round(transform.eulerAngles.z) == 0)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y + (transform.localScale.x * .5f));
                        }
                        if(Mathf.Round(transform.eulerAngles.z) == 180)
                        {
                            transform.position = new Vector2(transform.position.x, transform.position.y - (transform.localScale.x * .5f));
                        }
                        if (Mathf.Round(transform.eulerAngles.z) == 90)
                        {
                            transform.position = new Vector2(transform.position.x-(transform.localScale.x*.5f), transform.position.y);
                        }
                        if (Mathf.Round(transform.eulerAngles.z) == 270)
                        {
                            transform.position = new Vector2(transform.position.x + (transform.localScale.x * .5f), transform.position.y);
                        }
                    }
        
                }
                if (Mathf.Round(transform.eulerAngles.z) == 0)
                {

                    rb.velocity = new Vector2(currentspeed, 0);
                }
                if (Mathf.Round(transform.eulerAngles.z) == 90)
                {

                    rb.velocity = new Vector2(0, currentspeed);
                }
                if (Mathf.Round(transform.eulerAngles.z) == 180)
                {

                    rb.velocity = new Vector2(-currentspeed, 0);
                }
                if (Mathf.Round(transform.eulerAngles.z) == 270)
                {

                    rb.velocity = new Vector2(0, -currentspeed);
                }
                if (rayhitnumber == 0 && !wait)
                {

                    transform.localEulerAngles = Vector3.zero;
                    rb.gravityScale = 1;
                }
            }
        }
        protected virtual void handlewait()
        {
            currentwaittime -= Time.deltaTime;
            if (currentwaittime <= 0)
            {

                wait = false;
                currentwaittime = 0;
            }

        }

    }
}
