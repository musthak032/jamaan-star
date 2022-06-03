using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    
    public class hook : MonoBehaviour
    {
        protected GameObject player;
        protected grabbinghook Grabbinghook;


        [SerializeField] protected LayerMask layers;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            player = GameObject.FindWithTag("Player");
            Grabbinghook = player.GetComponent<grabbinghook>();


        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {

            if ((1 << collision.gameObject.layer & layers)!= 0 && !Grabbinghook.connected)
            {
                Grabbinghook.connected = true;
                Grabbinghook.objectconnectedto = collision.gameObject;
                collision.GetComponent<HingeJoint2D>().enabled = true;
                collision.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();


            }
            
        }
    

    }
}
