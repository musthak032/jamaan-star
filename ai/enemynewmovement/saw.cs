using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class saw : MonoBehaviour
    {
        [SerializeField] protected float maxspeed;   
        [SerializeField] protected float raylength;
        [SerializeField] protected float timetillmaxspeed;
        [SerializeField] protected int damageamount;
        [SerializeField] protected LayerMask ground;
        protected float acceleration;
        protected float direction;
        protected float runtime;
        protected float currentspeed;
        protected Rigidbody2D rb;
        protected Collider2D col;
        public GameObject groundray;
        public GameObject upgroundray;
        public bool onground;
       [SerializeField ]protected bool right;
        [SerializeField] protected bool up;
        protected GameObject player;
        protected bool hit;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>(); 
            col = GetComponent<Collider2D>();
            player = FindObjectOfType<character>().gameObject;
        }

        // Update is called once per frame
        void Update()
        {
       
           
        }
        protected virtual void FixedUpdate()
        {
            checkground();
            movement();
        }
        protected virtual void movement()
        {
            if (!up)
            {
                if (right)
                {

                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                else
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                }

                if (!onground)
                {
                    if (right)
                    {
                        right = false;
                    }
                    else
                    {
                        right = true;
                    }
                }

                acceleration = maxspeed / timetillmaxspeed;

                runtime += Time.deltaTime;
                currentspeed = acceleration * runtime;
                checkspeed();


                if (right)
                {
                    rb.velocity = new Vector2(currentspeed, rb.velocity.y);
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                else
                {
                    rb.velocity = new Vector2(-currentspeed, rb.velocity.y);
                    transform.localScale = new Vector2(-1, transform.localScale.y);

                }
            }
            else
            {
                if (right)
                {

                    transform.localScale = new Vector2(transform.localScale.x, 1);
                }
                else
                {
                    transform.localScale = new Vector2(transform.localScale.x, -1);
                }

                if (!onground)
                {
                    if (right)
                    {
                        right = false;
                    }
                    else
                    {
                        right = true;
                    }
                }

                acceleration = maxspeed / timetillmaxspeed;

                runtime += Time.deltaTime;
                currentspeed = acceleration * runtime;
                checkspeed();


                if (right)
                {
                    rb.velocity = new Vector2(0, currentspeed);
                    transform.localScale = new Vector2(transform.localScale.x, 1);
                }
                else
                {
                    rb.velocity = new Vector2(0, -currentspeed);
                    transform.localScale = new Vector2(transform.localScale.x, -1);

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
        protected virtual void dealdamage()
        {
            if (hit)
            {
                if (player.transform.position.x < transform.position.x)
                {

                    player.GetComponent<playerhealth>().left = false;
                }
                else
                {
                    player.GetComponent<playerhealth>().left = true;

                }
              player.GetComponent<playerhealth>().dealdamage(damageamount);
                hit = false;
            }


        }
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == player)
            {
                hit = true;
                dealdamage();
                collision.gameObject.GetComponent<playersound>().kundila.Play();
            }
            
        }
        protected virtual void checkground()
        {





            if (up)
            {
                onground = Physics2D.Raycast(upgroundray.transform.position, Vector2.right, raylength, ground);
            }
            else
            {
                onground = Physics2D.Raycast(groundray.transform.position, Vector2.down, raylength, ground);

            }


        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(new Vector2( groundray.transform.position.x,groundray.transform.position.y),new Vector2(groundray.transform.position.x,groundray.transform.position.y+raylength ));
            Gizmos.DrawLine(new Vector2(upgroundray.transform.position.x, groundray.transform.position.y), new Vector2(upgroundray.transform.position.x + raylength, groundray.transform.position.y));
        }
    }
}
