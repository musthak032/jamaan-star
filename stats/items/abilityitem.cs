using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace metroidvania
{
    [CreateAssetMenu(fileName = "itemtype", menuName = "metroidvania/items/consumable/ability", order = 2)]

    public class abilityitem : consumable
    {
      
        public override void useitem(GameObject player)
        {
         
            base.useitem(player);
            player.GetComponent<ability>().Invoke(itemname, 0);
        }
    }
}
