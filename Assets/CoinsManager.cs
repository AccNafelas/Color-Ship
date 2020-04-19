using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinsManager : MonoBehaviour
{
    public GameObject myCoinsUI;
    public Text currentCoins;
    private int globalCoins;
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
        getCurrentCoins();
    }

    public void getCurrentCoins(){
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
       currentCoins.text = globalCoins.ToString();
         }



    public void showCoins(){
    myCoinsUI.gameObject.SetActive(true);

    }

    public void hideCoins(){
    myCoinsUI.gameObject.SetActive(false);    
    }
    
    public void saveCoins(CoinTransactions trans, int value)
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

    }
   
}

public enum CoinTransactions
{
    pay,add,none
}
