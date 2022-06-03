using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace metroidvania
{
    public class findplayer : MonoBehaviour
    {
        GameObject player;
        mobileinput mob;
        public GameObject mobilecontrol;
        protected weapon currentprojectile;
        [HideInInspector]  public GameObject yellowbulletimage;
        [HideInInspector] public GameObject redbulletimage;
        [HideInInspector] public GameObject hookbulletimage;
        protected GameObject rightfullbutton;
        GameObject dashbutton;
        protected virtual void  Awake()
        {
       
        }

        // Start is called before the first frame update
       protected virtual void  Start()
        {

            player = FindObjectOfType<character>().gameObject;
            mob = player.GetComponent<mobileinput>();
            mobilecontrol = FindObjectOfType<showcanvas>().gameObject;
            currentprojectile = player.GetComponent<weapon>();
            yellowbulletimage = FindObjectOfType<yellowbulletsfinder>().gameObject;
            redbulletimage = FindObjectOfType<redbulletsfinder>().gameObject; 
            hookbulletimage = FindObjectOfType<hookbulletsfinder>().gameObject;
            rightfullbutton = FindObjectOfType<rightsidebuttonfinder>().gameObject;
            dashbutton = FindObjectOfType<dashfinder>().gameObject; //new
            mobilecontrol.SetActive(false);
            Invoke("show", .1f);
            



        }
        protected virtual void Update()
        {
            if ((rightfullbutton.activeSelf==true))
            {

                weaponshowui();
                if (player.GetComponent<dash>().enabled == true) //new
                {
                    dashbutton.SetActive(true);
                }
                else
                {
                    dashbutton.SetActive(false);
                }
            }
        }
        public virtual void weaponshowui()
        {

            if (currentprojectile.currentprojectile.gameObject.name == "baseprojectile")
            {
                yellowbulletimage.SetActive(true);
                redbulletimage.SetActive(false);
                hookbulletimage.SetActive(false);
              

            }
            if (currentprojectile.currentprojectile.gameObject.name == "baseprojectile 1")      //weaponuishow
            {
                yellowbulletimage.SetActive(false);
                redbulletimage.SetActive(true);
                hookbulletimage.SetActive(false);
               
            }
            if (currentprojectile.currentprojectile.gameObject.name == "grabbinghookporojectile")
            {
                yellowbulletimage.SetActive(false);
                redbulletimage.SetActive(false);
                hookbulletimage.SetActive(true);
               
            }



        }

        void show()
        {

            mobilecontrol.SetActive(true);
            if (rightfullbutton.activeSelf == true)
            {
                yellowbulletimage.SetActive(true);
                redbulletimage.SetActive(false);
                hookbulletimage.SetActive(false);
            }
        }

        // Update is called once per frame
      public void right()
        {

            mob.rightmove();
        }
        public void notright()
        {

            mob.notrightmove();
        }

        public void leftmove()
        {

            mob.leftmove();
        } 
        public void notleftmove()
        {

            mob.notleftmove();
        }
        public void crouch()
        {

            mob.crouch();
        }
        public void dashm()
        {

            mob.dashm();
        } 
        public void jumpm()
        {

            mob.jumpm();
        } 
        public void jumpholdm()
        {

            mob.jumpholdm();
        }
        public void jumpnotholdm()
        {

            mob.jumpnotholdm();
        }
        public void bigmaponheld()
        {

            mob.bigmaponmob();
        } 
        public void bigmapoffheld()
        {

            mob.bigmapoffmob();
        } 
    
       
        public void clickfiredon()
        {

            mob.clickfired();
        }  
        public void firedon()
        {

            mob.fired();
        }
        public void firedoff()
        {

            mob.notfired();
        }
        public void changeweapon()
        {
            mob.changeweapon();
           

        } 
       public void upfire()
        {

            mob.upheldweapon();
        }
        public void offupfire()
        {

            mob.offupheldweapon();
        }
         public void downfire()
        {
            mob.downheldweapon();
        }
        public void offdownfire()
        {
            mob.offdownheldweapon();
        }
        public void laderupon()
        {
            mob.laderup();

        }
        public void laderupoff()
        {

            mob.laderupoff();
        }
        public void laderdowmon()
        {
            mob.laderdownon();

        }
        public void laderdowmoff()
        {

            mob.laderdownoff();
        }

        public void grabupon()
        {

            mob.grabupon();
        }
        public void grabupoff()
        {
            mob.grabupoff();

        }
        public void grabdownon()
        {

            mob.grabdownon();
        }

        public void grabdownoff()
        {
            mob.grabdownoff();

        }
        public void onmapleft()
        {
            mob.onMapleft();
        }
        public void offmapleft()
        {
            mob.offMapleft();

        }
        public void onmapright()
        {
            mob.onMapright();
        }
        public void offmapright()
        {
            mob.offMapright();
        }

        public void UPledge() //new
        {

            mob.upledge();
        }


    }
}
