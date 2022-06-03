using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class playerindicatormovement : manager
    {
        protected Vector3 previousposition;
        protected Vector3 relativeposition;
        protected Transform origin;
        protected override void initialization()
        {
            base.initialization();
            origin = levelmanager.playerindicatespawnlocation[PlayerPrefs.GetInt(" "+character.gamefile+"spawnreference")];
            relativeposition = player.transform.position * -.1f;
        }
        protected virtual void LateUpdate()
        {

            Vector3 currentposition = player.transform.position;
            if (currentposition != previousposition)
            {
                transform.position = getrelativeposition(origin, player.transform.position * -.1f);
                transform.position = new Vector2(Mathf.Abs(transform.position.x - relativeposition.x), -Mathf.Abs(transform.position.y - relativeposition.y));
            }
            previousposition = currentposition;
        }
        protected virtual Vector3 getrelativeposition(Transform origin,Vector3 position)
        {
            Vector3 distance = position - origin.position;
            Vector3 relativep = Vector3.zero;
            relativep.x = Vector3.Dot(distance, origin.right.normalized);
            relativep.y = Vector3.Dot(distance, origin.up.normalized);
            relativep.z = Vector3.Dot(distance, origin.forward.normalized);
            return relativep;
           
        }
    }
}
