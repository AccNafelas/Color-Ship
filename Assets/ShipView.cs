using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipView : MonoBehaviour
{
    public int index;
    public ShipViewBluePrint BP;

    public bool selected = false;

    [Space]
    public Image shipImg;
    public Text valueTxt;
    public Image selectedCovert;
    public Image coinImg;
    public Image fullMarcImg;
    public Image priceMarcImg;



    void Start()
    {
        
    }

    private void OnEnable()
    {

    }

    public void SetUp(ShipViewBluePrint newBP)
    {
        this.BP = newBP;
        if (BP == null)
        {
            print("No tengo BluePrint");
            return;
        }

        shipImg.sprite = BP.shipImg;

        if (BP.owned)
        {
            valueTxt.gameObject.SetActive(false);
            coinImg.gameObject.SetActive(false);

            fullMarcImg.gameObject.SetActive (true);
            priceMarcImg.gameObject.SetActive(false);
        }
        else
        {
            valueTxt.gameObject.SetActive(true);
            valueTxt.text = BP.shipValue.ToString();
            coinImg.gameObject.SetActive(true);

            fullMarcImg.gameObject.SetActive(false);
            priceMarcImg.gameObject.SetActive(true);
        }
      
    }

    public void ShowCovert()
    {
        selectedCovert.gameObject.SetActive(true);

        valueTxt.gameObject.SetActive(false);
        coinImg.gameObject.SetActive(false);

        CoinsManager.instance.hideCoins();
    }

    public void HideCovert()
    {
        selectedCovert.gameObject.SetActive(false);

        valueTxt.gameObject.SetActive(true);
        coinImg.gameObject.SetActive(true);

        CoinsManager.instance.showCoins();

    }


    public void OnSelect()
    {
        selected = true;

        ShowCovert();
    }

    public void OnUnselect()
    {
        selected = false;

        HideCovert();
    }

}
