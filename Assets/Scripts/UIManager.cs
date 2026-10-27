using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //Canvases
    public Canvas pauseCanvas; //1
    public Canvas resultsCanvas; //2
    public Canvas countDownCanvas; //3

    //currently working on (WIP)
    public Canvas gameTimeCanvas;

    public void Awake()
    {
        //Singleton
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //On Awake
        HideResultsCanvas();

        //maybe these are controlled in game time manager
        //ShowCountDownCanvas(); //enable first, to count down to  
        ShowGameTimeCanvas(); //this
    }

    //Canvas Actions
    #region Canvas Actions
    //Pause Canvas
    public void ShowPauseCanvas()
    {
        //made pause ui toggle
        if (pauseCanvas.enabled == false)
        {
            pauseCanvas.enabled = true;
            //gameTimeCanvas.enabled = false;
        }
        else
        {
            pauseCanvas.enabled = false;
            //gameTimeCanvas.enabled = true;
        }
    }

    //Results Canvas
    public void ShowResultsCanvas()
    {
        resultsCanvas.enabled = true;
    }
    public void HideResultsCanvas()
    {
        resultsCanvas.enabled = false;
    }

    //Game Time Canvas
    public void ShowGameTimeCanvas()
    {
        gameTimeCanvas.enabled = true;
    }
    public void HideGameTimeCanvas()
    {
        gameTimeCanvas.enabled = false;
    }

    //Count Down Canvas
    public void ShowCountDownCanvas()
    {
        countDownCanvas.enabled = true;
    }
    public void HideCountDownCanvas()
    {
        countDownCanvas.enabled = false;
    }
    #endregion
}
