using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class fogofwar : manager
    {
        protected override void initialization()
        {
            base.initialization();
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<playerindicatormovement>())
            {

                levelmanager.removefog(this);
            }
            
        }
    }
}
