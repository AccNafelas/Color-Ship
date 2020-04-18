using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class ADManager : MonoBehaviour
{
    public string Admob_APP_ID= "ca-app-pub-3577746380704619~2296662877";
    public string IronSrc_APP_ID = "b97d148d";


    private BannerView bannerAD;
    private InterstitialAd interstialAd;
    private RewardBasedVideoAd rewardAd;

    private int RandomNumber;


    // Start is called before the first frame update
   
    

    void Start()
    {
        System.Random rand = new System.Random();
        RandomNumber =  rand.Next(2);

        //When you publish your app, not used for testing
        //MobileAds.Initialize(Admob_APP_ID);

        RequestBanner();
        RequestInterstitial();
        RequestVideoAd();
    }

  

//    //For Rewarded Video IRON SOURCE
//    IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.REWARDED_VIDEO);
////For Interstitial
//IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.INTERSTITIAL);
////For Offerwall
//IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.OFFERWALL);
////For Banners
//IronSource.Agent.init(YOUR_APP_KEY, IronSourceAdUnits.BANNER);

                          
    void RequestBanner()
    {
        //Iron Source Banner
        IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.BANNER);
        IronSource.Agent.loadBanner(new IronSourceBannerSize(320, 50), IronSourceBannerPosition.BOTTOM);
        //IronSource.Agent.displayBanner();

        
        string testDevice = "fbd7fe3a-f9ce-4397-9231-10ca1ad1abe1";

                       

        //5a8b653e1d7a1537738a222303914142    ironsrc Secret Key


        //bloque real ca-app-pub-3577746380704619/1087522985

        //AdMob Banner Init

        string banner_ID = "ca-app-pub-3940256099942544/6300978111"; //testing purposes
        bannerAD = new BannerView(banner_ID, AdSize.SmartBanner, AdPosition.Top);

        AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();

        //AdMob Banner Init End


        //Testing Purposes
                
        //For Real APP ADMOB
        //AdRequest adRequest = new AdRequest.Builder().Build();

        //COMMENTED FOR IRONSRC
        bannerAD.LoadAd(adRequest);
    }

    void RequestInterstitial()
    {

        IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.INTERSTITIAL);
        IronSource.Agent.loadInterstitial();

         //commented to force iron source

       // if (RandomNumber == 0)
       // { //ADMOB
       // string interstitial_ID = "ca-app-pub-3940256099942544/1033173712"; //testing purposes
       // interstialAd = new InterstitialAd(interstitial_ID);

       // AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
       // //Testing Purposes

       // //For Real APP
       // //AdRequest adRequest = new AdRequest.Builder().Build();
       // interstialAd.LoadAd(adRequest);
       // }
       //else if (RandomNumber == 1)


       // { //IronSource
       //     IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.INTERSTITIAL);
       //     IronSource.Agent.loadInterstitial();

       // }

        
    }

    void RequestVideoAd()
    {


         //For Rewarded Video IRON SOURCE
            IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);
       

         //COMENTADO PARA FORZAR IRON SOURCE
        //string video_ID = "ca-app-pub-3940256099942544/5224354917"; //testing purposes
        //rewardAd = RewardBasedVideoAd.Instance;

        //AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
        ////Testing Purposes

        ////For Real APP
        ////AdRequest adRequest = new AdRequest.Builder().Build();


        //rewardAd.LoadAd(adRequest, video_ID);
    }


    public void Display_BannerAdMob()
    {
        System.Random rand = new System.Random();
        int number = rand.Next(2);
        Console.WriteLine("esto trae:   " + number);
        //if (number == 0)
        //    reqAdmobBanner();
        //if (number == 1)
        //    requIronsrcBanner();
        bannerAD.Show();
    }

    public void Display_BannerIronSrc()
    {
        System.Random rand = new System.Random();
        int number = rand.Next(2);
        Console.WriteLine("esto trae:   " + number);
        //if (number == 0)
        //    reqAdmobBanner();
        //if (number == 1)
        //    requIronsrcBanner();
      
        IronSource.Agent.displayBanner();
    }




    public void Display_InterstitialAD()
    {
        //comentado para forzar iron source
        //if (RandomNumber == 0)
        //{
        //    if (interstialAd.IsLoaded())
        //    {
        //        interstialAd.Show();
        //    }
        //}else if (RandomNumber == 1) { 
        //if (IronSource.Agent.isInterstitialReady())
        //{
        //       IronSource.Agent.showInterstitial();
        // }
        //}

        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }

    }

    public void Display_Video()
    {
        // forzando iron source
        //if (rewardAd.IsLoaded())
        //{
        //    rewardAd.Show();
        //}

        bool available = IronSource.Agent.isRewardedVideoAvailable();
        if (available) {
            IronSource.Agent.showRewardedVideo();
        }
        
    }



    //HANDLE EVENTS BANNER ADMOB
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Display_BannerAdMob();
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

    //End Event for Banner Admob


    //Star event for banner Iron Source
   void HandleBannerADEventsIronSrc(bool subscribe)
        {
        if (subscribe)
        {      
            IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
        }
        else
        {
            IronSourceEvents.onBannerAdLoadedEvent -= BannerAdLoadedEvent;
            IronSourceEvents.onBannerAdLoadFailedEvent -= BannerAdLoadFailedEvent;
            IronSourceEvents.onBannerAdClickedEvent -= BannerAdClickedEvent;
            IronSourceEvents.onBannerAdScreenPresentedEvent -= BannerAdScreenPresentedEvent;
            IronSourceEvents.onBannerAdScreenDismissedEvent -= BannerAdScreenDismissedEvent;
            IronSourceEvents.onBannerAdLeftApplicationEvent -= BannerAdLeftApplicationEvent;
        }
    }
    
   
    void BannerAdLoadedEvent()
    {
        this.Display_BannerIronSrc();
    }
   
    void BannerAdLoadFailedEvent(IronSourceError error)
    { //Invoked when the banner loading process has failed.
      //@param description - string - contains information about the failure.
        RequestBanner();
    }
    // Invoked when end user clicks on the banner ad
    void BannerAdClickedEvent()
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerAdScreenPresentedEvent()
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerAdScreenDismissedEvent()
    {
    }
    //Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent()
    {
    }
    //End events for Iron Source Banner


    //start handling events for iron source interstitial
    void HandleInterstitialADEventsIronSrc(bool subscribe)
    {
        if (subscribe)
        {
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        }
        else
        {
            IronSourceEvents.onInterstitialAdReadyEvent -= InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent -= InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdShowSucceededEvent -= InterstitialAdShowSucceededEvent;
            IronSourceEvents.onInterstitialAdShowFailedEvent -= InterstitialAdShowFailedEvent;
            IronSourceEvents.onInterstitialAdClickedEvent -= InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent -= InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent -= InterstitialAdClosedEvent;
        }
    }

    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        RequestInterstitial();
    }
    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
    }
    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
    }
    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()

    {
        
    }
    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
    }
}

