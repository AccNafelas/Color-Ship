using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBanner : Obstacle
{
    public bool changeDir = false;
    public float speed=5f;
    public int dir =1;
    public float timeToChange = 3f;
    private float timer=0f;
    [Space]
    public float childSize = 0.5f;
    public float screenLimit = 5f;

    public List<GameObject> children = new List<GameObject>();


    void Start()
    {
        CompleteList();
    }

    void CompleteList()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children.Add(this.transform.GetChild(i).gameObject);
        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            print(GetFirstLeft().name);
        }

        Move();
    }

    void Move()
    {
        foreach (var item in children)
        {
            if (dir == 1)
            {
                if (item.transform.position.x > screenLimit)
                {
                    item.transform.position = GetFirstLeft().transform.position - new Vector3(childSize, 0, 0);
                }
            }
            else
            {
                if (item.transform.position.x < -screenLimit)
                {
                    item.transform.position = GetFirstRight().transform.position - new Vector3(-childSize, 0, 0);
                }
            }

            item.transform.Translate(Vector3.right * speed * dir * Time.deltaTime);

        }

        if (changeDir)
        {
            timer += Time.deltaTime;
            if (timer > timeToChange)
            {
                timer = 0f;
                dir *= -1;
            }
        }
    }

    GameObject GetFirstLeft()
    {
        GameObject obj = children[0];

        foreach (var item in children)
        {
            if (item.transform.position.x < obj.transform.position.x)
            {
                obj = item;
            }
        }
        return obj;
    }

    GameObject GetFirstRight()
    {
        GameObject obj = children[0];

        foreach (var item in children)
        {
            if (item.transform.position.x > obj.transform.position.x)
            {
                obj = item;
            }
        }
        return obj;

    }
}
