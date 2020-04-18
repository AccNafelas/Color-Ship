using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Obstacle
{
    public bool changeDir = false;
    public float speedRot = 5f;

    [Space]
    public float timeToChange = 3f;
    private float timer = 0f;
    public int right = 1;

    void Start()
    {
        
    }

   
    void Update()
    {
        Rot();
    }

    void Rot()
    {
        if (changeDir)
        {
            timer += Time.deltaTime;

            if (timer >= timeToChange)
            {
                right *= -1;
                timer = 0f;
            }
        }

        this.transform.Rotate(0, 0, speedRot * Time.deltaTime * right);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, 1.1f);
    }
}
