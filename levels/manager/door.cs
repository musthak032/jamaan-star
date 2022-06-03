using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class door : manager
    {
        [SerializeField] protected string[] tagtoopen;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            for(int i = 0; i < tagtoopen.Length; i++)
            {
                if (collision.gameObject.tag == tagtoopen[i])
                {

                    GetComponent<Collider2D>().enabled = false;
                    GetComponent<Animator>().SetBool("open", true);
                }

            }

        }
    }
}
