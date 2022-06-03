using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class enemyweapon : AiManager
    {
        [SerializeField] protected bool automatic;
        [SerializeField] protected bool onlyfirewhenclose;
        [SerializeField] protected bool aimatplayer;
        [SerializeField] protected weapontype weapon;
        [SerializeField] protected Transform projectilespawnposition;
        [SerializeField] protected Transform projectilespawnrotation;
        [SerializeField] protected int automaticburstamount;

        [HideInInspector] public List<GameObject> currentpool;
        [HideInInspector] public List<GameObject> totalpool;

        public GameObject currentprojectile;
        protected objectpooler objectpooler;
        protected GameObject projectileparentfolder;

        protected bool poolspawn;
        protected bool autofire;
        protected float autotime;
        protected int shotsfire;


        protected override void Initialization()
        {
            base.Initialization();
            Invoke("pool", .05f);
        }
        protected virtual void pool()
        {
            projectileparentfolder = new GameObject();
            objectpooler = FindObjectOfType<objectpooler>();
            objectpooler.createenemypool(weapon, currentpool, projectileparentfolder, this);
            timetilldoaction = originaltimetilldoaction;
            if (automatic)
            {
                autotime = weapon.timebetweenshot;
            }
            poolspawn = true;
        }
        protected virtual void FixedUpdate()

        {

            handlefire();
        }
        protected virtual void handlefire()
        {

            if (poolspawn)
            {
                timetilldoaction -= Time.deltaTime;
                if (timetilldoaction <= 0)
                {
                    if (automatic)
                    {
                        autofire = true;
                        fireautomaticweapon();
                    }
                    else
                    {
                        fireweapon();
                    }
                }
            }
            if (aimatplayer)
            {

                aim();
            }
        }
        protected virtual void fireweapon()
        {
            if (onlyfirewhenclose && enemycharacter.playerisclose||!onlyfirewhenclose)
            {
                currentprojectile = objectpooler.getenemyobject(currentpool, weapon, projectileparentfolder, weapon.projectile.tag);
                if (currentprojectile != null)
                {
                    Invoke("placeprojectile", .05f);
                    timetilldoaction = originaltimetilldoaction;
                }

            }
        }
        protected virtual void fireautomaticweapon()
        {
            if(autofire && onlyfirewhenclose &&enemycharacter.playerisclose||autofire && !onlyfirewhenclose)
            {

                autotime -= Time.deltaTime;
                if (autotime <= 0)
                {
                    currentprojectile = objectpooler.getenemyobject(currentpool, weapon, projectileparentfolder,weapon.projectile.tag);
                    if (currentprojectile != null)
                    {
                        Invoke("placeprojectile", .05f);
                       
                    }
                    autotime = weapon.timebetweenshot;
                    shotsfire++;
                    if (shotsfire == automaticburstamount)
                    {
                        timetilldoaction = originaltimetilldoaction;
                        shotsfire = 0;
                        autofire = false;
                    }

                }
            }
        }
        protected virtual void aim()
        { 
            if(player!=null && aimatplayer)
            {

                Vector3 target = player.transform.position;
                target.z = 0;
                Vector3 currentposition = projectilespawnposition.position;
                if (!enemycharacter.faceleft)
                {
                    target.x = target.x - currentposition.x;
                    target.y = playercollider.bounds.center.y - currentposition.y;
                }
                else
                {
                    target.x =  currentposition.x-target.x;
                    target.y = currentposition.y- playercollider.bounds.center.y ;
                }
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
                projectilespawnrotation.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }


           
        }
        protected virtual void placeprojectile()
        {
            currentprojectile.transform.position = projectilespawnposition.position;
            currentprojectile.transform.rotation = projectilespawnrotation.rotation;
            currentprojectile.SetActive(true);
            if (!enemycharacter.faceleft)
            {
              
                
                    currentprojectile.GetComponent<projectile>().left = false;
                
            }
            else
            {
               
                
                    currentprojectile.GetComponent<projectile>().left = true;
                
            }
            currentprojectile.GetComponent<projectile>().fired = true;
        }


    }
}
