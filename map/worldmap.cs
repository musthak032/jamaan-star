using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class worldmap : manager
    {
        public Bounds bounds;

        protected virtual void OnDrawGizmos()
        {


            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.localPosition + transform.parent.position, bounds.size);
        }
    }
}
