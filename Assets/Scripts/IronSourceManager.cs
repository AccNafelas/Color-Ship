using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IronSourceManager : MonoBehaviour
{
    public string IronSrc_APP_ID = "bc6dee3d";

    [Header("Banner")]
    public UnityEvent OnCloseBanner;
    [Header("Interestitial")]
    public UnityEvent OnCloseInsterestetitial_IronSource;
    public UnityEvent OnInterestitialFailed_IronSource;
    [Header("Rewarded")]
    public UnityEvent OnEndRewarde_IronSource;
    public UnityEvent OnFailedRewarded_IronSource;
    //public bool rewardedReady = false;

    //[Space]
    //public Image interestitialReady;
    //public Text timeText;


    //public void RequestAll()
    //{
    //    RequestBanner();
    //    RequestInterstitial();
    //    RequestVideoAd();
    //}

    void Start()
    {

        if (!AdvertisementManager.instance.useAds) return;

        //RequestBanner();

        //IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.INTERSTITIAL);
    }

    private void OnEnable()
    {
        HandleInterstitialADEventsIronSrc(true);
        HandleRewardedEventIronSrc(true);
    }

    private void OnDisable()
    {
       HandleInterstitialADEventsIronSrc(false);
       HandleRewardedEventIronSrc(false);
    }

    #region Banner
    void RequestBanner()
    {
        //Iron Source Banner
        IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.BANNER);
        IronSource.Agent.loadBanner(new IronSourceBannerSize(320, 50), IronSourceBannerPosition.BOTTOM);

        //IronSource.Agent.displayBanner();

      

        //5a8b653e1d7a1537738a222303914142    ironsrc Secret Key


        //bloque real ca-app-pub-3577746380704619/1087522985


       

    }

    public void Display_BannerIronSrc()
    {
        RequestBanner();
        IronSource.Agent.displayBanner();
    }

    

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
        // this.Display_BannerIronSrc();
        //RequestBanner();
    }

    void BannerAdLoadFailedEvent(IronSourceError error)
    { //Invoked when the banner loading process has failed.
      //@param description - string - contains information about the failure.

        //RequestBanner();

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
        OnCloseBanner.Invoke();
    }
    //Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent()
    {
    }
    //End events for Iron Source Banner

    #endregion

    #region Interestitial
    void RequestInterstitial()
    {
        if (IronSource.Agent.isInterstitialReady()) return;
        //IronSource
        IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.INTERSTITIAL);
        IronSource.Agent.loadInterstitial();

    }

    private float maxWaitForInterestitial = 10f;

    public void Display_InterstitialAD()
    {
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

        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            RequestInterstitial();
            while (!IronSource.Agent.isInterstitialReady() && (t < maxWaitForInterestitial))
            {
                t += Time.deltaTime;
                //timeText.text = t.ToString ();
                yield return new WaitForEndOfFrame();
            }

            if (IronSource.Agent.isInterstitialReady())
            { IronSource.Agent.showInterstitial(); }
            else
            {
                OnInterestitialFailed_IronSource.Invoke();
            }
        }
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
        OnCloseInsterestetitial_IronSource.Invoke();
        RequestInterstitial();
    }
    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
        //interestitialReady.color = Color.green;
    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
    }
    #endregion

    #region Rewarded
    public void HandleRewardedEventIronSrc(bool suscribe)
    {
        if (suscribe)
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


    //shall request when player die
    public void RequestVideoAd()
    {
        if(IronSource.Agent.isRewardedVideoAvailable())
        return;

        //For Rewarded Video IRON SOURCE
        IronSource.Agent.init(IronSrc_APP_ID, IronSourceAdUnits.REWARDED_VIDEO);
        IronSource.Agent.shouldTrackNetworkState(true);

    }

    public bool IsRewardedVideoReady()
    {
        return IronSource.Agent.isRewardedVideoAvailable();
    }
   
    public void Display_Video()
    {
        StartCoroutine(WaitForRewarded());
        //if (IronSource.Agent.isRewardedVideoAvailable())
        //    IronSource.Agent.showRewardedVideo();
    }


    public bool rewardedCanceled = false;
    IEnumerator WaitForRewarded()
    {
        rewardedCanceled = false;
        float t = 0f;

        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            //RequestVideoAd();
            while (!IronSource.Agent.isRewardedVideoAvailable() && (t < 10) && !rewardedCanceled)
            {
                t += Time.deltaTime;
                //timeText.text = t.ToString();
                yield return new WaitForEndOfFrame();
            }

            if (IronSource.Agent.isRewardedVideoAvailable() && !rewardedCanceled)
            { IronSource.Agent.showRewardedVideo(); }
            else
            {
                //OnFailedRewarded_IronSource.Invoke();
                AdvertisementManager.instance.HideLoadingScreen();
            }
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
        OnEndRewarde_IronSource.Invoke();
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

        OnFailedRewarded_IronSource.Invoke();
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
    #endregion
}