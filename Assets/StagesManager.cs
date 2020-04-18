using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : MonoBehaviour
{
    public Transform player;
    public GameObject currentStage;
    [Space]
    public List<GameObject> allStages = new List<GameObject>();
    public List<StageContainer> easy = new List<StageContainer>();
    public List<StageContainer> normal = new List<StageContainer>();
    public List<StageContainer> hard = new List<StageContainer>();

    [Space]
    public float maxHeight=0f;
    public int stagesActivesAmount=0;
    [SerializeField] private int inGameAmount = 4;

    [Space]
    public List<StageContainer> initials = new List<StageContainer>();


    public static StagesManager instance;
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
        SetUpLists();
      
    }

    void Start()
    {
        InitializeFirstOnes();
    }

    void InitializeFirstOnes()
    {
        foreach (var item in initials)
        {
            item.InitializeStage();
            maxHeight += item.height;
            stagesActivesAmount++;
        }
    }

    void SetUpLists()
    {
        foreach (var i in allStages)
        {
            var item = i.GetComponent<StageContainer>();
            if (i == null)
            {
                Debug.LogAssertion(i.gameObject.name + "Is not a StageContainer");
            }

            switch (item.difficulty)
            {
                case StageDifficulty.easy:
                    easy.Add(item);

                    break;
                case StageDifficulty.normal:
                    normal.Add(item);

                    break;
                case StageDifficulty.hard:
                    hard.Add(item);

                    break;
                default:
                    print(item.gameObject.name + "  dont have difficulty Level");
                    break;
            }

        }
    }

    public void GenerateNewStage()
    {
        //Replace by Difficulty adjustment
        int rnd = Random.Range(0, allStages.Count-1);

        GameObject s = Instantiate(allStages[rnd].gameObject);
        var stageInfo = s.GetComponent<StageContainer>();
        s.transform.position = new Vector3(0f, maxHeight + stageInfo.height/2, 0f);
        //s.gameObject.SetActive(true);

        stageInfo.InitializeStage();

        maxHeight += stageInfo.height;

        stagesActivesAmount++;
    }

    
  
    void Update()
    {
       

    }

    public void ChangeStage(GameObject go)
    {
        if (go == currentStage)
            return;

        //define next Current
        this.currentStage = go;

        //generate new Stage
        while (stagesActivesAmount <= inGameAmount)
        {
            GenerateNewStage();
        }

    }



    //public bool PlayerPassedCurrent()
    //{
    //    if (player.transform.position.y >= currentStage.transform.position.y)
    //    {
    //        return true;
    //    }
    //    else return false;
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(0, maxHeight, 0), Vector3.one);
    }
}

//[System.Serializable]
//public class StageInfo
//{
//    public StageDifficulty difficulty;
//    public List<Transform> points = new List<Transform>();
//    public GameObject container;
//    public float height = 10f;
//}

public enum StageDifficulty
{
    easy,
    normal,
    hard
}
