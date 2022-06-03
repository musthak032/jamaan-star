using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    //moving and placing projectile
    public class projectile : MonoBehaviour
    {
        //what type of weapon

      [SerializeField]  protected weapontype weapon;
        
        [SerializeField] protected int damageamount;
        [SerializeField] protected LayerMask damagelayers;   
        [SerializeField] protected LayerMask groundlayers; 
        [SerializeField] protected LayerMask explosivelayers;

        //audio
       
        //bool for for
        public bool fired;
        //bool for player facing left or right
        public bool left;
        public float projectilelifetime;
        //player face rigth player fce left
        private bool flipped;
        // Start is called before the first frame update
        //
        [SerializeField] protected GameObject[] explosives;
        protected GameObject hideexplosive;
        //
        protected virtual void OnEnable()
        {
            projectilelifetime = weapon.lifetime;
        }
        // Update is called once per frame
        protected virtual void FixedUpdate()
        {
            movement();
        }
        public virtual void movement()
        {
            if (fired) 
            {
                projectilelifetime -= Time.deltaTime;
                if (projectilelifetime > 0)
                {
                    if (gameObject.tag == "grabbinghook")
                    {

                        transform.parent = GameObject.FindWithTag("Player").transform;
                        transform.Translate(Vector2.right * weapon.projectilespeed * Time.deltaTime);
                        return;
                    }
                    if (!left)
                    {
                        if (flipped) 
                        {
                            flipped = false;
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                        transform.Translate(Vector2.right * weapon.projectilespeed * Time.deltaTime);
                    }
                    else
                    {
                        if (!flipped)
                        {
                            flipped = true;
                            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                        }
                        transform.Translate(Vector2.left * weapon.projectilespeed * Time.deltaTime);
                    }
                }
            }
            else 
            {
                destroyprojectile();
            }
            if (projectilelifetime < 0) // own code for bullet hide
            {
                destroyprojectile();

            }
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
           
            if ((1<<collision.gameObject.layer & groundlayers) != 0)
            {
                destroyprojectile();

            }
            if ((1 << collision.gameObject.layer & explosivelayers) != 0)
            {
                if (collision.gameObject.GetComponents<Health>() != null)
                {
                  hideexplosive  =Instantiate(explosives[0].gameObject, transform.position, Quaternion.identity);
                    collision.gameObject.GetComponent<Health>().dealdamage(damageamount);
                    Invoke("Hideexplosive",.001f);
                }

            }

            if ((1 << collision.gameObject.layer & damagelayers) != 0)
            {
              
                if (collision.gameObject.GetComponents<Health>() !=null)
                {

                    collision.gameObject.GetComponent<Health>().dealdamage(damageamount);
                   
                }
                if (collision.gameObject.tag == "Player")
                {
                    if (collision.transform.position.x < transform.position.x)
                    {
                        collision.gameObject.GetComponent<playerhealth>().left = true;

                    }
                    else
                    {
                        collision.gameObject.GetComponent<playerhealth>().left = false;

                    }

                   
                }
                destroyprojectile();
            }
           

        }
        public virtual void destroyprojectile()
        {

            projectilelifetime = weapon.lifetime;
            gameObject.SetActive(false);
        }
        protected virtual void Hideexplosive()
        {
            Destroy(hideexplosive,1f);
        }
    }
}
