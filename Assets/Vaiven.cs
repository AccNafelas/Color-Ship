using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaiven : Obstacle
{
    public bool horizontal = true;
    public float speed = 3f;
    public float amplitude =5f;

    private float timer = 0f;
    private Vector3 initialPos;

    void Start()
    {
        
    }

   
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 deltaMove = Vector3.zero;
        if (horizontal)
        {
            float x = Mathf.Sin(timer) * amplitude;
            deltaMove.x = x;
        }
        else
        {
            float y = Mathf.Sin(timer) * amplitude;
            deltaMove.y = y;
        }

        timer += Time.deltaTime;
        Vector3 nextPos = this.transform.position;
        nextPos += (deltaMove)* Time.deltaTime ;
        this.transform.position = nextPos;

    }
}
