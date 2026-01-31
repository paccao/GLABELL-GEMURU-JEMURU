using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    
    public int currentCurrency;
    public int minimumCurrency = 0;

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
    }

    public void AddMoney(int amount)
    {
        currentCurrency += amount;
    }
}
