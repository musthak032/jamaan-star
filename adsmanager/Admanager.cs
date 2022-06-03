using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
namespace metroidvania
{
    public class Admanager : MonoBehaviour
    {

        private BannerView bannerad;
        private InterstitialAd interstitial;
        private RewardedAd rewardbase;
        
        public string appid = "ca-app-pub-3940256099942544~3347511713";

        bool reward = false;
        
        public static Admanager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        void Start()
        {
            MobileAds.Initialize(initStatus => { });
         
            //this.requestbanner();
            this.requestrewardad();

            //new reward
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardbase.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardbase.OnAdClosed += HandleRewardedAdClosed;
          

        }
        private void Update()
        {
            if (reward)
            {
                reward = false;
                int currentcoin = PlayerPrefs.GetInt("coin");
                currentcoin += 500;
                PlayerPrefs.SetInt("coin", currentcoin);
            }
        }
        private AdRequest createrequest()
        {

            return new AdRequest.Builder().Build();
        }
        public void requestbanner()
        {
            string adunitid = "ca-app-pub-3940256099942544/6300978111";
            this.bannerad = new BannerView(adunitid, AdSize.SmartBanner, AdPosition.Top);

            this.bannerad.LoadAd(this.createrequest());
            this.bannerad.Show();

        }
     
        public void hidebannerads()
        {

            this.bannerad.Hide();
        }
        public void requestintersial()
        {
            string adunitid = "ca-app-pub-3940256099942544/1033173712";
            if (this.interstitial != null)
            {

                this.interstitial.Destroy();
            }
            this.interstitial = new InterstitialAd(adunitid);

            //load interdial ads
            this.interstitial.LoadAd(this.createrequest());


        }

        public void showintersition()
        {

            if (this.interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                Debug.Log("intersition not ready");
            }
        }
        public void requestrewardad()
        {
            string adunitid = "ca-app-pub-3940256099942544/5224354917";
            if (this.rewardbase != null)
            {
                this.rewardbase.Destroy();
            }
            this.rewardbase = new RewardedAd(adunitid);
            this.rewardbase.LoadAd(this.createrequest());

        }
        public void showrewardad()
        {
            if (this.rewardbase.IsLoaded())
            {
                rewardbase.Show();
            }
           
            

        }
        #region RewardedAd callback handlers
        public void HandleRewardedAdClosed(object sender, EventArgs args)
        {
            this.requestrewardad();
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            reward = true;
        }
        #endregion
    }
}
