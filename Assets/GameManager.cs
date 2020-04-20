using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startPanel, gamePanel, pausePanel, endPanel;

    [Header("Game Status")]
    public bool inGame=false;
    public bool paused = false;

    public UnityEvent OnStartGame;
    public UnityEvent OnPauseGame;
    public UnityEvent OnResumeGame;
    public UnityEvent OnEndGame;
    public UnityEvent OnResurectGame;

    public static GameManager instance;
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

    void Start()
    {
        startPanel.SetActive(true);

        gamePanel.SetActive(false);
        pausePanel.SetActive(false);
        endPanel.SetActive(false);

        SoundManager.instance.PlayStartMusic();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
        OnStartGame.Invoke();

        inGame = true;

        SoundManager.instance.PlayGameMusic();
    }

    public void PauseGame()
    {
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
        OnPauseGame.Invoke();
        paused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        OnResumeGame.Invoke();
        paused = false;
    }

    public void EndGame()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);

        inGame = false;

        OnEndGame.Invoke();
        ScoreManager.instance.SaveScore();
        AdvertisementManager.instance.RequestRewarded();

    }

    public void PlayerDontWantToContinue()
    {
        AdvertisementManager.instance.ShowInterestitial(false);
        //NewGame();
        SaveCoins();
    }

    public void NewGame()
    {
        //SaveCoins();
        //AdvertisementManager.instance.ShowInterestitial(false);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SaveCoins()
    {
        int score = ScoreManager.instance.currScore;

        CoinsManager.instance.saveCoins(CoinTransactions.add, score);
    }


    public void SetUpContinueGame()
    {
        StartGame();
        OnResurectGame.Invoke();
    }

   
}
