using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class bigmap : manager
    {
        protected virtual void LateUpdate()
        {
            if (!Uimanager.bigmapon)
            {
                transform.position = new Vector3(playerindicator.transform.position.x, playerindicator.transform.position.y, -10);
            }



        }
    }
}
