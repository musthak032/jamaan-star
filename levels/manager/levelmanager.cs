using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace metroidvania
{
    public class levelmanager : manager
    {
        public Bounds levelsize;
        public List<GameObject> initialplayer; //change
        public Image fadescreen;

        public List<Transform> availablespawnlocation = new List<Transform>();
        public List<Transform> playerindicatespawnlocation = new List<Transform>();
        public Transform fogspawnlocation;
        public GameObject Fogofwar;

        private Vector3 startinglocation;
        private Vector3 playerimdicatelocation;


        protected fogofwar[] fog;
        protected List<fogofwar> fogtiles = new List<fogofwar>();
        protected List<int> id = new List<int>();
        public int[] tileid;

       public bool loadfromsave;
        protected bool nextdoorscene;
        protected virtual void Awake()
        {
            
            int gamefile = PlayerPrefs.GetInt("gamefile"); // for save next different so we use
        
           
            loadfromsave = PlayerPrefs.GetInt(" " + gamefile + "loadfromsave") == 1 ? true : false;
            if (loadfromsave)  //
            {

                startinglocation = availablespawnlocation[PlayerPrefs.GetInt(" " + gamefile + "savespawnreference")].position;
                playerimdicatelocation = playerindicatespawnlocation[PlayerPrefs.GetInt(" " + gamefile + "savespawnreference")].position;


            }
           
            if (availablespawnlocation.Count <= PlayerPrefs.GetInt(" " + gamefile + "spawnreference"))
            {

                startinglocation = availablespawnlocation[0].position;
                playerimdicatelocation = playerindicatespawnlocation[0].position;
            }
            else
            {
                if (!loadfromsave) 
                {
                    startinglocation = availablespawnlocation[PlayerPrefs.GetInt(" " + gamefile + "spawnreference")].position;
                    playerimdicatelocation = playerindicatespawnlocation[PlayerPrefs.GetInt(" " + gamefile + "spawnreference")].position;
                    nextdoorscene = false;
                  


                }
                createplayer(initialplayer[PlayerPrefs.GetInt("playerno")], startinglocation);
               
                Instantiate(Fogofwar, fogspawnlocation.position, Quaternion.identity);
                
                fog = FindObjectsOfType<fogofwar>();
            }
          
            
        }

        protected override void initialization()
        {
            base.initialization();
            int gamefile = PlayerPrefs.GetInt("gamefile");
            PlayerPrefs.SetInt(" " + gamefile + "loadfromsave", levelmanager.loadfromsave ? 1 : 0);
            playerindicator.transform.position = playerimdicatelocation;
            
            StartCoroutine(fadein());
            for(int i = 0; i < fog.Length; i++)
            {

                fogtiles.Add(fog[i]);
            }
            int[] numberarray = PlayerPrefsX.GetIntArray(" " +gamefile + "TilesToRemove");
            foreach(int number in numberarray)
            {
                id.Add(number);
                Destroy(fogtiles[number].gameObject);

            }

            //new
            Admanager.instance.requestintersial();

        }

        public virtual void removefog(fogofwar fogtile)
        {
            id.Add(fogtiles.IndexOf(fogtile));
            Destroy(fogtile.gameObject);

        }
        protected virtual void OnDisable()
        {
            tileid = id.ToArray();
            PlayerPrefsX.SetIntArray(" " + character.gamefile+"TilesToRemove", tileid);
            PlayerPrefs.SetInt(" " + character.gamefile + "facingleft", character.isfaceleft?1:0);
            
        }

        public virtual void NextScene(SceneReference scene,int spawnreference)
        {
            tileid = id.ToArray();
            PlayerPrefsX.SetIntArray(" " + character.gamefile + "TilesToRemove", tileid);
            PlayerPrefs.SetInt(" " + character.gamefile + "facingleft", character.isfaceleft ? 1 : 0);
            PlayerPrefs.SetInt(" " + character.gamefile + "spawnreference", spawnreference);

            PlayerPrefs.SetInt(" " + character.gamefile + "currenthealth", player.GetComponent<playerhealth>().healthpoints);
            //for near door
            //new
            if (Random.Range(0, 3) == 0)
            {
                Admanager.instance.showintersition();
            }

            StartCoroutine(fadeout(scene));
        }

        protected virtual IEnumerator fadein()
        {

            float timestarted = Time.time;
            float timesincestarted = Time.time - timestarted;
            float percentagecomplete = timesincestarted / .5f;
            Color currentcolor = fadescreen.color;
            while (true)
            {
                 timesincestarted = Time.time - timestarted;
                 percentagecomplete = timesincestarted / .5f;
                currentcolor.a = Mathf.Lerp(1, 0, percentagecomplete);
                fadescreen.color = currentcolor;
                if (percentagecomplete >= 1)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
        }

        public virtual IEnumerator fallfadeout()
        {
            float timestarted = Time.time;
            float timesincestarted = Time.time - timestarted;
            float percentagecomplete = timesincestarted / .5f;
            Color currentcolor = fadescreen.color;
            while (true)
            {
                timesincestarted = Time.time - timestarted;
                percentagecomplete = timesincestarted / .5f;
                currentcolor.a = Mathf.Lerp(0, 1, percentagecomplete);
                fadescreen.color = currentcolor;
                if (percentagecomplete >= 1)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
            StartCoroutine(fadein());

        }
        protected virtual IEnumerator fadeout(SceneReference scene)
        {

            float timestarted = Time.time;
            float timesincestarted = Time.time - timestarted;
            float percentagecomplete = timesincestarted / .5f;
            Color currentcolor = fadescreen.color;
            while (true)
            {
                timesincestarted = Time.time - timestarted;
                percentagecomplete = timesincestarted / .5f;
                currentcolor.a = Mathf.Lerp(0, 1, percentagecomplete);
                fadescreen.color = currentcolor;
                if (percentagecomplete >= 1)
                {
                    break;
                }
                yield return new WaitForEndOfFrame();

            }
          


            SceneManager.LoadScene(scene);
        }

        protected virtual void OnDrawGizmos()
        {

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(levelsize.center, levelsize.size);


        }


    }
       
}
