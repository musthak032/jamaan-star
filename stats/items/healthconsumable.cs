using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    [CreateAssetMenu(fileName = "itemtype", menuName = "metroidvania/items/consumable/healthconsumable", order = 2)]

    public class healthconsumable : consumable
    {
        public int amount;
        public override void useitem(GameObject player)
        {
            player.GetComponent<playerhealth>().gaincurrenthealth(amount);
            base.useitem(player);
        }
    }
}
