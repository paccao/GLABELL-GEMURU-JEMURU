using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currentCurrency;
    public int minimumCurrency = 0;

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
