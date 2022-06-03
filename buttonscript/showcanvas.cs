using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class showcanvas : MonoBehaviour
    {
        GameObject player;
        GameObject dashbutton;
        bool find=false;
        void Start()
        {
            player = FindObjectOfType<character>().gameObject;
          
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        protected virtual void showoffdash()
        {
           
            if (!find)
            {
                dashbutton = FindObjectOfType<dashfinder>().gameObject;
                find = true;
            }
            if (player.GetComponent<dash>().enabled == true)
            {

               dashbutton.SetActive(true);
            }
            else
            {
                dashbutton.SetActive(false);
            }
        }
    }
}
