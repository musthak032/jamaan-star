using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    [CreateAssetMenu(fileName ="itemtype",menuName ="metroidvania/items/nullitem",order =2)]
    //[CreateAssetMenu(fileName = "weapontype", menuName = "metroidvania/weapon", order = 1)]
    public class itemtype : ScriptableObject
    {
        // Start is called before the first frame update
       public virtual void useitem(GameObject player)
        {

        }
    }
}
