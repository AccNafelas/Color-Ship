using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorInfo", menuName = "Color Info/Color")]
public class ColorInfo : ScriptableObject
{
    public int index;
    public Color color;
    public new string name;

}
