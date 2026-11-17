using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int cointCount;
    public TMP_Text coinCountText;
    public static MoneyManager Instance;

    public void Start()
    {
        String CurrentCoinCount = cointCount.ToString("N");
        coinCountText.text = CurrentCoinCount;
    }

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

    public void Initialize()
    {
        //Initialized 1: Session Data Manager
        Debug.Log("MoneyManager Initialized");

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
