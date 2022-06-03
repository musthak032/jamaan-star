using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class Health : manager
    {
        public int maxhealthpoints;

        [HideInInspector] public int healthpoints;
        protected override void initialization()
        {
            base.initialization();
            healthpoints = maxhealthpoints;
        }
        public virtual void dealdamage(int amount)
        {

            healthpoints -= amount;
        }
    }
    
}
