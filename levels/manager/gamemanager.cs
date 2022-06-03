using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class gamemanager : MonoBehaviour
    {
       
        protected GameObject player;
        protected character character;
        protected levelmanager levelmanager;
        protected GameObject playerindicator;
        protected GameObject bigmapcamera;
        protected inputmanager input;
        protected uimanager Uimanager;
        [HideInInspector] public float xmin;
        [HideInInspector] public float xmax;
        [HideInInspector] public float ymin;
        [HideInInspector] public float ymax;
        [HideInInspector] public bool gamepause;

        void Start()
        {

            initialization();
        }

        protected virtual void initialization()
        {

            player = FindObjectOfType<character>().gameObject;
            character = player.GetComponent<character>();
            levelmanager = FindObjectOfType<levelmanager>();
            playerindicator = FindObjectOfType<playerblip>().gameObject;
            input = player.GetComponent<inputmanager>();

            Uimanager = FindObjectOfType<uimanager>();
            bigmapcamera = FindObjectOfType<bigmap>().gameObject;
            xmin = levelmanager.levelsize.min.x;
            xmax = levelmanager.levelsize.max.x;
            ymin = levelmanager.levelsize.min.y;
            ymax = levelmanager.levelsize.max.y;
          
        }
        protected virtual void createplayer(GameObject initialplayer,Vector3 location)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 0)
            {

                foreach(GameObject obj in players)
                {
                    Destroy(obj);
                }
            }
            Instantiate(initialplayer, new Vector3(location.x,location.y,0), Quaternion.identity);
            initialplayer.GetComponent<character>().initializeplayer();



        }

       
    }
}
