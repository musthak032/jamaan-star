using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class abilitypickup : MonoBehaviour
    {
        public itemtype item;
        protected bool found;

        protected GameObject player;
        bool dash;
            bool walljump;
        protected virtual void Awake()
        {
            player = FindObjectOfType<character>().gameObject;
            dash = PlayerPrefs.GetInt(" " + PlayerPrefs.GetInt("gamefile") + "Dashability") == 1 ? true : false;
            walljump = PlayerPrefs.GetInt(" " + PlayerPrefs.GetInt("gamefile") + "Walljumpability") == 1 ? true : false;

        }
        protected virtual void Start()
        {
   
        }
        private void OnEnable()
        {
            found = PlayerPrefs.GetInt(" "+PlayerPrefs.GetInt("gamefile") + item)==1?true:false;


            if (found)
            {

                if (!dash && !walljump)
                {
                    found = false;
                    PlayerPrefs.SetInt(" " + PlayerPrefs.GetInt("gamefile") + item, found ? 1 : 0);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            
            
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {


            if (collision.gameObject.tag == "Player")
            {


                item.useitem(collision.gameObject);
                found = true;
                PlayerPrefs.SetInt(" " + PlayerPrefs.GetInt("gamefile") + item, found ? 1 : 0);
                Destroy(gameObject);
            }
        }
    }
}

