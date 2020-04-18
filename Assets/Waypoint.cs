using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public int playerLayer=10;

    public bool colorChanger = false;
    public ColorInfo color;
    public SpriteRenderer[] sprite;
    public SpriteRenderer centerSprite;

    private void OnEnable()
    {
        StageContainer container = this.transform.parent.gameObject.GetComponent<StageContainer>();

        if (!container.points.Contains(this.transform))
        {
            container.points.Add(this.transform);
        }

        if (colorChanger)
        {
            //print("COLOR CHANGER");
            for (int i = 0; i < sprite.Length; i++)
            {
                Color c = color.color;
                c.a = sprite[i].color.a;
                sprite[i].color = c;

            }
            centerSprite.enabled = true;
        }
        else
        {
            centerSprite.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            StagesManager.instance.ChangeStage(this.transform.parent.gameObject);
            this.gameObject.SetActive(false);
        }


    }

}
