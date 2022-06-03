using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace metroidvania
{
    public class minimap : manager
    {
        protected GameObject currentscene;
        protected Camera minimapcamera;
        protected float mincamerax;
        protected float mincameray;
        protected float maxcamerax; 
        protected float maxcameray;
        protected override void initialization()
        {
            base.initialization();
            minimapcamera = GetComponent<Camera>();
            currentscene = GameObject.Find(SceneManager.GetActiveScene().name);
            transform.position = currentscene.gameObject.transform.position;
            xmin = currentscene.GetComponent<worldmap>().bounds.min.x+transform.position.x+ transform.localPosition.x;
            ymin = currentscene.GetComponent<worldmap>().bounds.min.y+transform.position.y + transform.localPosition.y;
            xmax = currentscene.GetComponent<worldmap>().bounds.max.x+transform.position.x + transform.localPosition.x;
            ymax = currentscene.GetComponent<worldmap>().bounds.max.y+ transform.position.y + transform.localPosition.y;
            mincamerax = minimapcamera.ViewportToWorldPoint(new Vector2(0, 0)).x;
            mincameray = minimapcamera.ViewportToWorldPoint(new Vector2(0, 0)).y;
            maxcamerax = minimapcamera.ViewportToWorldPoint(new Vector2(1, 1)).x;
            maxcameray = minimapcamera.ViewportToWorldPoint(new Vector2(1, 1)).y;
        }
        protected virtual void LateUpdate()
        {

            transform.position = new Vector3(playerindicator.transform.position.x, playerindicator.transform.position.y, -10);
            transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, xmin - mincamerax, xmax - maxcamerax), Mathf.Clamp(transform.localPosition.y, ymin - mincameray, ymax - maxcameray), - 10);

        }
    }
}
