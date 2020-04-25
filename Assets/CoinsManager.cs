using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinsManager : MonoBehaviour
{
    public GameObject myCoinsUI;
    public Text currentCoins;
    public int globalCoins;
    public float moneyRatio = 5;

    public static CoinsManager instance;

    private void Awake()
    {
        AwakeSingleton();

    }

    void AwakeSingleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }
    private void OnEnable()
    {
        GetCurrentCoins();
    }

    public void GetCurrentCoins()
    {
        int myCoins = 0;
        if (PlayerPrefs.HasKey("myCoins"))
        {
            myCoins = PlayerPrefs.GetInt("myCoins");

        }
        else
        {
            PlayerPrefs.SetInt("myCoins", 0);
            PlayerPrefs.Save();
        }

        globalCoins = myCoins;
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI()
    {
        currentCoins.text = globalCoins.ToString();
    }


    public void ShowCoins() {
        myCoinsUI.gameObject.SetActive(true);

    }

    public void HideCoins() {
        myCoinsUI.gameObject.SetActive(false);
    }

    public void SaveCoins(CoinTransactions trans, int value)
    {
        switch (trans)
        {
            case CoinTransactions.pay:
                globalCoins -= value;
                break;
            case CoinTransactions.add:
                value = (int)(value * moneyRatio);
                globalCoins += value;
                break;
            case CoinTransactions.none:
                globalCoins += 0;
                break;
            default:
                break;
        }

        PlayerPrefs.SetInt("myCoins", globalCoins);
        PlayerPrefs.Save();

        UpdateCoinsUI();

    }


    [ContextMenu("Add Money")]
    public void AddMoney()
    {
        globalCoins += 1000;

        PlayerPrefs.SetInt("myCoins", globalCoins);
        PlayerPrefs.Save();

        GetCurrentCoins();
    }
}

public enum CoinTransactions
{
    pay,add,none
}
