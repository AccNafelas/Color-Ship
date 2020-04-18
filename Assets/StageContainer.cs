using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageContainer : MonoBehaviour
{

    public StageDifficulty difficulty;
    public List<Transform> points = new List<Transform>();
    public float height = 10f;

    [Space]
    public Transform playerPos;
    public float distToDelete = 15f;

    [Space]
    public TextMesh numberMesh;

    void Start()
    {
        numberMesh.gameObject.SetActive(false);
        numberMesh.text = this.gameObject.name;
    }

    private void OnEnable()
    {

    }

    public void InitializeStage()
    {
        AssingPointsToManager();
        GetPlayer();
    }

    public void AssingPointsToManager()
    {
        WayPointsManager.instance.AddWaypoints(points);
    }

    void GetPlayer()
    {
        playerPos = StagesManager.instance.player;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckDistanceY();
    }

    void CheckDistanceY()
    {
        if (DistY() > distToDelete)
        {
            //Debug.Log("Bye Bye: " + this.gameObject.name);
            StagesManager.instance.stagesActivesAmount--;
            Destroy(this.gameObject);
        }
    }

    public float DistY()
    {

        return playerPos.transform.position.y - this.transform.position.y;

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = this.transform.position;
        Vector3 size = new Vector3 (5f,height,10f);
        Gizmos.DrawWireCube(center, size);
    }

}
