using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class EnemeyCharacter : MonoBehaviour
    {
        [HideInInspector] public bool faceleft;
        [HideInInspector] public bool followplayer;
        [HideInInspector] public bool playerisclose;
        protected Rigidbody2D rb;
        protected Collider2D col;
        protected EnemeyMovement enemeyMovement;
        protected GameObject player;
        protected Collider2D playercollider;
        protected int rayhitnumber;
        protected float timetilldoaction;

        public float originaltimetilldoaction;
        /// <summary>
        ///  new edith
        /// </summary>
        /// 
    
        
        // Start is called before the first frame update
        void Start()
        {
            Initialization();
        }
        protected virtual void Initialization()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            enemeyMovement = GetComponent<EnemeyMovement>();
           
            player = FindObjectOfType<character>().gameObject;
            playercollider = player.GetComponent<Collider2D>();
            //
         
        }
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
        protected virtual void CheckGround()
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
            Ray2D forwardray = new Ray2D();
            if (rightward)
            {
                forwardray.origin = new Vector2(transform.position.x + (transform.localScale.x * .5f) + .05f, transform.position.y + (transform.localScale.y * .5f));

            }
            else
            {
                forwardray.origin = new Vector2(transform.position.x + (transform.localScale.x * .5f) - .05f, transform.position.y + (transform.localScale.y * .5f));

            }

            Ray2D[] groundrays = new Ray2D[3];
            groundrays[0].origin = new Vector2(transform.position.x - (transform.localScale.x * .5f), transform.position.y - .05f); 
            groundrays[1].origin = new Vector2(transform.position.x , transform.position.y - .05f); 
            groundrays[0].origin = new Vector2(transform.position.x + (transform.localScale.x * .5f), transform.position.y - .05f);
            if (Mathf.Round(transform.localEulerAngles.z) == 90)
            {
                groundrays[0].origin = new Vector2(transform.position.x +.05f, transform.position.y+(transform.localScale.x*.5f));
                groundrays[1].origin = new Vector2(transform.position.x+.05f, transform.position.y);
                groundrays[0].origin = new Vector2(transform.position.x + .05f, transform.position.y - (transform.localScale.x * .5f));
                if (rightward)
                {
                    forwardray.origin = new Vector2(transform.position.x - transform.localScale.x, transform.position.y + (transform.localScale.y * .25f) + .05f);
                }
                else
                {
                    forwardray.origin = new Vector2(transform.position.x + transform.localScale.x, transform.position.y - (transform.localScale.y * .25f) - .05f);
                }

            }
            if (Mathf.Round(transform.localEulerAngles.z) == 180)
            {
                groundrays[0].origin = new Vector2(transform.position.x -(transform.localScale.x*.5f), transform.position.y +.05f);
                groundrays[1].origin = new Vector2(transform.position.x , transform.position.y+.05f);
                groundrays[0].origin = new Vector2(transform.position.x +(transform.localScale.x*.5f), transform.position.y+.05f);
                if (rightward)
                {
                    forwardray.origin = new Vector2(transform.position.x - (transform.localScale.x * .5f) - .05f, transform.position.y - (transform.localScale.y * .5f));

                }
                else
                {
                    forwardray.origin = new Vector2(transform.position.x - (transform.localScale.x * .5f) + .05f, transform.position.y - (transform.localScale.y * .5f));

                }

            }
            if (Mathf.Round(transform.localEulerAngles.z) == 270)
            {
                groundrays[0].origin = new Vector2(transform.position.x - .05f, transform.position.y + (transform.localScale.x * .5f));
                groundrays[1].origin = new Vector2(transform.position.x - .05f, transform.position.y);
                groundrays[0].origin = new Vector2(transform.position.x - .05f, transform.position.y - (transform.localScale.x * .5f));
                if (rightward)
                {
                    forwardray.origin = new Vector2(transform.position.x + transform.localScale.x, transform.position.y - (transform.localScale.y * .25f) - .05f);
                }
                else
                {
                    forwardray.origin = new Vector2(transform.position.x - transform.localScale.x, transform.position.y + (transform.localScale.y * .25f) + .05f);
                }
            }
            RaycastHit2D forwardhits = new RaycastHit2D();
            if (rightward)
            {

                forwardhits = Physics2D.Raycast(forwardray.origin, transform.right, .1f);
            }
            else
            {

                forwardhits = Physics2D.Raycast(forwardray.origin, -transform.right, .1f);
            }
            if (forwardhits && forwardhits.collider.gameObject != player && forwardhits.collider.gameObject.layer != gameObject.layer)
            {
               
                enemeyMovement.turn = true;
                
            }
            else
            {
              
                enemeyMovement.turn = false;
               

            }
            RaycastHit2D[] hits = new RaycastHit2D[3];
            for(int i = 0; i < 3; i++)
            {

                hits[i] = Physics2D.Raycast(groundrays[i].origin, -transform.up, Mathf.Abs(transform.localScale.x * .5f));
            }
            int numberofhit = 0;
            foreach(RaycastHit2D hit in hits)
            {

                if (hit)
                {

                    numberofhit++;
                }
            }
            rayhitnumber = numberofhit;
            
        }

    }
}
