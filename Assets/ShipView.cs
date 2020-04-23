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
    public GameObject selectedFrame;

    [HideInInspector]
    public ShipListManager listManager;


    void Start()
    {
        
    }

    private void OnEnable()
    {

    }

    public void IsOwned()
    {
        string nombre = "Ship-" + BP.shipIndex;
        if (PlayerPrefs.HasKey(nombre))
        {
            if (PlayerPrefs.GetInt(nombre) == 1)
            {
                BP.owned = true;

                //return true;
            }
            else
            {
                BP.owned = false;
                //return false;
            }
        }
        else
        {
            BP.owned = false;
            //return false;
        }

    }

    public void SetUp(ShipViewBluePrint newBP,ShipListManager manager)
    {
        this.listManager = manager;

        this.BP = newBP;
        if (BP == null)
        {
            print("No tengo BluePrint");
            return;
        }

        BP.shipIndex = index;
        IsOwned();

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

       // CoinsManager.instance.hideCoins();
    }

    public void HideCovert()
    {
        selectedCovert.gameObject.SetActive(false);

        valueTxt.gameObject.SetActive(true);
        coinImg.gameObject.SetActive(true);

        //CoinsManager.instance.showCoins();

    }

    public void HandleButtonDown()
    {
        if (!selected)
        {
           OnSelect();
        }
        else
        {
            if (BP.owned)
            {
                return;
            }
            else
            {
                OnBuy();
            }
        //Comprar, validar jeje
        }
    }


    public void OnSelect()
    {
        listManager.ChangeSelectedShip(this);
        selected = true;


        // si ay la tengo no muestres lña cobertura, sino la de seleccion
        ShowCovert();
    }

    public void OnUnselect()
    {
        selected = false;

        HideCovert();
    }
    //[ContextMenu("Purchase")]
    public void Purchase()
    {
        string nombre = "Ship-" + BP.shipIndex;
        
        PlayerPrefs.SetInt(nombre, 1);
        PlayerPrefs.Save();
        
        BP.owned = true;
        SetUp(BP,this.listManager);

    }

     public void OnBuy(){

         listManager.ValidateBuy(BP.shipValue);
        
    }

 

   

}
