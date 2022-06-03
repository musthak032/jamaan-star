//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
////namespace metroidvania

//    public class adsmanager : MonoBehaviour
//    {
//        public static adsmanager instance;
       
//        public string appid = "	ca-app-pub-3940256099942544/3419835294";

//        private BannerView bannerView;

//        private string bannerid = "ca-app-pub-3940256099942544/6300978111";

//        private InterstitialAd fullscreenads;

//        private string fullscreenadsid = "	ca-app-pub-3940256099942544/1033173712";
//        private void Awake()
//        {

//            if (instance == null)
//            {

//                instance = this;
//                DontDestroyOnLoad(instance.gameObject);
//            }
//            else
//            {

//                Destroy(this);
//            }
//        }
//        private void Start()
//        {
//            //for publish
//            //MobileAds.Initialize(InitializationStatus => { });
//            requestfullscreenads();
//        }
//        public void requestbanner()
//        {

//            bannerView = new BannerView(bannerid, AdSize.Banner, AdPosition.Bottom);
//        /*for publish
//            AdRequest request = new AdRequest.Builder().Build();*/
//        AdRequest request = new AdRequest.Builder()
//            .Build();
        
            


//        bannerView.LoadAd(request);
//        bannerView.Show();

//    }
//        public void hidebanner()
//        {

//            bannerView.Hide();
//        }
//        public void requestfullscreenads()
//        {
//            if (fullscreenads != null)
//            {

//                fullscreenads.Destroy();
//            }
//            fullscreenads = new InterstitialAd(fullscreenadsid);
//            AdRequest request = new AdRequest.Builder().Build();
//            fullscreenads.LoadAd(request);
//        }
//        public void showfullscreends()
//        {
//            requestfullscreenads();
//            if (fullscreenads.IsLoaded())
//            {
//                fullscreenads.Show();
//            }
//            else
//            {
//                Debug.Log("full screen ads not loaded");
              
//            }
//        }
//    }

