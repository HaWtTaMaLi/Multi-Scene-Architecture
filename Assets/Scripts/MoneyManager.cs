using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int cointCount;
    public TMP_Text coinCountText;

    public void Start()
    {
        String CurrentCoinCount = cointCount.ToString("N");
        coinCountText.text = CurrentCoinCount;
    }
}
