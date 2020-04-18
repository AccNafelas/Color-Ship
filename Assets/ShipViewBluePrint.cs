using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship Info", menuName = "Ship Info/Ships")]
public class ShipViewBluePrint : ScriptableObject
{
    public bool owned = false;
    public GameObject shipPrefab;
    public Sprite shipImg;
    public int shipValue = 100;

}
