using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public bool canTouch = true;
    private float timer =0f;


    public UnityEvent OnTouch;
    public bool paused = false;
    public bool inGame = false;


    public static InputManager instance;
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
        
    }

    
    void Update()
    {

        if (paused) return;

        if ( ( (Input.touchCount >= 1 &&Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0) ) && canTouch && !SobreUI() )
        {
            if (!inGame)
            {
                GameManager.instance.StartGame();
                inGame = true;
            }

            OnTouch.Invoke();
        }


    }

    public void EnterPuase()
    {
        paused = true;
    }

    public void EndPause()
    {
        paused = false;
    }


    [Header("OverUI")]
    #region over UI
    public SobreUI[] EnUI;
    bool SobreUI()
    {
        bool OverUIElement = false;

        for (int i = 0; i < EnUI.Length; i++)
        {
            if (OverUIElement)
                continue;
            OverUIElement = EnUI[i].EnUI();

        }
        //print(OverUIElement);
        return OverUIElement;
        //		return EnUI.EnUI();
    }
    #endregion
}
