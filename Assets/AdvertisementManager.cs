using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class AdvertisementManager : MonoBehaviour
{
    public bool useAds = false;
    [Tooltip("El booleano TestMode Indica si los anuncios que se van a mostrar son de Test (True:SI)")]
    public bool TestMode = false;
    [Space]
    public int totalRndNum = 10;
    [SerializeField] private int currNum;

    [Header("Banner")]
    public bool showBannerOnStart = true;
    [Tooltip("Este numero determina que tan grande es la posibilidad de usar AdMob")]
    public int percentBanner;


    [Header("Interestitial")]
    [Tooltip("Este numero determina que tan grande es la posibilidad de usar AdMob")]
    public int percentInterestitial;
    [Tooltip("Si un random de 1 a 10 es menor a este numero, NO lo muestra")]
    public int showPercent =5;

    [Header("Rewarded")]
    [Tooltip("Este numero determina que tan grande es la posibilidad de usar AdMob")]
    public int percentReward;

    [Space]
    public IronSourceManager IronSourceManager;
    public AdMOBManager AdMobManager;

    [Header("Ad loading Screen")]
    public GameObject adLoadingScreen;
    public GameObject cancelLoading;


    public static AdvertisementManager instance;

    private void Awake()
    {
        AwakeSingleton();

        AwakeConfig();

    }

    void AwakeSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Start()
    {
        if (!useAds) return;

        if(showBannerOnStart)
            ShowBanner();
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    ShowBanner();
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    ShowInterestitial();
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    ShowRewarded();
        //}
    }

    #region Banner
    public void ShowBanner()
    {
        if (!useAds) return;


        currNum = Random.Range(1, totalRndNum);
        //if (currNum > percentBanner)
        //{
        //    //Use IronSource
        //    print("Use Iron Source -> Banner");
        //    IronSourceManager.Display_BannerIronSrc();

        //}
        //else
        //{
            //Use AdMob;
            print("Use Ad Mob -> Banner");
            AdMobManager.Display_BannerAdMob();
        //}
    }

    #endregion

    #region Interestitial
    public void ShowInterestitial(bool obligatory = true)
    {
        if (!useAds) return;


        if (!obligatory)
        {
            int n = Random.Range(0, 10);
            if (n > showPercent)
            {
                print("DONT Show Interestitial +++" + n);
                return;
            }
        }

        currNum = Random.Range(1, totalRndNum);
        if (currNum > percentInterestitial)
        {
            //Use IronSource
            print("Use Iron Source ->Interstitial");
            ShowLoadingScreen();
            IronSourceManager.Display_InterstitialAD();
            
        }
        else
        {
            //Use AdMob;
            print("Use Ad Mob ->Interstitial");
            ShowLoadingScreen();
            AdMobManager.Display_InterstitialAD();
           
        }
    }
    #endregion

    #region Rewarded
    private int SourceSelected = 0;
    private bool requested = false;
    public void RequestRewarded()
    {
        if (!useAds) return;


        requested = false;
        currNum = Random.Range(1, totalRndNum);
        if (currNum > percentReward)
        {
            requested = true;
            SourceSelected = 1;
            IronSourceManager.RequestVideoAd();
        }
        else
        {
            requested = true;
            SourceSelected = 2;
            AdMobManager.RequestVideoAd();
        }

        
    }

    public bool IsRewardedVideoAvailable()
    {
        if (!requested) return false;

        bool status = false;

        switch (SourceSelected)
        {
            case 1:
                status = IronSourceManager.IsRewardedVideoReady();
                break;
            case 2:
                status = AdMobManager.RewardedVideoReady();
                break;
            default:
                print("Que pasó? No puedo mostrar nada amigo");
                status = false;
                break;
        }

        return status;
    }

    public void ShowRewardedVideo()
    {
        if (!requested) RequestRewarded();

        switch (SourceSelected)
        {
            case 1:
                ShowRewarded_IronSource();
                break;
            case 2:
                ShowRewarded_AdMob();
                break;
            default:
                print("Que pasó? No puedo mostrar nada amigo");
                break;
        }

    }

    void ShowRewarded_IronSource()
    {
        //Use IronSource
        print("Use Iron Source ->Rewarded");
        ShowLoadingScreen(true);
        IronSourceManager.Display_Video();
        

    }

    void ShowRewarded_AdMob()
    {
        //Use AdMob;
        print("Use Ad Mob ->Rewarded");
        ShowLoadingScreen(true);
        AdMobManager.Display_Video();
        
    }
    #endregion


    #region Loading Screen
    public void CancelRewardAd()
    {
        AdMobManager.rewardedCanceled = true;
        IronSourceManager.rewardedCanceled = true;
    }

    public void ShowLoadingScreen()
    {
        adLoadingScreen.SetActive(true);

        cancelLoading.SetActive(false);
    }

    public void ShowLoadingScreen(bool isLoadingRewarded)
    { 
        adLoadingScreen.SetActive(true);

        if (isLoadingRewarded)
        {
            cancelLoading.SetActive(true);
        }
        else
        {
            cancelLoading.SetActive(false);
        }
    }

    public void HideLoadingScreen()
    {
        adLoadingScreen.SetActive(false);
    }
    #endregion


    #region Remote Config

    public struct userAttributes { }
    public struct appAttributes { }


    void AwakeConfig()
    {
        ConfigManager.FetchCompleted += HandleFetch;
        FetchChanges();
    }

    public void FetchChanges()
    {
        ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
    }

    void HandleFetch(ConfigResponse response)
    {
        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:

                break;
            case ConfigOrigin.Cached:

                break;
            case ConfigOrigin.Remote:

                this.useAds = ConfigManager.appConfig.GetBool("Use Ads");
                this.TestMode = ConfigManager.appConfig.GetBool("Testing");

                this.percentBanner = ConfigManager.appConfig.GetInt("AdMob_Banner");
                this.percentInterestitial = ConfigManager.appConfig.GetInt("AdMob_Inter");
                this.percentReward = ConfigManager.appConfig.GetInt("AdMob_Rewarded");

                break;
            default:

                break;
        }

    }

    private void OnDestroy()
    {
        ConfigManager.FetchCompleted -= HandleFetch;

    }

    #endregion
}
