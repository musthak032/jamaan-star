using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class inputmanager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] protected KeyCode Crouchheld;
        [SerializeField] protected KeyCode Dashpress;
        [SerializeField] protected KeyCode Sprintheld;
        [SerializeField] protected KeyCode Jumpheld;
        [SerializeField] protected KeyCode weaponfire;
        [SerializeField] protected KeyCode upheld;     // use for fire in w diection 
        [SerializeField] protected KeyCode downheld;   // use for fire in s diection 
        [SerializeField] protected KeyCode tiltedupheld;// use for fire in e diection
        [SerializeField] protected KeyCode tilteddownheld;// use for fire in q diection
        [SerializeField] protected KeyCode aimingheld;
        [SerializeField] protected KeyCode changeweaponpress; 
        [SerializeField] protected KeyCode bigmappressed;
        [SerializeField] protected KeyCode LeftHeld;
        [SerializeField] protected KeyCode RightHeld;
        // audio

      
        //mobile
        public bool mobilecrouch;
        public bool dashmo;
        public bool jumpmo;
        public bool jumpholdmo;
        public bool bigmapon;
        public bool fireheld; 
        public bool firenotheld;
        public bool changeweapon;
        public bool weaponup;
        public bool weapondown;
        public bool laderup;//forlederinput
        public bool laderdown;
        public bool grabup;//forgrabinput  
        public bool grabdown;//forgrabinput
        public bool mapright;
        public bool mapleft;
        public bool ledgeup;

        // Update is called once per frame
        void Update()
        {
            crouchheld();
            dashpress();
            sprintheld();
            jumppressed();
            jumpheld();
            Weaponfire();
            Weaponfireheld(); //automatic fire
            Upheld();
            Downheld();
            Leftheld();
            Rightheld();
            Tiltedupheld();
            Tilteddownheld();
            Aimingheld();
            Bigmappressed();
            Up(); //new
        }
        public virtual bool crouchheld()
        {
            if (Input.GetKey(Crouchheld)||mobilecrouch)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool dashpress()
        {
            if (Input.GetKeyDown(Dashpress) ||dashmo)
            {
               
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool sprintheld()
        {

            if (Input.GetKey(Sprintheld))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public virtual bool jumpheld()
        {
            if (Input.GetKey(Jumpheld)||jumpholdmo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool jumppressed()
        {
            if (Input.GetKeyDown(Jumpheld)||jumpmo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Weaponfire()  // thisfunction use in weapon script
        {
            if (Input.GetKeyDown(weaponfire)||firenotheld)
            {
              
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Weaponfireheld()  // thisfunction use in weapon script for auto fire
        {
            if (Input.GetKey(weaponfire)||fireheld)
            {
               
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Upheld()             // thisfunction use in amimmanager script
        {
            if (Input.GetKey(upheld)||laderup||grabup)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Up()             // thisfunction use in amimmanager script
        {
            if (Input.GetKey(upheld)||ledgeup)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Upheldweapon()             // thisfunction use in amimmanager script
        {
            if (Input.GetKey(upheld)||weaponup)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Downheld()           // thisfunction use in amimmanager script
        {
            
            if (Input.GetKey(downheld)||laderdown||grabdown)
            {
                return true;
            }
            else
            {
                return false;
            }
        } public virtual bool Downheldweapon()           // thisfunction use in amimmanager script
        {
            
            if (Input.GetKey(downheld)||weapondown)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Tiltedupheld() // thisfunction use in amimmanager script
        {
            
                if (Input.GetKey(tiltedupheld))
                {
                    return true;
                }
                else
                {
                    return false;
                }
          }
        public virtual bool Tilteddownheld()  // thisfunction use in amimmanager script
        {
            if (Input.GetKey(tilteddownheld))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       public virtual bool Aimingheld()
        {
            if (Input.GetKey(aimingheld)) // aimmanger script for close aiming target
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Changeweaponpressed()     // change weapon
        {
            if (Input.GetKeyDown(changeweaponpress)||changeweapon) // aimmanger script
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Leftheld()           // thisfunction use in amimmanager script
        {

            if (Input.GetKey(LeftHeld)||mapleft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Rightheld()           // thisfunction use in amimmanager script
        {

            if (Input.GetKey(RightHeld)||mapright)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual bool Bigmappressed()    
        {
            if (Input.GetKeyDown(bigmappressed)||bigmapon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void offallmobileinput()
        {


            mobilecrouch = false;
            dashmo = false;
            jumpmo = false;
            jumpholdmo = false;
            bigmapon = false;
            fireheld = false;
            firenotheld = false;
            changeweapon = false;
            weaponup = false;
            weapondown = false;
            laderup = false;
            laderdown = false;
            grabup = false;
            grabdown = false;
            mapright = false;
            mapleft = false;
            ledgeup = false;
            GetComponent<mobileinput>().notrightmove();
            GetComponent<mobileinput>().notleftmove();

        }
 

    }
}
