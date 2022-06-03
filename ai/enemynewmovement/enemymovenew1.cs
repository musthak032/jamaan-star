using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania {
    public class enemymovenew1 :AiManager
    {
        [SerializeField] protected bool spawnleft;
        
        [SerializeField] protected LayerMask ground;
        [SerializeField] protected bool isground;
        [SerializeField] protected float tilltimemaxspped;
        [SerializeField] protected float maxspeed;
        [SerializeField] protected float mindistance;

        protected bool facingleft;
        protected float direction;    
        protected float accelaration;
        protected float currentspeed;
        protected float runtime;
      


        protected Animator anim;
        protected override void Initialization()
        {
            base.Initialization();
            anim = GetComponent<Animator>();
            if (spawnleft)
            {
                facingleft = true;
                transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
            }
            else
            {

                facingleft = false;
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            }
            Invoke("spawnoff",.5f);
           
        }
        protected virtual void Update()
        {

       
        }

        protected virtual void FixedUpdate()
        {
            checkground();
            turngroundend();
            move();
            Followplayer();
        }
        protected virtual void checkground()
        {
           
            if(facingleft)
            {

                Ray2D hits = new Ray2D();
                hits.origin = new Vector2(col.bounds.min.x, col.bounds.min.y);
                isground = Physics2D.Raycast(hits.origin, Vector2.down, 2f, ground);
            }
            if(!facingleft)
            {
                Ray2D hits = new Ray2D();
                hits.origin = new Vector2(col.bounds.max.x, col.bounds.min.y);
                isground = Physics2D.Raycast(hits.origin, Vector2.down, 2f, ground);

            }
          

        }
       

        protected virtual void turngroundend()
        {

            if (facingleft&&!isground)
            {
                facingleft = false;
                transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y);
               
                
            }
            else if (!facingleft && !isground)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                facingleft = true;
               
            }

        }
        protected virtual void move()
        {
            if (facingleft)
            {

                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
                
            }
            if(!facingleft)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
               
            }

            direction = transform.localScale.x;
            accelaration = maxspeed / tilltimemaxspped;
             runtime +=Time.deltaTime;
            currentspeed =direction* accelaration * runtime;
            checkspeed();
            rb.velocity = new Vector2(currentspeed, rb.velocity.y);
            anim.SetBool("run", true);
           

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
        protected virtual void Followplayer()
        {
            if (enemycharacter.followplayer)
            {

                bool tooclose = new bool();
                if (Mathf.Abs(transform.position.x - player.transform.position.x) < mindistance)
                {
                    tooclose = true;

                    anim.SetBool("run", false);
                }
                else
                {
                    tooclose = false;
                }
                if (!enemycharacter.faceleft)
                {

                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;

                    //transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, currentspeed * Time.deltaTime);
                    //rb.MovePosition(Vector2.MoveTowards(transform.position, distancetoplayer, currentspeed * Time.deltaTime));
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), currentspeed * Time.deltaTime));

                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                }
                else
                {

                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                    //transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, -currentspeed * Time.deltaTime);
                    //rb.MovePosition(transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, -currentspeed * Time.deltaTime));
                    rb.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), -currentspeed * Time.deltaTime));

                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }

                }

                //
            }

        }
        protected virtual void spawnoff()
        {

            spawnleft = false;

        }
        
        
        
    } 
}
