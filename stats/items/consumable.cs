using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    [CreateAssetMenu(fileName = "itemtype", menuName = "metroidvania/items/consumable/consumableitem", order = 2)]
   
    public class consumable : itemtype
    {
        public string itemname;
        public GameObject prefarb;
        public override void useitem(GameObject player)
        {
            base.useitem(player);
        }
    }
}
