using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class explosivedamage : MonoBehaviour
    {

        [SerializeField] protected LayerMask damagelayers;
        [SerializeField] protected int damageamount;

        [SerializeField] int rand;
        [SerializeField] int force;
      
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & damagelayers) != 0)
            {

                if (collision.gameObject.GetComponents<Health>() != null)
                {
                       int flow  =Random.Range(100, rand);
                    int flow1 = Random.Range(-rand, rand);
                    collision.gameObject.GetComponent<Health>().dealdamage(damageamount);
                  
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(flow1, flow) * force * Time.deltaTime, ForceMode2D.Force);
                }
                if (collision.gameObject.tag == "Player")
                {
                    if (collision.transform.position.x < transform.position.x)
                    {
                        collision.gameObject.GetComponent<playerhealth>().left = true;
                        int flow = Random.Range(100, rand);
                        int flow1 = Random.Range(100,rand);
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-flow1, flow)* force * Time.deltaTime, ForceMode2D.Force);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<playerhealth>().left = false;

                        int flow = Random.Range(100, rand);
                        int flow1 = Random.Range(-rand, rand);
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(flow1, flow)*force*Time.deltaTime, ForceMode2D.Force);
                    }


                }

            }

        }
        
    }
}
