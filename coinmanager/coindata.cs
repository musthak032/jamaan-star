using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace metroidvania
{
    public class coindata : MonoBehaviour
    {

        public List<string> currentcoinname = new List<string>();

        public int count;

        bool found;
        // Start is called before the first frame update
        void Start()
        {


         
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void addstring(GameObject currentcoins)
        {
            currentcoinname.Add(currentcoins.name);
            save();
        }
        protected virtual void save()
        {

            found = true;
            PlayerPrefs.SetInt("coinfound", found ? 1 : 0);


            for (int i = 0; i < currentcoinname.Count; i++)
            {

                PlayerPrefs.SetString("currentcoin" + i, currentcoinname[i]);
            }
            PlayerPrefs.SetInt("count", currentcoinname.Count);

        }

        public virtual void load()
        {


            currentcoinname.Clear();
            count = PlayerPrefs.GetInt("count");
            for (int i = 0; i < count; i++)
            {

                string savecurrentcoin = PlayerPrefs.GetString("currentcoin" + i);
                currentcoinname.Add(savecurrentcoin);
            }
        }
    }
}
