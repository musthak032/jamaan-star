using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class mobileinput :MonoBehaviour
    {
        inputmanager inputs;
        horizontalmove horizontal;
        public bool mobmoveleft;
        GameObject player;
        bool mapon;
    
        public bool mobmoveright;

        protected findplayer findplayer; //for ui dispalyweapon
        protected weapon currentprojectile;

    
        // Start is called before the first frame update
        private void Awake()
        {
            

        }
        void Start()
        {

            player = FindObjectOfType<character>().gameObject;
            findplayer = FindObjectOfType<findplayer>().gameObject.GetComponent<findplayer>();

            inputs = player.GetComponent<inputmanager>();
            horizontal = player.GetComponent<horizontalmove>();
            currentprojectile = GetComponent<weapon>();
            //new
           
            //dashbutton.SetActive(false);
         
        }

        // Update is called once per frame
        void Update()
        {
            //dashuionoff();
        }
      
       
        public void crouch()
        {
           
          
            if (inputs.mobilecrouch)
            {
                inputs.mobilecrouch = false;
            }
            else
            {
                inputs.mobilecrouch = true;
            }
           
        }
        public void dashm()
        {

            if (!inputs.dashmo)
            {
                inputs.dashmo = true;
                Invoke("canceldashbool", .1f);
            }
           

        }
        protected virtual void canceldashbool()
        {
            inputs.dashmo = false;

        }
        public void jumpm()
        {
           
            
                inputs.jumpmo = true;
                
           
           
        }
        public void jumpholdm()
        {
           
            {
                inputs.jumpholdmo = true;
            }


        }
        public void jumpnotholdm()
        {
           
            {
                inputs.jumpholdmo = false;
            }


        }
        public void leftmove()
        {

            mobmoveleft = true;


        }

        public void notleftmove()
        {
            mobmoveleft = false;

        }
        public void rightmove()
        {

          
              
                mobmoveright = true;
          

            
        }
        public void notrightmove()
        {
            
                mobmoveright = false;
            


        }
        public void bigmaponmob()
        {
            inputs.bigmapon = true;
            Invoke("bigmapoffmob", .0001f);
        } 
        public void bigmapoffmob()
        {
            inputs.bigmapon = false;
        }
            
        public void clickfired()
        {

            inputs.firenotheld = true;
            Invoke("offclick", 0);
        } 
        void offclick()
        {
            inputs.firenotheld = false;
        }
        public void fired()
        {

            inputs.fireheld = true;
        }
        public void notfired()
        {

            inputs.fireheld = false;
        }
        public void changeweapon()
        {
            inputs.changeweapon = true;
           
            Invoke("offchangeweapon", .01f);
        }
        public void offchangeweapon()
        {
            inputs.changeweapon = false;
        }

        public void upheldweapon()
        {

            inputs.weaponup = true;

        }

        public void upledge() //new
        {
            if (player.GetComponent<character>().grabbingLedge)
            {
                inputs.ledgeup= true;
                Invoke("offledge", 6f);
                
            }
        }

        protected virtual void offledge() //new
        {

            inputs.ledgeup = false;
        }
        public void offupheldweapon()
        {

            inputs.weaponup = false;

        }
        
        public void downheldweapon()
        {

            inputs.weapondown = true;
        }
        public void offdownheldweapon()
        {

            inputs.weapondown = false;
        }
        public void laderup()           //forleder input
        {

            inputs.laderup = true;

        } 
        public void laderupoff()           //forleder input
        {

            inputs.laderup = false;

        }
        public void laderdownon()           //forleder input
        {
            inputs.laderdown = true;


        } 
        public void laderdownoff()           //forleder input
        {

            inputs.laderdown = false;

        }
        public void grabupon()
        {

            inputs.grabup = true;
        }
        public void grabupoff()
        {
            inputs.grabup = false;

        }
        public void grabdownon()
        {
            inputs.grabdown = true;

        }
        public void grabdownoff()
        {

            inputs.grabdown = false;
        }
        public void onMapleft()
        {
            inputs.mapleft = true;

        } 
        public void offMapleft()
        {
            inputs.mapleft = false;
        }
        public void onMapright()
        {
            inputs.mapright = true;
        }
        public void offMapright()
        {
            inputs.mapright = false;
        }
    }
}
