
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class NihuilAdManager : MonoBehaviour
{
    public string Admob_APP_ID = "ca-app-pub-3577746380704619~2296662877";
    public string IronSrc_APP_ID = "b97d148d";


    private BannerView bannerAD;
    private InterstitialAd interstialAd;
    private RewardBasedVideoAd rewardAd;

    private int RandomNumber;

    #region Banner

   
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
    #endregion

    #region Interstitial
    void RequestInterstitial()
    {
        System.Random rand = new System.Random();
        RandomNumber = rand.Next(2);

        if (RandomNumber == 0)
        { //ADMOB
            string interstitial_ID = "ca-app-pub-3940256099942544/1033173712"; //testing purposes
            interstialAd = new InterstitialAd(interstitial_ID);

            AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            //Testing Purposes

            //For Real APP
            //AdRequest adRequest = new AdRequest.Builder().Build();
            interstialAd.LoadAd(adRequest);
        }
        else if (RandomNumber == 1)


        { //IronSource
            IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.INTERSTITIAL);
            IronSource.Agent.loadInterstitial();

        }


    }

    public void Display_InterstitialAD()
    {
        
        if (RandomNumber == 0)
        {
            if (interstialAd.IsLoaded())
            {
                interstialAd.Show();
            }
        }
        else if (RandomNumber == 1)
        {
            if (IronSource.Agent.isInterstitialReady())
            {
                IronSource.Agent.showInterstitial();
            }
        }


    }

    //start handling events for admob interstitial
    void HandleInterstitialADEventsAdMob(bool subscribe)
    {
        if (subscribe)
        {  
    // Called when an ad request has successfully loaded.
    this.interstialAd.OnAdLoaded += HandleInterAdMobOnAdLoaded;
    // Called when an ad request failed to load.
    this.interstialAd.OnAdFailedToLoad += HandleInterAdMobOnAdFailedToLoad;
    // Called when an ad is shown.
    this.interstialAd.OnAdOpening += HandleInterAdMobOnAdOpened;
    // Called when the ad is closed.
    this.interstialAd.OnAdClosed += HandleInterAdMobOnAdClosed;
    // Called when the ad click caused the user to leave the application.
    this.interstialAd.OnAdLeavingApplication += HandleInterAdMobOnAdLeavingApplication;
            //start handling events for iron source interstitial
        }
        else
        {
            // Called when an ad request has successfully loaded.
            this.interstialAd.OnAdLoaded -= HandleInterAdMobOnAdLoaded;
            // Called when an ad request failed to load.
            this.interstialAd.OnAdFailedToLoad -= HandleInterAdMobOnAdFailedToLoad;
            // Called when an ad is shown.
            this.interstialAd.OnAdOpening -= HandleInterAdMobOnAdOpened;
            // Called when the ad is closed.
            this.interstialAd.OnAdClosed -= HandleInterAdMobOnAdClosed;
            // Called when the ad click caused the user to leave the application.
            this.interstialAd.OnAdLeavingApplication -= HandleInterAdMobOnAdLeavingApplication;
            //start handling events for iron source interstitial
        }
    }

    public void HandleInterAdMobOnAdLoaded(object sender, EventArgs args)
    {
       // RequestInterstitial();
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleInterAdMobOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();

        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleInterAdMobOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleInterAdMobOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleInterAdMobOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


       





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



    #endregion

    #region VideoReward
    void RequestVideoAd()
    {
        System.Random rand = new System.Random();
        RandomNumber = rand.Next(2);

        if (RandomNumber == 0)
        {  //For Rewarded Video IRON SOURCE
            IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.REWARDED_VIDEO);
            IronSource.Agent.shouldTrackNetworkState(true);

        }
        else if(RandomNumber == 1)
        {
            
            string video_ID = "ca-app-pub-3940256099942544/5224354917"; //testing purposes
            rewardAd = RewardBasedVideoAd.Instance;

            AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            //Testing Purposes

            //For Real APP
            //AdRequest adRequest = new AdRequest.Builder().Build();


            rewardAd.LoadAd(adRequest, video_ID);
        }





    }

    public void Display_Video()
    {
 
        bool available = IronSource.Agent.isRewardedVideoAvailable();
        if (RandomNumber == 0 && available)
        {
            IronSource.Agent.showRewardedVideo();

        }else if (RandomNumber == 0 && rewardAd.IsLoaded()) {
            rewardAd.Show();
        }

    }
    void HandleVideoRewardADEventsIronSource(bool subscribe)
    {
        if (subscribe)
        {
            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        }
        else
        {
            IronSourceEvents.onRewardedVideoAdOpenedEvent -= RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent -= RewardedVideoAdClickedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent -= RewardedVideoAdClosedEvent;
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent -= RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent -= RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent -= RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent -= RewardedVideoAdShowFailedEvent;
        }
    }

    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
    }

    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for 
    // the callback from the  ironSource server.
    //@param - placement - placement object which contains the reward data
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        RequestVideoAd();
    }

    // ----------------------------------------------------------------------------------------
    // Note: the events below are not available for all supported rewarded video ad networks. 
    // Check which events are available per ad network you choose to include in your build. 
    // We recommend only using events which register to ALL ad networks you include in your build. 
    // ----------------------------------------------------------------------------------------

    //Invoked when the video ad starts playing. 
    void RewardedVideoAdStartedEvent()
    {
    }
    //Invoked when the video ad finishes playing. 
    void RewardedVideoAdEndedEvent()
    {
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement placement)
    {

    }


    
    void HandleVideoRewardADEventsAdmob(bool subscribe)
    {
        if (subscribe)
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
        RequestVideoAd();

        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
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
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }




    #endregion
}
