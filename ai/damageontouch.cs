using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class damageontouch : AiManager
    {
        [SerializeField] protected int damageamount;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {

                player.GetComponent<Health>().dealdamage(damageamount);
                if (transform.position.x < player.transform.position.x)
                {

                    player.GetComponent<playerhealth>().left = false;
                }
                else
                {
                    player.GetComponent<playerhealth>().left = true;

                }
            }

            
        }
    }
}
