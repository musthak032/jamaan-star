using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    [CreateAssetMenu(fileName ="coin",menuName ="metroidvania/coin",order =1)]
    public class coinitem : ScriptableObject
    {
        public string itemname;
        public GameObject coin;
       public void destroyobject(GameObject currentcoin)
        {

            Destroy(currentcoin.gameObject,.1f);
        }
    }
}
