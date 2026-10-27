using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    //set up the count up clock( on awake 
    // a 3 2 1 timmer before the clock starts counting
    //set up the 3 2 1 in UI manager
    //pause the clock on pause (escPressed)S
    //stop the clock on results (spaceBarPress)
    //Text

    public static GameTimeManager Instance;

    //Count Down
    public int countDownTime;
    public TMP_Text countDownText;

    //Timer
    public TMP_Text gameTimeText;

    public TimeSpan timePlaying;
    public bool isTimerGoing;

    public float countUpTime;

    public void Awake()
    {
        //Singleton
        if (Instance == null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        gameTimeText.text = "00:00:00";
        isTimerGoing = false;

        StartCoroutine(CountdownToStart());

    }

    IEnumerator CountdownToStart()
    {
        //show count down canavas
        UIManager.Instance.ShowCountDownCanvas();

        //if count down time is greater then 0
        while (countDownTime > 0)
        {
            //set count text to count time and print to string
            countDownText.text = countDownTime.ToString();
            //wait for 1 secont
            yield return new WaitForSeconds(1f);

            //count down
            countDownTime--;
        }

        //when it finally reaches 0 ...
        countDownText.text = "GO!";

        //start timmer(WIP)
        
        //wait 1 second
        yield return new WaitForSeconds(1f);

        //hide count down canvas
        UIManager.Instance.HideCountDownCanvas();

        //start the timer
        StartTimer();
    }

    IEnumerator CountUpForever()
    {
        UIManager.Instance.ShowGameTimeCanvas();

        while (isTimerGoing)
        {
            //increase every frame
            countUpTime += Time.deltaTime;

            //
            timePlaying = TimeSpan.FromSeconds(countUpTime);

            //display to screen
            string timePlayingString = "" + timePlaying.ToString("hh':'mm':'ss");

            //set the text to time playing string
            gameTimeText.text = timePlayingString;

            yield return null;
        }
    }

    public void StartTimer()
    {
        isTimerGoing = true;
        countUpTime = 0;

        StartCoroutine(CountUpForever());
    }

    public void EndTimer()
    {
        isTimerGoing = false;
    }

    public void ResumeTimer()
    {
        if (!isTimerGoing)
        {
            isTimerGoing= true;
            StartCoroutine(CountUpForever());
        }
    }

    public void EndTimerAndShowResults()
    {

    }
}
