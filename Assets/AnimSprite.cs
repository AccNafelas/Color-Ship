using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    public List<AnimSet> animations = new List<AnimSet>();
    public float timeChange = 0.12f;

    private float timer=0f;
    private int ind = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeChange)
        {
            GetNextStep();
            timer = 0f;
        }
    }
    public void GetNextStep()
    {
        foreach (var item in animations)
        {
            if (ind < item.sprites.Count - 1)
            {
                ind++;
            }
            else { ind = 0; }


            item.SR.sprite = item.sprites[ind];
        }
    }
}

[System.Serializable]
public class AnimSet
{
    public SpriteRenderer SR;
    public List<Sprite> sprites = new List<Sprite>();
}