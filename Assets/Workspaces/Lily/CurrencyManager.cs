using System;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    
    public int currentCurrency = 0;
    public int minimumCurrency = 0;
    public TMP_Text currentCurrencyText;

    public int currentScore = 0;
    public TMP_Text hajScore;
    
    public bool attackUpgraded = false;
    public bool rangeUpgraded = false;
    public bool sppedUpgraded = false;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void RemoveMoney(int amount)
    {
        currentCurrency -= amount;
        if (currentCurrency < minimumCurrency)
        {
            currentCurrency = minimumCurrency;
        }
        currentCurrencyText.text = currentCurrency.ToString();
    }

    public void AddMoney(int amount)
    {
        currentCurrency += amount;
        currentCurrencyText.text = currentCurrency.ToString();
    }
    
    public void AddScore(int amount)
    {
        currentScore += amount;
        hajScore.text = currentScore.ToString();
    }
}
