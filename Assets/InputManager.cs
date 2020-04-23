using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public bool canTouch = true;
    private float timer =0f;

    [Space]
    [Tooltip("In screen Percentage")]
    [Range(0f,1f)]
    public float panelHieght = 0.5f;


    [Space]
    public UnityEvent OnTouch;
    public bool paused = false;
    public bool inGame = false;

    public bool isModalBuyOpen = false;

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

        if (paused || isModalBuyOpen) return;

        if ( ( (Input.touchCount >= 1 &&Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0) ) && canTouch && !SobreUI()  && !IsInPanel())
        {
            if (!inGame)
            {
                GameManager.instance.StartGame();
                inGame = true;
            }

            OnTouch.Invoke();
        }


    }

    bool IsInPanel()
    {
        if (inGame) return false;

        Vector2 screenPos;
        if (Input.touchSupported)
        { screenPos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position); }
        else
        { screenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition); }

        //print(screenPos);

        if (screenPos.y > panelHieght)
        {
            return false;
        }
        else
            return true;
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
