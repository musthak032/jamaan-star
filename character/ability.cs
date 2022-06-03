using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class ability : character
    {
        [HideInInspector] public bool dashability; 
        [HideInInspector] public bool walljumpability;
            
        protected character character;
       
        protected override void initialization()
        {
            base.initialization();
            character = GetComponent<character>();
            dashability = PlayerPrefs.GetInt(" " + character.gamefile + "Dashability") == 1 ? true : false;
            walljumpability = PlayerPrefs.GetInt(" " + character.gamefile + "Walljumpability") == 1 ? true : false;
            turnonabilities();
        }
       
        public virtual void Dashability()
        {
            dashability = true;
            dash.enabled = true;
            PlayerPrefs.SetInt(" " + character.gamefile + "Dashability", dashability ? 1 : 0);
           

        } 
        public virtual void Walljumpability()
        {
            jumpscript.walljumpability = true;
            PlayerPrefs.SetInt(" " + character.gamefile + "Walljumpability",jumpscript.walljumpability ? 1 : 0);
          
        }
        public virtual void turnonabilities()
        {
            if (dashability)
            {
                dash.enabled = true;

            }
            if (walljumpability)
            {
                jumpscript.walljumpability = true;
            }

        }
   


    }
}
