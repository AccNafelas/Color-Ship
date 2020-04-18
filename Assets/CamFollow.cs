using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public bool inPlace = true;
    public Transform objective;
    public Vector3 offset;
    public float speed = 5f;

    void Start()
    {
        //GetOffset();
    }

    void GetOffset()
    {
        offset.y = objective.transform.InverseTransformPoint(this.transform.position).y;
    }

    public float GetDistanceToObj()
    {
        return Vector3.Distance(this.transform.position, objective.position);
    }
 
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    StartMovement();
        //}

        Follow();
    }

    public void StartMovement(Transform newObj)
    {
        objective = newObj;
        inPlace = false;
    }

    void Follow()
    {
        if (inPlace || objective == null) return;

        Vector3 pos = this.transform.position;
        pos.y = objective.transform.position.y + offset.y;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, Time.deltaTime * speed);

        if (GetDistanceToObj() <= 0.1f)
        {
            inPlace = true;
        }

    }
}
