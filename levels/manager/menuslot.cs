using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class menuslot : MonoBehaviour
    {
        public int slotnumber;

        protected titlescreen titlescreen;
        void Start()
        {
            titlescreen = FindObjectOfType<titlescreen>();
        }
        public virtual void newgameslot()
        {

            titlescreen.newGame(slotnumber);
        }
        public virtual void loadgameslot()
        {

            titlescreen.loadGame(slotnumber);
        }
        public virtual void gobacktomainmenu()
        {

            titlescreen.Mainmenu();
        }

    }
}
