using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class itempickup : MonoBehaviour
    {
        public itemtype item;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {


            if (collision.gameObject.tag == "Player")
            {

                collision.gameObject.GetComponent<playersound>().pulicha.Play();
                item.useitem(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
