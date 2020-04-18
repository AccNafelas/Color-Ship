using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdPanelLoader : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public Text noAdsText;
    public Image yesButtonImg;
    public string sorryNoAds = "NO Ads Available, Sorry";

    void Start()
    {
        
    }

    private void OnEnable()
    {

        if (AdvertisementManager.instance.IsRewardedVideoAvailable())
        {
            AdReady();
        }
        else
        {
            AdNoReady();
        }
    }

    void AdReady()
    {
        yesButton.interactable = true;
        noAdsText.enabled = false;
        noAdsText.text = sorryNoAds;
    }

    void AdNoReady()
    {
        yesButton.interactable = false;
        noAdsText.enabled = true;
        noAdsText.text = sorryNoAds;
    }


    void Update()
    {
        
    }
}
