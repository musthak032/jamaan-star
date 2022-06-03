using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace metroidvania 
{
    public class objectpooler : MonoBehaviour
    {
        // make scriot statcic
        public static objectpooler instance;
        /// constructor for this script
        public static objectpooler Instance
        {

            get
            {
                //add object and add objectpooler script
                if (instance == null)
                {

                    GameObject obj = new GameObject("objectpooler");
                    obj.AddComponent<objectpooler>();

                }
                return instance;
            }

        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);

            }
            else
            {
                Destroy(gameObject);
            }
        }
        private GameObject currentitem;
        // Start is called before the first frame update
        public void createpool(weapontype weapon,List<GameObject> currentpool,GameObject projectileparentfolder,weapon weaponscript)
        {
            weaponscript.totalpools.Add(projectileparentfolder); // help to switch weapon what weapon is it
            for (int i = 0; i < weapon.amounttopool; i++)
            {
                currentitem = Instantiate(weapon.projectile);
                currentitem.SetActive(false);
                currentpool.Add(currentitem);
                currentitem.transform.SetParent(projectileparentfolder.transform);
                
            }
            projectileparentfolder.name = weapon.name;
            projectileparentfolder.tag = weapon.projectile.tag;
        }
        public void createenemypool(weapontype weapon, List<GameObject> currentpool, GameObject projectileparentfolder,enemyweapon weaponscript)
        {
            //weaponscript.totalpools.Add(projectileparentfolder); // help to switch weapon what weapon is it
            for (int i = 0; i < weapon.amounttopool; i++)
            {
                currentitem = Instantiate(weapon.projectile);
                currentitem.SetActive(false);
                currentpool.Add(currentitem);
                currentitem.transform.SetParent(projectileparentfolder.transform);

            }
            projectileparentfolder.name = weapon.name;
            projectileparentfolder.tag = weapon.projectile.tag;
        }
        //
        public void createenemypoolbean(weapontype weapon, List<GameObject> currentpool, GameObject projectileparentfolder, beanweapon weaponscript)
        {
            //weaponscript.totalpools.Add(projectileparentfolder); // help to switch weapon what weapon is it
            for (int i = 0; i < weapon.amounttopool; i++)
            {
                currentitem = Instantiate(weapon.projectile);
                currentitem.SetActive(false);
                currentpool.Add(currentitem);
                currentitem.transform.SetParent(projectileparentfolder.transform);

            }
            projectileparentfolder.name = weapon.name;
            projectileparentfolder.tag = weapon.projectile.tag;
        }
        // tag is use for weapon swap
        public virtual GameObject getobject(List<GameObject> currentpool,weapontype weapon,weapon weaponscript,GameObject projectileparentfolde,string tag)
        {
            for(int i = 0; i < currentpool.Count; i++)
            {
                /// check inactive bullet in editorscene
                if (!currentpool[i].activeInHierarchy&&currentpool[i].tag==tag)
                {
                    if (weapon.canresetpool && weaponscript.bulletstoreset.Count < weapon.amounttopool)
                    {
                        weaponscript.bulletstoreset.Add(currentpool[i]);
                    }
                    return currentpool[i];
                 
                }
            }
            //this for weapon switch for what bullet is rest or expan
            foreach(GameObject item in currentpool)
            {
                // thi statement use to expand bullet into barrel 
                if (weapon.canexpandpool && item.tag==tag)
                {

                    currentitem = Instantiate(weapon.projectile);
                    currentitem.SetActive(false);
                    currentpool.Add(currentitem);
                    currentitem.transform.SetParent(projectileparentfolde.transform);
                    return currentitem;
                }
                // thi statement use to reset bullet into barrel 
                if (weapon.canresetpool&& item.tag == tag)
                {
                    currentitem = weaponscript.bulletstoreset[0];
                    weaponscript.bulletstoreset.RemoveAt(0);
                    currentitem.SetActive(false);
                    weaponscript.bulletstoreset.Add(currentitem);
                    currentitem.GetComponent<projectile>().destroyprojectile();
                    return currentitem;
                }
            }
            return null;
            
        }
        public virtual GameObject getenemyobject(List<GameObject> currentpool, weapontype weapon, GameObject projectileparentfolde, string tag)
        {
            for (int i = 0; i < currentpool.Count; i++)
            {
                /// check inactive bullet in editorscene
                if (!currentpool[i].activeInHierarchy && currentpool[i].tag == tag)
                {
                   
                    return currentpool[i];

                }
            }
            //this for weapon switch for what bullet is rest or expan
            foreach (GameObject item in currentpool)
            {
                if (weapon.canexpandpool && item.tag == tag)
                {

                    currentitem = Instantiate(weapon.projectile);
                    currentitem.SetActive(false);
                    currentpool.Add(currentitem);
                    currentitem.transform.SetParent(projectileparentfolde.transform);
                    return currentitem;
                }
                // thi statement use to expand bullet into barrel 

                // thi statement use to reset bullet into barrel 

            }
            return null;

        }
    }
 
}

