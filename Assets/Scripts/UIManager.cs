using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //Canvases
    public Canvas pauseCanvas;
    public Canvas resultsCanvas;
    public Canvas gameTimeCanvas;
    public Canvas countDownCanvas;

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
        HidePauseCanvas();
        HideResultsCanvas();

        //maybe these are controlled in game time manager
        ShowCountDownCanvas(); //enable first, to count down to  
        ShowGameTimeCanvas(); //this
    }

    //Canvas Actions
    #region Canvas Actions
    //Pause Canvas
    public void ShowPauseCanvas()
    {
        pauseCanvas.enabled = true;
        gameTimeCanvas.enabled = false;
    }
    public void HidePauseCanvas()
    {
        pauseCanvas.enabled = false;
        gameTimeCanvas.enabled = true;
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
