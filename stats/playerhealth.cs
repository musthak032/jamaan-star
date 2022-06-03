using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace metroidvania
{
    public class playerhealth : Health
    {
        [SerializeField] protected float iframetime;
        [SerializeField] protected float verticaldamageforce;
        [SerializeField] protected float horizontaldamageforce;
        [SerializeField] protected float slowdowntimeamount;
        [SerializeField] protected float slowdownspeed;

        protected SpriteRenderer[] sprites;
        protected Rigidbody2D rb;
        protected Image deadscreenimage;
        protected Text deadscreentext;
        
        [HideInInspector] public bool invulnerable;
        [HideInInspector] public bool hit;
        [HideInInspector] public bool left;

        protected float originaltimescale;

        protected override void initialization()
        {
            base.initialization();
            sprites = GetComponentsInChildren<SpriteRenderer>();
            deadscreenimage = Uimanager.deadscreen.GetComponent<Image>();
            deadscreentext = Uimanager.deadscreen.GetComponentInChildren<Text>();
            rb = GetComponent<Rigidbody2D>();
        }
        protected virtual void FixedUpdate()
        {

            handleiframe();
            handledamagemovement();
        }
        public override void dealdamage(int amount)
        {
            if (!character.isdead) 
            {
                if (invulnerable || character.isdashing)
                {

                    return;
                }
                base.dealdamage(amount);
                if (healthpoints <= 0)
                {
                    character.isdead = true;
                    healthpoints = 0;
                    player.GetComponent<Animator>().SetBool("dying", true);
                    StartCoroutine(dead());
                }
                originaltimescale = Time.timeScale;
                hit = true;
                invulnerable = true;
                Invoke("cancel", iframetime);
            }

        }

        public virtual void handledamagemovement()
        {
            if (hit)
            {
                Time.timeScale = slowdownspeed;
                rb.AddForce(Vector2.up * verticaldamageforce);
                if (!left)
                {
                    rb.AddForce(Vector2.right * horizontaldamageforce);
                }
                else
                {
                    rb.AddForce(Vector2.left * horizontaldamageforce);

                }
                Invoke("hitcancel", slowdowntimeamount);
            }


        }
        protected virtual void handleiframe()
        {

            Color spritecolors = new Color();
            if (invulnerable)
            {

                foreach (SpriteRenderer sprite in sprites)
                {

                    spritecolors = sprite.color;
                    spritecolors.a = .5f;
                    sprite.color = spritecolors;
                }
            }
            else
            {
                foreach (SpriteRenderer sprite in sprites)
                {

                    spritecolors = sprite.color;
                    spritecolors.a = 1;
                    sprite.color = spritecolors;
                }

            }
        }

        protected virtual void cancel()
        {
            invulnerable = false;

        }

        protected virtual void hitcancel()
        {

            hit = false;
            Time.timeScale = originaltimescale;
        }
        public virtual void gaincurrenthealth(int amount)
        {
            healthpoints += amount;
            if (healthpoints > maxhealthpoints)
            {

                healthpoints = maxhealthpoints;
            }


        }
        protected virtual IEnumerator dead()
        {
            Uimanager.deadscreen.SetActive(true);
            Uimanager.leftfullbutton.SetActive(false);
            Uimanager.rightfullbutton.SetActive(false);
            float timestarted = Time.time;
            float timesincestarted = Time.time - timestarted;
            float percentagecomplete = timesincestarted / 2f;
            Color currentcolor = deadscreenimage.color;
            Color currenttextcolor = deadscreentext.color;
            Color spritecolor = new Color();
            foreach (SpriteRenderer sprite in sprites)
            {

                spritecolor = sprite.color;
            }
            while (true)
            {
                timesincestarted = Time.time - timestarted;
                percentagecomplete = timesincestarted / 2f;
                currentcolor.a = Mathf.Lerp(0, 1, percentagecomplete);
                deadscreenimage.color = currentcolor;        
                currenttextcolor.a = Mathf.Lerp(0, 1, percentagecomplete);
                deadscreentext.color = currenttextcolor;
                foreach (SpriteRenderer sprite in sprites)
                {
                    spritecolor.a = Mathf.Lerp(0, 1, percentagecomplete);
                    sprite.color =spritecolor;

                }
                    if (percentagecomplete >= 1)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
            Invoke("loadgame", 2);
        }
        public virtual void loadgame()
        {

            levelmanager.loadfromsave = true;
            PlayerPrefs.SetInt(" " +character.gamefile + "loadfromsave", levelmanager.loadfromsave ? 1 : 0);
            string scene = PlayerPrefs.GetString(" " + character.gamefile + "loadgame");
            SceneManager.LoadScene(scene);
        }
    }
   
}
