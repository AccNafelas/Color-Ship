using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();

    public static WayPointsManager instance;

    void AwakeSingleton()
    {
        if (instance == null)
            instance = this;
        else
        { Destroy(this.gameObject); }
    }
    private void Awake()
    {
        AwakeSingleton();
    }

    private void Start()
    {
      
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    AddWaypoints(add);
        //}
    }


    public Transform GetFirstPoint()
    {
        return waypoints[0];
    }

    public void HandleReachPos()
    {
        waypoints.RemoveAt(0);
    }

    public void AddWaypoints(List<Transform> points)
    {
        List<Transform> ordered = points;

        //order by Y ASC
        for (int i = 1; i < ordered.Count; i++)
        {
            for (int j = 0; j < ordered.Count-1; j++)
            {
                if (ordered[i].position.y <= ordered[j].position.y)
                {
                    Transform temp = ordered[i];
                    ordered[i] = ordered[j];
                    ordered[j] = temp;
                }
            }

           points[i].gameObject.name = points[i].parent.gameObject.name + "_" + i;
        }

        waypoints.AddRange(ordered);
    }
}
