using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    [Header("Save Settings")]
    public string loadName;
    public int currScore=0;
    public int loadedScore = 0;

    [HideInInspector] public bool newTopScore;

    [Header("UI")]
    public Text scoreText;

    public static ScoreManager instance;
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
        GetLoadedScore();
    }

    public void UpdateScore(int amount)
    {
        currScore += amount;

        scoreText.text = currScore.ToString("000");
    }

    public int GetLoadedScore()
    {
        if (PlayerPrefs.HasKey(loadName))
        {
            loadedScore = PlayerPrefs.GetInt(loadName);
            //print("Max Score Loaded");
        }
        else
        {
            loadedScore = 0;
            print("Max Score couldnt be load");
        }
        return loadedScore;
    }

    public void SaveScore()
    {
        if (currScore > GetLoadedScore())
        {
            newTopScore = true;

            PlayerPrefs.SetInt(loadName, currScore);

            print("Max Score Saved: " + currScore);
            PlayerPrefs.Save();
        }

    }

}
