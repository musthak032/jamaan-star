using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class smallenemyhouse : MonoBehaviour
    {   
        [SerializeField] protected float originaltime;
        [SerializeField] protected GameObject greenenemy;
        [SerializeField] protected Transform spawnloaction;
        [SerializeField] protected float spawnburst;
        [SerializeField] protected int spawnamount;
        [SerializeField] protected float originaltimewait;
        protected int number;
        protected float timebetweenspawn;
        protected float wait;
        protected GameObject player;
        public bool facingleft;

       

       
        // Start is called before the first frame update
        protected virtual void Start()
        {
            initialization();
        }

        protected virtual void initialization()
        {

            player = FindObjectOfType<character>().gameObject;
            timebetweenspawn = originaltime;
            wait = originaltimewait;
            number = spawnamount;
            if (transform.localScale.x==1)
            {

                facingleft = true;

            }
            if (transform.localScale.x == -1)
            {
                facingleft = false;

            }



        }

        // Update is called once per frame
       protected virtual void Update()
        {
            spawngreenenemy();
        }

        protected virtual void spawngreenenemy()
        {
            if (number > 0)
            {
                timebetweenspawn -= Time.deltaTime;


                if (timebetweenspawn < 0)
                {
                    GameObject spawnenemy;


                    spawnenemy = Instantiate(greenenemy, spawnloaction.position, Quaternion.identity);
                    number--;

                    if (facingleft)
                    {
                        spawnenemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-spawnburst, spawnenemy.GetComponent<Rigidbody2D>().velocity.y), ForceMode2D.Impulse);


                    }
                    else
                    {
                        spawnenemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(spawnburst, spawnenemy.GetComponent<Rigidbody2D>().velocity.y), ForceMode2D.Impulse);

                    }

                    timebetweenspawn = originaltime;

                }
            }
            if (number <= 0)
            {
                wait -= Time.deltaTime;
                if (wait <= 0)
                {

                    number = spawnamount;
                    wait = originaltimewait;
                }

            }

           


        }


    }
}
