using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class meeleattack : AiManager
    {
        [SerializeField] protected bool hitplayerwhenclose;
        [SerializeField] protected int damageamount;
        protected Collider2D swipecollider;
        protected Animator anim;
        protected GameObject swipe;
        protected playerhealth playerhealth;
        protected bool hit;

        protected override void Initialization()
        {
            base.Initialization();
            swipe = transform.GetChild(0).gameObject;
            swipecollider = swipe.GetComponent<Collider2D>();
            anim = swipe.GetComponent<Animator>();
            playerhealth = player.GetComponent<playerhealth>();
            swipe.SetActive(false);
        }

        protected virtual void FixedUpdate()
        {

            hitplayer();
        }
        protected virtual void hitplayer()
        {

            if (hitplayerwhenclose && !enemycharacter.playerisclose)
            {

                return;
            }
            timetilldoaction -= Time.deltaTime;
            if (timetilldoaction < 0)
            {
                swipe.SetActive(true);
                anim.SetBool("attack", true);
                timetilldoaction = originaltimetilldoaction;
                if (hit)
                {
                    hit = false;
                }
            }
            Invoke("cancelswipe", anim.GetCurrentAnimatorStateInfo(0).length);
        }
        protected virtual void dealdamage()
        {
            if (hit)
            {
                if (player.transform.position.x < transform.position.x)
                {

                    player.GetComponent<playerhealth>().left = false;
                }
                else
                {
                    player.GetComponent<playerhealth>().left = true;

                }
                playerhealth.dealdamage(damageamount);
                //hit = false;
            }


        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject == player && !hit)
            {


                 hit = true;

                dealdamage();
            }
        }
        protected virtual void cancelswipe()
        {

            anim.SetBool("attack", false);
            swipe.SetActive(false);
        }
    }
}
