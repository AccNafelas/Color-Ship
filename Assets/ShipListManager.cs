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
        
        if (selectedShip != null){
            selectedShip.OnUnselect();
            selectedShip = null;
        }

        selectedShip = newship;
    }

    public void ValidateBuy(int price){

       int currentMoney = CoinsManager.instance.globalCoins;

      if (currentMoney >= price){
            //CUANTA PLATA PIBE MUY BIEN
            //CoinsManager.instance.saveCoins(CoinTransactions.pay,price);
            //selectedShip.Purchase();
            setPurchaseModalValues();

      }
        else{
          //Hacer el truqito para que se mueva
          print("POBRE DE MIERDA");
      }

    }

    public void setPurchaseModalValues()
    {
        InputManager.instance.isModalBuyOpen = true;
        confirmBuyModal.SetActive(true);
        txtPrice.text = selectedShip.BP.shipValue.ToString();
        shipImage.sprite = selectedShip.BP.shipImg;
    }

    public void OnClickYesPurchase()
    {
        CoinsManager.instance.saveCoins(CoinTransactions.pay, selectedShip.BP.shipValue);
        selectedShip.Purchase();
        confirmBuyModal.SetActive(false);
        InputManager.instance.isModalBuyOpen = false;

    }
    public void OnClickNoPurchase()
    {
        InputManager.instance.isModalBuyOpen = false;
        confirmBuyModal.SetActive(false);

    }

}
