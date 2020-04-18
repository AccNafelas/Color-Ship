using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorElement : MonoBehaviour
{
    public ColorInfo color;

    public List<SpriteRenderer> sprite;

    private void OnEnable()
    {
        if (sprite.Count <= 0)
        {
            sprite[0] = this.GetComponent<SpriteRenderer>();

          
        }

        foreach (var item in sprite)
        {
            item.color = color.color;
        }
        

    }
}
