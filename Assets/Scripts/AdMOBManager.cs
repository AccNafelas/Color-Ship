using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdMOBManager : MonoBehaviour
{

    public string Admob_APP_ID = "ca-app-pub-3577746380704619~2780183495";

    [Header("Banner")]
    public UnityEvent OnCloseBanner;
    [Header("Interestitial")]
    public UnityEvent OnCloseInsterestetitial_AdMob;
    public UnityEvent OnInterestitialFailed_AdMob;
    [Header("Rewarded")]
    public UnityEvent OnEndRewarded_AdMob;
    public UnityEvent OnFailedRewarded_AdMob;

    private BannerView bannerAD;
    private InterstitialAd interstialAd;
    private RewardBasedVideoAd rewardAd;

    private string BannerAdID = "ca-app-pub-3577746380704619/2588611804";
    private string IntestitialAdID = "ca-app-pub-3577746380704619/4172268165";
    private string RewardedAdID = "ca-app-pub-3577746380704619/4335919690";

    //[Space]
    //public Image interestitialReady;
    //public Text timeText;


    public void RequestAll()
    {
        RequestBanner();
        RequestInterstitial();
        RequestVideoAd();
    }
    void Start()
    {
        if (!AdvertisementManager.instance.useAds) return;
    
        MobileAds.Initialize(Admob_APP_ID);
       
        RequestBanner();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
       
    }

   
    private void OnDestroy()
    {
       
    }

    #region Banner
    void RequestBanner()
    {
        Boolean testmode = AdvertisementManager.instance.TestMode;
        if (testmode){
            //Id Nahue 0BEB4F2A5DF02E32340C6FB6B72DBAA1
            //Id Fede DACE943851F93FC242A335870EA607D2 - Z2 Play
            //Id Lauta 3E42573D7C76C2498D8DE51A7EBFC053
            //Id Fer

            bannerAD = new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.SmartBanner, AdPosition.Bottom);
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice("DACE943851F93FC242A335870EA607D2")
                .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
                .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
                .Build();


            bannerAD.LoadAd(adRequest);

        }
        else
        {


            //        //AdMob Banner Init
            //#if UNITY_ANDROID
            //        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
            //#elif UNITY_IPHONE             
            //        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
            //#else             
            //        string adUnitId = "unexpected_platform";
            //#endif

            //string banner_ID = "ca-app-pub-3940256099942544/6300978111"; //testing purposes

            bannerAD = new BannerView(BannerAdID, AdSize.SmartBanner, AdPosition.Bottom);
            //For Real APP ADMOB
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice("DACE943851F93FC242A335870EA607D2")
                .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
                .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
                .Build();

        //COMMENTED FOR IRONSRC

        bannerAD.LoadAd(adRequest);
        }
    }

    public void Display_BannerAdMob()
    {
        bannerAD.Show();
    }

    //HANDLE EVENTS BANNER ADMOB
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //Display_BannerAdMob();
        RequestBanner();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        OnCloseBanner.Invoke();

    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    //Event for Banner Admob
    void HandleBannerADEventsAdMob(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded += this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening += this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed += this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.bannerAD.OnAdLoaded -= this.HandleOnAdLoaded;
            // Called when an ad request failed to load.
            this.bannerAD.OnAdFailedToLoad -= this.HandleOnAdFailedToLoad;
            // Called when an ad is clicked.
            this.bannerAD.OnAdOpening -= this.HandleOnAdOpened;
            // Called when the user returned from the app after an ad click.
            this.bannerAD.OnAdClosed -= this.HandleOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.bannerAD.OnAdLeavingApplication -= this.HandleOnAdLeavingApplication;
        }

    }



    void onEnableAdMobBanner()
    {
        HandleBannerADEventsAdMob(true);
    }
    void onDisableAdMobBanner()
    {
        HandleBannerADEventsAdMob(false);
    }

    #endregion

    #region Interstitial
    void RequestInterstitial()
    {
      Boolean testmode = AdvertisementManager.instance.TestMode;
        if (testmode)
        { //string interstitial_ID = "ca-app-pub-3940256099942544/1033173712"; //testing purposes
            interstialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
            HandleInterstitialADEventsAdMob(true);
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
                .AddTestDevice("DACE943851F93FC242A335870EA607D2")
                .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
                .Build();
            //Id Nahue 0BEB4F2A5DF02E32340C6FB6B72DBAA1
            //Id Fede DACE943851F93FC242A335870EA607D2 - Z2 Play
            //Id Lauta 3E42573D7C76C2498D8DE51A7EBFC053
            //Id Fer

            interstialAd.LoadAd(adRequest);
        }
        else
        {        
       

        interstialAd = new InterstitialAd(IntestitialAdID);

        HandleInterstitialADEventsAdMob(true);


        //For Real APP
        AdRequest adRequest = new AdRequest.Builder()
          .AddTestDevice("DACE943851F93FC242A335870EA607D2")
          .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
          .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
          .Build();

      


        interstialAd.LoadAd(adRequest);
        }
    }

    private float maxWaitForInterestitial = 10f;
    public void Display_InterstitialAD()
    {
        RequestInterstitial();
        StartCoroutine(WaitForInterestitial());
        //if (IronSource.Agent.isInterstitialReady())
        //{
        //    IronSource.Agent.showInterstitial();
        //}
        //else { RequestInterstitial(); }
    }

    IEnumerator WaitForInterestitial()
    {
        float t = 0f;

        if (interstialAd.IsLoaded())
        {
            interstialAd.Show();
        }
        else
        {
            
            while (!interstialAd.IsLoaded() && (t < maxWaitForInterestitial))
            {
                t += Time.deltaTime;
                //timeText.text = t.ToString();
                yield return new WaitForEndOfFrame();
            }

            if (interstialAd.IsLoaded())
            { interstialAd.Show(); }
            else
            {
                OnInterestitialFailed_AdMob.Invoke();
            }
        }
    }

    //start handling events for admob interstitial
    void HandleInterstitialADEventsAdMob(bool subscribe)
    {
        if (subscribe)
        {
            // Called when an ad request has successfully loaded.
            interstialAd.OnAdLoaded += HandleInterAdMobOnAdLoaded;
            // Called when an ad request failed to load.
            interstialAd.OnAdFailedToLoad += HandleInterAdMobOnAdFailedToLoad;
            // Called when an ad is shown.
            interstialAd.OnAdOpening += HandleInterAdMobOnAdOpened;
            // Called when the ad is closed.
            interstialAd.OnAdClosed += HandleInterAdMobOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstialAd.OnAdLeavingApplication += HandleInterAdMobOnAdLeavingApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            interstialAd.OnAdLoaded -= HandleInterAdMobOnAdLoaded;
            // Called when an ad request failed to load.
            interstialAd.OnAdFailedToLoad -= HandleInterAdMobOnAdFailedToLoad;
            // Called when an ad is shown.
            interstialAd.OnAdOpening -= HandleInterAdMobOnAdOpened;
            // Called when the ad is closed.
            interstialAd.OnAdClosed -= HandleInterAdMobOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            interstialAd.OnAdLeavingApplication -= HandleInterAdMobOnAdLeavingApplication;
            
        }
    }

    public void HandleInterAdMobOnAdLoaded(object sender, EventArgs args)
    {
        // RequestInterstitial();
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleInterAdMobOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //RequestInterstitial();

        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);

        HandleInterstitialADEventsAdMob(false);

        OnInterestitialFailed_AdMob.Invoke();
    }

    public void HandleInterAdMobOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleInterAdMobOnAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
        HandleInterstitialADEventsAdMob(false);

        OnCloseInsterestetitial_AdMob.Invoke();
        // interstialAd.Destroy();

        
    }

    public void HandleInterAdMobOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    #endregion

    #region Rewarded
    public bool RewardedVideoReady()
    {
        if (rewardAd == null)
            return false;

        if (rewardAd.IsLoaded())
        {
            return true;
        }
        else return false;
    }

    public void RequestVideoAd()
    {
        
        Boolean testmode = AdvertisementManager.instance.TestMode;
        if (testmode)
        {
            rewardAd = RewardBasedVideoAd.Instance;
            HandleVideoADEventsAdMob(true);

            //Id Nahue 0BEB4F2A5DF02E32340C6FB6B72DBAA1
            //Id Fede DACE943851F93FC242A335870EA607D2 - Z2 Play
            //Id Lauta 3E42573D7C76C2498D8DE51A7EBFC053
            //Id Fer
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
                .AddTestDevice("DACE943851F93FC242A335870EA607D2")
                .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
                .Build();

            rewardAd.LoadAd(adRequest, "ca-app-pub-3940256099942544/5224354917");            //Testing Purposes
        }
        else
        {
            rewardAd = RewardBasedVideoAd.Instance;
            HandleVideoADEventsAdMob(true);
 
            //For Real APP
            AdRequest adRequest = new AdRequest.Builder()
              .AddTestDevice("DACE943851F93FC242A335870EA607D2")
              .AddTestDevice("0BEB4F2A5DF02E32340C6FB6B72DBAA1")
              .AddTestDevice("3E42573D7C76C2498D8DE51A7EBFC053")
              .Build();
   

            rewardAd.LoadAd(adRequest, RewardedAdID);
        }

    }

    public void Display_Video()
    {
        StartCoroutine(WaitForVideo());
        //if (rewardAd.IsLoaded()) {
        //    rewardAd.Show();
        //}
    }

    public bool rewardedCanceled = false;
    public float maxWaitForReward = 10f;
    IEnumerator WaitForVideo()
    {
        rewardedCanceled = false;

        float t = 0f;
        if (rewardAd.IsLoaded())
        {
            rewardAd.Show();
        }
        else
        {
            //RequestVideoAd();
            while (!rewardAd.IsLoaded() && t < maxWaitForReward && !rewardedCanceled)
            {
                t += Time.deltaTime;
                //timeText.text = t.ToString();
                yield return new WaitForEndOfFrame();
            }
            if (rewardAd.IsLoaded() && !rewardedCanceled)
            {
                rewardAd.Show();
            }
            else
            {
                AdvertisementManager.instance.HideLoadingScreen();
            }
        }
        
    }

    void HandleVideoADEventsAdMob(bool suscribe)
    {
        if (suscribe)
        {
            // Called when an ad request has successfully loaded.
            rewardAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            rewardAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            rewardAd.OnAdOpening += HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            rewardAd.OnAdStarted += HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            rewardAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            rewardAd.OnAdLoaded -= HandleRewardBasedVideoLoaded;
            // Called when an ad request failed to load.
            rewardAd.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
            // Called when an ad is shown.
            rewardAd.OnAdOpening -= HandleRewardBasedVideoOpened;
            // Called when the ad starts to play.
            rewardAd.OnAdStarted -= HandleRewardBasedVideoStarted;
            // Called when the user should be rewarded for watching a video.
            rewardAd.OnAdRewarded -= HandleRewardBasedVideoRewarded;
            // Called when the ad is closed.
            rewardAd.OnAdClosed -= HandleRewardBasedVideoClosed;
            // Called when the ad click caused the user to leave the application.
            rewardAd.OnAdLeavingApplication -= HandleRewardBasedVideoLeftApplication;
        }

    }


    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);

        HandleVideoADEventsAdMob(false);

        OnFailedRewarded_AdMob.Invoke();
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");

        //RequestVideoAd();
        //OnEndRewarded_AdMob.Invoke();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);

        HandleVideoADEventsAdMob(false);

        OnEndRewarded_AdMob.Invoke();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion

}
