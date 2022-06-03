using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class enemyhealth : Health
    {
        [SerializeField] protected float revivetime;
        private GameObject slider;
        [SerializeField] LayerMask enemey;
      
        protected override void initialization()
        {
            base.initialization();
            if (gameObject.tag == "target")
            {
                slider = GetComponentInChildren<enemyhealthbar>().gameObject;
                //
                slider.SetActive(false);
            }
        }
        public override void dealdamage(int amount)
        {
            base.dealdamage(amount);
            int ran = Random.Range(0, 6);
            if (healthpoints <= 0 && gameObject.GetComponent<EnemeyCharacter>())
            {
               
                if (ran <= 3)
                {
                    player.GetComponent<playersound>().savu.Play();
                }
                else
                {
                    player.GetComponent<playersound>().savumla.Play();
                }
               //audio
                if (gameObject.GetComponent<randomdrops>())
                {
                  
                    gameObject.GetComponent<randomdrops>().roll();
                }
                gameObject.SetActive(false);
                Invoke("revie", revivetime);
            }
            if (healthpoints <= 0 && (gameObject.GetComponent<beanenemy>()||gameObject.GetComponent<smallenemyhouse>()))
            {
                if (ran <= 3)
                {
                    player.GetComponent<playersound>().savu.Play();
                }
                else
                {
                    player.GetComponent<playersound>().savumla.Play();
                }
               //audio
                if (gameObject.GetComponent<randomdrops>())
                {
                  
                    gameObject.GetComponent<randomdrops>().roll();
                }
                gameObject.SetActive(false);
                Invoke("revie", revivetime);
            }
            if (healthpoints <= 0&&gameObject.tag=="explosivetag")
            {
               
                gameObject.SetActive(false);
                Invoke("revies", revivetime+2f);

            }
            if (gameObject.tag == "target")
            {
                slider.SetActive(true); //new
                Invoke("offslider", 10f);
            }

        }
        protected virtual void revie()
        {
            gameObject.GetComponent<Health>().healthpoints += 100;
            gameObject.SetActive(true);
        }
        protected virtual void revies()
        {
            gameObject.GetComponent<Health>().healthpoints += 10;
            gameObject.SetActive(true);
        }

        protected virtual void offslider()
        {
            slider.SetActive(false);

        }
    }
}
