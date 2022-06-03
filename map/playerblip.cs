using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class playerblip : manager
    {
        [SerializeField] protected float throbspeed;
        [SerializeField] protected float changespeed;
        [SerializeField] protected float blipsizemultiplier;
        [SerializeField] protected Color bigcolor;
        [SerializeField] protected Color littlecolor;


        protected Vector3 throbsize;
        protected Vector3 originalsize;
        protected SpriteRenderer sprite;

        protected override void initialization()
        {
            base.initialization();
            originalsize = transform.localScale;
            throbsize = new Vector3(transform.localScale.x * blipsizemultiplier, transform.localScale.y * blipsizemultiplier, -10);
            sprite = GetComponent<SpriteRenderer>();
        }
        protected virtual void FixedUpdate()
        {
            throb();

        }
        protected virtual void throb()
        {

            float t = Mathf.Sin((Time.time - Time.deltaTime) * throbspeed) * changespeed;
            transform.localScale = Vector3.Lerp(throbsize, originalsize, t);
            sprite.color = Color.Lerp(bigcolor, littlecolor, t);
        }




    }
}
