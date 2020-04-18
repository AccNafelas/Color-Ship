using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipListManager : MonoBehaviour
{
    public GameObject shipInfoPrefab;
    [Space]
    public List<ShipViewBluePrint> shipsData = new List<ShipViewBluePrint>();
    [Space]
    public List<ShipView> sipsViews = new List<ShipView>();

    [Space]
    public Transform parent;



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

            infoScript.SetUp(item);

            ind++;
        }
    }

}
