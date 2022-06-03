using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace metroidvania {
    public class playermanager : MonoBehaviour
    {
        [SerializeField] protected GameObject select1;
        [SerializeField] protected GameObject select2;
        [SerializeField] protected Text coinvalue;

        [SerializeField] protected GameObject buybutton1;
      

        [SerializeField] protected Text item1value;


        bool Buy1;
        // Start is called before the first frame update
        void Start()
        {
            ///
            if (Buy1 = PlayerPrefs.GetInt("buy1") == 1 ? true : false)
            {
                buybutton1.SetActive(false);
                select2.SetActive(true);

                item1value.text = "sold";

            }

            //
            if (PlayerPrefs.GetInt("playerno") == 0)
            {
                select1.SetActive(false);
                if (Buy1 = PlayerPrefs.GetInt("buy1") == 1 ? true : false)
                {
                    select2.SetActive(true);
                }

            }
            if (PlayerPrefs.GetInt("playerno") == 1)
            {
                select1.SetActive(true);
                select2.SetActive(false);

            }
          
        }

        // Update is called once per frame
        void Update()
        {
            coincalculation();
        }

        protected virtual void coincalculation()
        {
            coinvalue.text =PlayerPrefs.GetInt("coin").ToString();

        }
        public virtual void player()
        {

            PlayerPrefs.SetInt("playerno", 0);
            select1.SetActive(false);
            select2.SetActive(true);
        }
        public virtual void player1()
        {

            PlayerPrefs.SetInt("playerno", 1);
            select1.SetActive(true);
            select2.SetActive(false);
        }
        public virtual void buy1()
        {
           
            int currentcoin = PlayerPrefs.GetInt("coin");
            if (PlayerPrefs.GetInt("coin") >= 200)
            {

                int coin = PlayerPrefs.GetInt("coin") - 200;
                PlayerPrefs.SetInt("coin", coin);
                Buy1 = true;
                PlayerPrefs.SetInt("buy1", Buy1 ? 1 : 0);
                item1value.text = "sold";
                buybutton1.SetActive(false);
                select2.SetActive(true);
             
            }
        }
    } 
}
