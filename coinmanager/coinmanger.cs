using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class coinmanger : MonoBehaviour
    {
        int coin;
       public bool found;

    
       


        [Header("value")] [SerializeField] protected int coinvalue;
        [SerializeField] protected coinitem coinitem;

        protected GameObject coindata;
        // Start is called before the first frame update
        void Start()
        {

            coindata = FindObjectOfType<coindata>().gameObject;

            found = PlayerPrefs.GetInt("coinfound") == 1 ? true : false;
            if (found)
            {


                coindata.GetComponent<coindata>().load();

                for(int i = 0; i < coindata.GetComponent<coindata>().currentcoinname.Count; i++)
                {

                    if (coindata.GetComponent<coindata>().currentcoinname[i] == gameObject.name)
                    {

                        Destroy(gameObject);
                    }


                }
                
                

              
            }

        }
        protected virtual void offfound()
        {

            found = false;
            PlayerPrefs.SetInt("coinfound", found ? 1 : 0);
        }

        private void OnEnable()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

        }
       
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.tag == "Player")
            {

                coin = PlayerPrefs.GetInt("coin");
                coin += coinvalue;
                PlayerPrefs.SetInt("coin", coin);


                coindata.GetComponent<coindata>().addstring(gameObject);
                   
               

                coinitem.destroyobject(gameObject);
                collision.gameObject.GetComponent<playersound>().coinsound.Play();
            }
        }
       
   
    }
}
