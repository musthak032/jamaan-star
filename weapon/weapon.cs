using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania
{
    public class weapon : ability
    {
        //create list for objectpooler
        [SerializeField]protected List<weapontype> weapontypes;
        //create list of gameobject for objectpooler to get item of bullet to this list<>gameobject
        [HideInInspector]
        public List<GameObject> currentpool = new List<GameObject>();



        ///create game object for parenting bullet of obbject pooler script
        // gun barellposition
        [Tooltip("this variable is really cool")]
        public Transform gunbarrel;
        public Transform gunrotation; // is also use for grabhook
        //aimmangeruse
        public weapontype currentweapon;
        //change arms to enblefire
        public float currenttimetillchangearms;
        public GameObject currentprojectile; //get current pool object
        [HideInInspector]
        public List<GameObject> bulletstoreset = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> totalpools;


        private GameObject projectileparentfolder;
        private float currenttimebetweenshot; //

        //new
        [SerializeField] protected GameObject muzzleflash;
        List<GameObject> flash = new List<GameObject>();
        GameObject flashobject;
        [SerializeField] protected Transform flashposition;
        // audio
        [SerializeField] protected AudioSource fire;

        protected override void initialization()
        {
            base.initialization();
            changeweapon();
        }
        protected virtual void Update()
        {
            if (Gamemanager.gamepause)
            {
                return;
            }
            if (input.Weaponfire() && !character.grabbingLedge)//new ledge
            {

                fireweapon();
            }
            if (input.Changeweaponpressed())
            {

                changeweapon();
            }
        }
       protected virtual void FixedUpdate()
        {

            pointgun();
            negatetimetillchangearms();
            fireweaponheld();
        }
        protected virtual void fireweapon()
        {
            currenttimetillchangearms = currentweapon.lifetime;
            currentprojectile = Objectpooler.getobject(currentpool, currentweapon, this, projectileparentfolder, currentweapon.projectile.tag);
            aimmanger.changearms(); // for change aim direction
            
            if (currentprojectile != null)
            {
                Invoke("placeprojectile", .1f);
                if (currentweapon.projectile.tag != "grabbinghook")
                {
                    fire.Play();
                }//audio
            }
            currenttimebetweenshot = currentweapon.timebetweenshot; //automatic fire weapon 
        }
        protected virtual void fireweaponheld()
        {
            if (input.Weaponfireheld()&&!character.grabbingLedge) //new ledge
            {
                if (currentweapon.automatic)
                {
                    currenttimetillchangearms = currentweapon.lifetime;
                    aimmanger.changearms(); // for change aim direction
                    currenttimebetweenshot -= Time.deltaTime;
                    if (currenttimebetweenshot < 0)
                    { 

                        currentprojectile = Objectpooler.getobject(currentpool, currentweapon, this, projectileparentfolder,currentweapon.projectile.tag);
                    
                        if (currentprojectile != null)
                        {
                            Invoke("placeprojectile", .1f);
                            if (currentweapon.projectile.tag != "grabbinghook")
                            {
                                fire.Play();
                            }//audio
                            
                        }
                        currenttimebetweenshot = currentweapon.timebetweenshot;
                    }
                }

            }

        }

        protected virtual void pointgun()
        {

            if (!aimmanger.aiming) 
            {
                if (!character.isfaceleft)
                {
                    if (character.iswallsliding)
                    {
                        aimmanger.wheretoaim.position = new Vector2(aimmanger.bounds.min.x, aimmanger.bounds.center.y);
                    }
                    else
                    {
                        aimmanger.wheretoaim.position = new Vector2(aimmanger.bounds.max.x, aimmanger.bounds.center.y);
                    }
                }
                else
                {
                    if (character.iswallsliding)
                    {
                        aimmanger.wheretoaim.position = new Vector2(aimmanger.bounds.max.x, aimmanger.bounds.center.y);
                    }
                    else
                    {
                        aimmanger.wheretoaim.position = new Vector2(aimmanger.bounds.min.x, aimmanger.bounds.center.y);
                    }
                }
            }
            aimmanger.aiminggun.transform.GetChild(0).position = aimmanger.wheretoaim.position;
            aimmanger.aiminglefthand.transform.GetChild(0).position = aimmanger.wheretoplacehand.position;


        }
        protected virtual void negatetimetillchangearms()
        {
            if (grabbinghook.connected)
            {
                return;
            }
            currenttimetillchangearms -= Time.deltaTime;
        }
        protected virtual void changeweapon()
        {
            bool match = new bool();
            for(int i = 0; i < weapontypes.Count; i++)
            {
                if (currentweapon == null)      // current weapon
                {

                    currentweapon = weapontypes[0];
                    currenttimebetweenshot = currentweapon.timebetweenshot;
                    currentprojectile = currentweapon.projectile;
                    Newpool();
                    return;
                }
                else
                {
                    if(weapontypes[i]==currentweapon)
                    {

                        i++;
                        if (i == weapontypes.Count)
                        {

                            i = 0;
                        }
                        currentweapon = weapontypes[i];
                        currenttimebetweenshot = currentweapon.timebetweenshot;
                    }
                }
            }
            for(int i = 0; i < totalpools.Count; i++)
            {


                if (currentweapon.projectile.tag == totalpools[i].tag)
                {

                    projectileparentfolder = totalpools[i].gameObject;
                    currentprojectile = currentweapon.projectile;
                    match = true;
                }
            }
            if (currentweapon.projectile.tag == "grabbinghook")
            {

               grabbinghook.enabled = true;        //grabbinghook script
            }
            else
            {

                grabbinghook.remove = true;
                grabbinghook.removegrabble();
                grabbinghook.enabled = false;
            }
            if (!match)
            {
                Newpool();
            }
            if(currentweapon.canresetpool)
            {

                bulletstoreset.Clear();
            }

        }
        // create pool in objectpooler
        protected virtual void Newpool()
        {
            GameObject newpool = new GameObject();
            projectileparentfolder = newpool;
            Objectpooler.createpool(currentweapon, currentpool, projectileparentfolder,this);
            currentprojectile = currentweapon.projectile;
            //if (currentweapon.canresetpool)
            //{
            //    bulletstoreset.Clear();
            //}

        }
        //new
        protected virtual void placemuzzleflash()
        {

            flashobject=Instantiate(muzzleflash, flashposition.position,Quaternion.identity);
            flashobject.transform.rotation = gunrotation.rotation;
            flash.Add(flashobject);

            destroyflash();
        }
        protected virtual void destroyflash()
        {

            for(int i = 0; i < flash.Count; i++)
            {


                Destroy(flash[i].gameObject,.5f);
            }

        }
        //
        protected virtual void placeprojectile()
        {
            currentprojectile.transform.position = gunbarrel.position;
            //currentprojectile.transform.position = gunbarrel.position;
            currentprojectile.transform.rotation = gunrotation.rotation;
            currentprojectile.SetActive(true);
            if (!character.isfaceleft) 
            {
                if (character.iswallsliding)
                {
                    currentprojectile.GetComponent<projectile>().left = true;

                }
                else
                {
                    currentprojectile.GetComponent<projectile>().left = false;
                }
            }
            else
            {
                if (character.iswallsliding)
                {
                    currentprojectile.GetComponent<projectile>().left = false;

                }
                else
                {
                    currentprojectile.GetComponent<projectile>().left = true;
                }
            }
            currentprojectile.GetComponent<projectile>().fired = true;
            placemuzzleflash();
        }
    }
}
