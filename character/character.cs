using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    
    public class character : MonoBehaviour
    {
        protected Animator anim;
        protected Rigidbody2D rb;
        protected Collider2D col;
        [HideInInspector] public bool isfaceleft;
        [HideInInspector]public bool isground;
        ///crouchin
        [HideInInspector] public bool iscrouching;
        Vector2 faceleft;
        //das
        [HideInInspector] public bool isdashing;
        [HideInInspector] public bool isjump;

        [HideInInspector] public bool isjumpingthroughplateform;
        protected horizontalmove movement;
        //wallsliding

        protected jump jumpscript;
        [HideInInspector] public bool iswallsliding;
        [HideInInspector] public bool isonladder;
        [HideInInspector] public bool isswimming;
        [HideInInspector] public bool isdead;

         [HideInInspector] public bool grabbingLedge;
         [HideInInspector] public int gamefile;
                                      
        /// input mangerscript
        protected inputmanager input;
        //objectpool

        protected objectpooler Objectpooler;
        protected aimmanager aimmanger;
        protected weapon Weapon;
        protected grabbinghook grabbinghook;
        protected dash dash;
        protected GameObject currentflateform;
        protected gamemanager Gamemanager;
        protected GameObject player;
        protected mobileinput mobinput;

        // Start is called before the first frame update
        void Start()
        {
            initialization();
            
        }

        // Update is called once per frame
       protected virtual void initialization()
        {
            gamefile = PlayerPrefs.GetInt("gamefile");
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<Collider2D>();
            anim = GetComponent<Animator>();
            movement = GetComponent<horizontalmove>();
            jumpscript = GetComponent<jump>();
            faceleft = new Vector2(-transform.localScale.x, transform.localScale.y);
            input = GetComponent<inputmanager>();
            mobinput =GetComponent<mobileinput>();
           
            Objectpooler = objectpooler.Instance;
            aimmanger = GetComponent<aimmanager>();
            Weapon = GetComponent<weapon>();
            grabbinghook = GetComponent<grabbinghook>();
            dash = GetComponent<dash>();
            Gamemanager = FindObjectOfType<gamemanager>();



        }
        protected virtual void flip()
        {

            if (isfaceleft||(!isfaceleft && iswallsliding))
            {
                transform.localScale = faceleft;
            }
            if(!isfaceleft||(isfaceleft && iswallsliding))
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        protected virtual bool collisioncheck(Vector2 direction,float distance,LayerMask collison)
        {
            RaycastHit2D[] rayhits = new RaycastHit2D[10];
            int numhit = col.Cast(direction, rayhits,distance);
          
            for (int i = 0; i<numhit; i++)
            {
                if ((1 << rayhits[i].collider.gameObject.layer & collison) != 0)
                {
                    currentflateform = rayhits[i].collider.gameObject;
                    return true;
                }
               
            }

            return false;
        }
        public virtual bool falling(float velocity)
        {
            if (!isground && rb.velocity.y<velocity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected virtual void fallspeed(float speed)
        {
            rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * speed));
        }
        public void initializeplayer()
        {

            player = FindObjectOfType<character>().gameObject;
            player.GetComponent<character>().isfaceleft = PlayerPrefs.GetInt(" "+ gamefile +"facingleft") == 1 ? true : false;
            if (player.GetComponent<character>().isfaceleft)
            {
                player.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

    }

  
}
