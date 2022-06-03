using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class playerdetection :AiManager
    {
        [SerializeField] protected enum detectiontype { rectangle,circle}
        [SerializeField] protected detectiontype type;
        [SerializeField] protected bool followplayerisfound;
        [SerializeField] protected float radius;
        [SerializeField] protected float distance;
        [SerializeField] protected Vector2 detectionoffset;
        [SerializeField] protected LayerMask layer;

        protected virtual void FixedUpdate()
        {
            detectplayer();
        }
        protected virtual void detectplayer()
        {

            RaycastHit2D hit;
            if (type == detectiontype.rectangle)
            {

                if (!enemycharacter.faceleft)
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x + col.bounds.extents.x + detectionoffset.x + (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, layer);


                }
                else
                {
                    hit = Physics2D.BoxCast(new Vector2(transform.position.x - col.bounds.extents.x - detectionoffset.x - (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y + detectionoffset.y), 0, Vector2.zero, 0, layer);

                }
                if (hit)
                {

                    if (followplayerisfound)
                    {
                        enemycharacter.followplayer = true;
                    }
                    enemycharacter.playerisclose = true;
                }
                else
                {
                    enemycharacter.followplayer = false;
                    enemycharacter.playerisclose = false;
                    if (enemeyMovement.standstill)
                    {
                        rb.velocity = Vector2.zero;
                    }
                  
                }
            }
            if (type == detectiontype.circle)
            {
                hit = Physics2D.CircleCast(col.bounds.center, radius, Vector2.zero, 0, layer);
                if (hit)
                {

                    if (followplayerisfound)
                    {
                        enemycharacter.followplayer = true;
                    }
                    enemycharacter.playerisclose = true;
                }
                else
                {
                    enemycharacter.followplayer = false;
                    enemycharacter.playerisclose = false;
                    if (enemeyMovement.standstill)
                    {
                        rb.velocity = Vector2.zero;
                    }
                   
                   
                }

            }
        }

        private void OnDrawGizmos()
        {

            col = GetComponent<Collider2D>();
            if (type == detectiontype.rectangle)
            {
                Gizmos.color = Color.red;
                if (transform.localScale.x > 0)
                {
                    Gizmos.DrawWireCube(new Vector2(transform.position.x + col.bounds.extents.x + detectionoffset.x + (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y));
                }
                else
                {
                    Gizmos.DrawWireCube(new Vector2(transform.position.x - col.bounds.extents.x - detectionoffset.x - (distance * .5f), col.bounds.center.y), new Vector2(distance, col.bounds.size.y));
                }
            }
            if (type == detectiontype.circle)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(col.bounds.center, radius);

            }
        }



    } 
}
