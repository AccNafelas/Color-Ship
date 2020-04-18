using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanelManager : MonoBehaviour
{
    //public GameObject normalPanel;
    //public GameObject AdPanel;



    //void Start()
    //{

    //}

    //private void OnEnable()
    //{
    //    ShowNormalPanel();
    //}

    //public void ShowNormalPanel()
    //{
    //    normalPanel.SetActive(true);
    //    AdPanel.SetActive(false);
    //}

    //public void ShowAdPanel()
    //{
    //    normalPanel.SetActive(false);
    //    AdPanel.SetActive(true);
    //}

    public Button watchAd;
    public GameObject loadingImg;
    public GameObject readyImg;
    public GameObject noAdsImg;

    public float maxWaitTime = 20f;

    [Space]
    public Text currScore;
    public Text topScore;
    public GameObject newScroeText;

    private void OnEnable()
    {
        StartCoroutine(WaitForAdAvailable());
        //ActivateCorrectImg(2);

        SetCurrScore();
        SetMaxScore();
        GetNewRecord();
    }

    void DetermineAd()
    {
        if (AdvertisementManager.instance.IsRewardedVideoAvailable())
        {
            //watchAd.gameObject.SetActive(true);
            watchAd.enabled = true;
            ActivateCorrectImg(2);

        }
        else
        {
            //watchAd.gameObject.SetActive(false);
            watchAd.enabled = false;
            ActivateCorrectImg(3);

        }

    }

    IEnumerator WaitForAdAvailable()
    {
        float t = 0f;
        watchAd.enabled = false;

        if (!AdvertisementManager.instance.useAds)
        {
            ActivateCorrectImg(3);

        }
        else
        {

            while (t < maxWaitTime && !AdvertisementManager.instance.IsRewardedVideoAvailable())
            {
                t += Time.deltaTime;
                ActivateCorrectImg(1);
                yield return new WaitForEndOfFrame();
            }
            DetermineAd();
        }

    }
    /// <summary>
    /// 1=> Loading, 2=> Ready , 3=> No ads available
    /// </summary>
    /// <param name="i"> indice</param>
    void ActivateCorrectImg(int i)
    {
        switch (i)
        {
            case 1:
                loadingImg.SetActive(true);
                readyImg.SetActive(false);
                noAdsImg.SetActive(false);
                break;

            case 2:
                loadingImg.SetActive(false);
                readyImg.SetActive(true);
                noAdsImg.SetActive(false);
                break;

            case 3:
                loadingImg.SetActive(false);
                readyImg.SetActive(false);
                noAdsImg.SetActive(true);
                break;

            default:
                loadingImg.SetActive (true);
                readyImg.SetActive(false);
                noAdsImg.SetActive(false);

                break;
        }

    }


    //Score
    void SetCurrScore()
    {
        currScore.text = ScoreManager.instance.currScore.ToString();
    }

    void SetMaxScore()
    {
        topScore.text = ScoreManager.instance.GetLoadedScore().ToString();
    }

    void GetNewRecord()
    {
        newScroeText.SetActive(false);

        if (ScoreManager.instance.currScore> ScoreManager.instance.GetLoadedScore())
        {
            newScroeText.SetActive(true);
        }
    }
}
