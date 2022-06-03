using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class camerafollow : manager
    {
        [SerializeField] protected float xadjustment;
        [SerializeField] protected float yadjustment;
        [SerializeField] protected float zadjustment;
        [SerializeField] protected float tvalue;


        private float originaladYjustment;
        private bool falling;

        protected float halfcamerax;
        protected float halfcameray;


        protected override void initialization()
        {
            base.initialization();
            originaladYjustment = yadjustment;         //original y adjustment
            halfcamerax = GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).x;
            halfcameray = GetComponent<Camera>().ViewportToWorldPoint(new Vector2(0, 0)).y;
        }
        // Start is called before the first frame update
        protected virtual void FixedUpdate()
        {

            followplayer();
          

        }
        protected virtual void followplayer()
        {
            if (character.isjump)
            {

                float newadjustment = originaladYjustment;
                newadjustment += 5;
                yadjustment = newadjustment;
            }
            if (!character.isjump && !character.falling(0))
            {
                yadjustment = originaladYjustment;

            }
            if (character.falling(-5) && !falling)
            {
                falling = true;
                yadjustment *= -1;

            }
            if (!character.falling(0) && falling)
            {
                falling = false;
                yadjustment *= -1;

            }
            if (!character.isfaceleft)
            {
                transform.position = Vector3.Lerp(new Vector3(player.transform.position.x + xadjustment, player.transform.position.y + yadjustment, player.transform.position.z - zadjustment), transform.position, tvalue);
            }
            else
            {
                transform.position = Vector3.Lerp(new Vector3(player.transform.position.x - xadjustment, player.transform.position.y + yadjustment, player.transform.position.z - zadjustment), transform.position, tvalue);
            }
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, xmin - halfcamerax, xmax + halfcamerax), Mathf.Clamp(transform.position.y, ymin - halfcameray, ymax + halfcameray),-zadjustment);
            
        }
    }
}
