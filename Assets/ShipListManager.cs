using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShipListManager : MonoBehaviour
{
    public GameObject shipInfoPrefab;
    [Space]
    public List<ShipViewBluePrint> shipsData = new List<ShipViewBluePrint>();
    [Space]
    public List<ShipView> shipsViews = new List<ShipView>();

    [Space]
    public Transform parent;
    
    [Space]
    public ShipView selectedShip;

    [Header("Modal Confirm Props")]
    public GameObject confirmBuyModal;
    public Text txtPrice;
    public Image shipImage;


    [Space]
    public Plane playerShip;
    public ShipViewBluePrint original;

 public static ShipListManager instance;
    void AwakeSingleton()
    {
        if (instance == null)
            instance = this;
        else
        { Destroy(this.gameObject); }
    }

    private void Awake()
    {
        AwakeSingleton();
    }

    void Start()
    {
        
    }

    private void OnEnable()
    {
        CreateShipViewList();

        selectedShip = FindByBluePrint ( GetLastSelected() );
        selectedShip.selected = true;
    }

    private void OnDisable()
    {
        
    }

    public void CreateShipViewList()
    {
        int ind = 0;
        foreach (var item in shipsData)
        {
            var info = Instantiate(shipInfoPrefab, parent);
            var infoScript = info.GetComponent<ShipView>();
            infoScript.index = ind;
            //infoScript.BP.shipIndex = ind;


            infoScript.SetUp(item,this);
            shipsViews.Add(infoScript);
            ind++;
        }
    }

    public void ChangeSelectedShip(ShipView newship){
        
        if (selectedShip != null)
        {
            selectedShip.OnUnselect();
            selectedShip = null;
        }

        selectedShip = newship;

        ChangePlayerShipSprite(selectedShip.BP);

        SaveLastSelected();
    }

    public void ValidateBuy(int price){

       int currentMoney = CoinsManager.instance.globalCoins;

      if (currentMoney >= price){
            //CUANTA PLATA PIBE MUY BIEN
            //CoinsManager.instance.saveCoins(CoinTransactions.pay,price);
            //selectedShip.Purchase();
            SetPurchaseModalValues();

      }
      else{
          //Hacer el truqito para que se mueva
          print("POBRE DE MIERDA");
      }

    }

    public void SetPurchaseModalValues()
    {
        InputManager.instance.isModalBuyOpen = true;
        confirmBuyModal.SetActive(true);
        txtPrice.text = selectedShip.BP.shipValue.ToString();
        shipImage.sprite = selectedShip.BP.shipImg;
    }

    public void OnClickYesPurchase()
    {
        CoinsManager.instance.SaveCoins(CoinTransactions.pay, selectedShip.BP.shipValue);
        selectedShip.Purchase();
        confirmBuyModal.SetActive(false);
        InputManager.instance.isModalBuyOpen = false;

    }
    public void OnClickNoPurchase()
    {
        InputManager.instance.isModalBuyOpen = false;
        confirmBuyModal.SetActive(false);

    }


    public void CheckPlayerShip()
    {
        if (!playerShip.currBP.owned)
        {
            ChangePlayerShipSprite(original);
        }
    }

    public void ChangePlayerShipSprite(ShipViewBluePrint BP)
    {
        playerShip.StablishNewShip(BP);
    }



    public void SaveLastSelected()
    {
        if (PlayerPrefs.HasKey("LastShipSelected"))
        {
            PlayerPrefs.SetString("LastShipSelected", selectedShip.BP.name);
        }
        else
        {
            PlayerPrefs.SetString("LastShipSelected", "Original");
        }

        PlayerPrefs.Save();
    }

    public ShipViewBluePrint GetLastSelected()
    {
        string name;

        if (PlayerPrefs.HasKey("LastShipSelected"))
        {
           name = PlayerPrefs.GetString("LastShipSelected");
        }
        else
        {
            name = "Original";
        }


        foreach (var item in shipsViews)
        {
            if (item.BP.name == name)
            {
                return item.BP;
            }
        }

        return original;
    }

    public ShipView FindByBluePrint(ShipViewBluePrint BP)
    {
        foreach (var item in shipsViews)
        {
            if (item.BP.name == BP.name)
            {
                return item;
            }
        }
        return shipsViews[0];
    }

}
