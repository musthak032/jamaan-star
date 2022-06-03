using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class alienmovement : MonoBehaviour
    {
        [SerializeField] protected enum detectiontype { rectangle, circle }
        [SerializeField] protected detectiontype type;
        public bool followplayerisfound;
       
        public bool spawnleft;
        public float radius;
        public float distance;
        public float mindistance;
        public float maxspeed;
        public float timetillmaxspeed;
       
        public float originalwaittime;
        public float bulletoriginalwaittime;


        public Vector2 detectionoffset;
        public bool standstill;
        public LayerMask playerlayer;
        public LayerMask groundlayerlayer;
        public LayerMask collidertoturnaroundon;
        public Transform bulletposition;
        public weapontype bullettype;
        public List<GameObject> destroybullets;

        Rigidbody2D rb;
        Animator anim;
        Collider2D col;

        bool followplayer;
        bool playerisclose;
        bool facingleft;
       public bool turnaroundoncollision;
        bool spawning = true;
        float currentspeed;
        float acceleration;
        float runtime;
        float timetowait;
        float bulletwait;
        int direction;

         Ray2D forwardray = new Ray2D();
      
      public  bool groundhit;
        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            player = FindObjectOfType<character>().gameObject;
            if (spawnleft == true)
            {
                gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
                facingleft = true;
            }
            else
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
                facingleft = false;
            }
            Invoke("Spawning", .01f);
            timetowait = originalwaittime;
            bulletwait = bulletoriginalwaittime;
        }
        protected virtual void Spawning()
        {

            spawning = false;
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            Followplayer();
            shootplayer();
        }
        private void FixedUpdate()
        {
            playerdetection();
            checkground();
        }

        protected virtual void playerdetection()
        {

            RaycastHit2D hit;
            if (type == detectiontype.rectangle)
            {

                if (!facingleft)
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x + col.bounds.extents.x + detectionoffset.x + (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, playerlayer);


                }
                else
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x - col.bounds.extents.x - detectionoffset.x - (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, playerlayer);

                }
                if (hit&&groundhit)
                {

                    if (followplayerisfound)
                    {
                        followplayer = true;
                    }
                    playerisclose = true;
                }
                else
                {
                    followplayer = false;
                    playerisclose = false;
                    if (standstill)
                    {
                        rb.velocity = Vector2.zero;
                    }


                }
               
            }
            if (type == detectiontype.circle)
            {
                hit = Physics2D.CircleCast(col.bounds.center, radius, Vector2.zero, 0, playerlayer);
                if (hit&&groundhit)
                {

                    if (followplayerisfound)
                    {
                        followplayer = true;
                    }
                    playerisclose = true;
                }
                else
                {
                   followplayer = false;
                    playerisclose = false;
                    if (standstill)
                    {
                        rb.velocity = Vector2.zero;
                    }


                }

            }
        }

        protected virtual void Followplayer()
        {
            if (followplayer)
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
                 
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), currentspeed * Time.deltaTime);
                   
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);//new
                    }
                    else
                    {
                        anim.SetBool("run", true);
                    }
                }
                else
                {
                    Vector2 distancetoplayer = (new Vector3(transform.position.x - 2, transform.position.y) - player.transform.position).normalized * mindistance + player.transform.position;
                    //transform.position = Vector2.MoveTowards(transform.position, distancetoplayer, -currentspeed * Time.deltaTime);
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(distancetoplayer.x, transform.position.y), -currentspeed * Time.deltaTime);
                  
                    if (tooclose)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        anim.SetBool("run", false);
                    }
                    else
                    {
                        anim.SetBool("run", true);
                    }

                }
            }

        }

        protected virtual void Movement()
        {
           
            if (!facingleft)
            {

                direction = 1;
                if (collisioncheck(Vector2.right, .5f, collidertoturnaroundon) && turnaroundoncollision  && !spawning || (followplayer && player.transform.position.x < transform.position.x))
                {

                    facingleft = true;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    if (standstill)
                    {
                        //rb.velocity = new Vector2(-jumphorzontalforce, rb.velocity.y);
                    }
                }
            }
            else
            {

                direction = -1;
                if (collisioncheck(Vector2.left, .5f, collidertoturnaroundon) && turnaroundoncollision  && !spawning || (followplayer && player.transform.position.x > transform.position.x))
                {

                    facingleft = false;
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                    if (standstill)
                    {
                        //rb.velocity = new Vector2(jumphorzontalforce, rb.velocity.y);
                    }
                }
            }
           

            acceleration = maxspeed / timetillmaxspeed;

            runtime += Time.deltaTime;
            currentspeed = direction * acceleration * runtime;
            checkspeed();

            if (!followplayer)
            {
                anim.SetBool("run", true);

            }
            

            
            if (groundhit)
            {
                if (!standstill && !followplayer)
                {
                    rb.velocity = new Vector2(currentspeed, rb.velocity.y);

                }
            }
            else
            {
                if (!followplayer)
                {
                    anim.SetBool("run", false);

                }
                rb.velocity = new Vector2(currentspeed*0, rb.velocity.y);
                waitandturn();
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


        protected virtual void waitandturn()
        {
           
            timetowait -= Time.deltaTime;
            if (timetowait <= 0)
            {
                if (facingleft == true)
                {
                    gameObject.transform.localScale = new Vector2(transform.localScale.x*-1, gameObject.transform.localScale.y);
                    facingleft = false;
                }
                else
                {
                    gameObject.transform.localScale = new Vector2(transform.localScale.x*-1, gameObject.transform.localScale.y);
                    facingleft = true;

                }

                timetowait = originalwaittime;
            }



        }


        /// side collisioncheck
        protected virtual bool collisioncheck(Vector2 direction, float distance, LayerMask collison)
        {
            RaycastHit2D[] rayhits = new RaycastHit2D[10];
            int numhit = col.Cast(direction, rayhits, distance);

            for (int i = 0; i < numhit; i++)
            {
                if ((1 << rayhits[i].collider.gameObject.layer & collison) != 0)
                {

                    return true;
                }

            }

            return false;
        }

        //ground check
        protected virtual void checkground()
        {


            bool rightward;
            if (transform.localScale.x > 0)
            {
                rightward = true;
            }
            else
            {
                rightward = false;
            }
          
            if (rightward)
            {
                forwardray.origin = new Vector2(transform.position.x + (transform.localScale.x * .5f) + .05f, transform.position.y + (transform.localScale.y * .5f));

            }
            else
            {
                forwardray.origin = new Vector2(transform.position.x + (transform.localScale.x * .5f) - .05f, transform.position.y + (transform.localScale.y * .5f));

            }
      
            if (rightward)
            {

                groundhit = Physics2D.Raycast(forwardray.origin, -transform.up, .1f,groundlayerlayer);
            }
            else
            {

                groundhit = Physics2D.Raycast(forwardray.origin, -transform.up, .1f,groundlayerlayer);
            }
        }

        protected virtual void shootplayer()
        {
            bulletwait -= Time.deltaTime;
            if (bulletwait <= 0) {
                if (playerisclose)
                {

                    if (!facingleft)
                    {
                        GameObject bullets;
                        bullets = Instantiate(bullettype.projectile, bulletposition.position, Quaternion.identity);
                        bullets.GetComponent<projectile>().left = false;
                        bullets.SetActive(true);
                        for (int i = 0; i < destroybullets.Count; i++)
                        {

                            destroybullets.Add(bullets);
                        }

                    }
                    else
                    {

                        GameObject bullets;
                        bullets = Instantiate(bullettype.projectile, bulletposition.position, Quaternion.identity);
                        bullets.GetComponent<projectile>().left = true;
                        bullets.SetActive(true);
                        destroybullets.Add(bullets);
                    }
                }
                Invoke("bulletdestroy", 5f);
                bulletwait = bulletoriginalwaittime;
            }



        }

        protected virtual void bulletdestroy()
        {


            for(int i = 0; i < destroybullets.Count; i++)
            {

                Destroy(destroybullets[i].gameObject);
            }
            destroybullets.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(new Vector2(transform.position.x + (transform.localScale.x * .5f) - .05f, transform.position.y + (transform.localScale.y * .5f)), .5f);
           
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
