using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    [CreateAssetMenu(fileName ="weapontype",menuName ="metroidvania/weapon",order =1)]
    public class weapontype : ScriptableObject
    {
        
        public GameObject projectile;
        public float projectilespeed;
        public int amounttopool;
        //life time of bullet
        public float lifetime;
        // automatic bool
        public bool automatic;
        public float timebetweenshot;

        public bool canexpandpool;  //extra bullet
        public bool canresetpool;   //relooadbullet
        // Start is called before the first frame update
      
    
        protected virtual void OnEnable()
        {
            if (canexpandpool && canresetpool)
            {
                canresetpool = false;
            }
          
        }

    }
}
