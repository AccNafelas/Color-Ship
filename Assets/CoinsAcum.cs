using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinsAcum : MonoBehaviour
{
    public Text coinsGained;


    private void OnEnable()
    {
        int scoreGenerated = ScoreManager.instance.currScore;
        float currencyratio = CoinsManager.instance.moneyRatio;
        coinsGained.text ="+" + ((float)(scoreGenerated * currencyratio)).ToString();
    }

}
