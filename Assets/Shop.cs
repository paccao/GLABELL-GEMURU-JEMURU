using UnityEngine;

public class Shop : MonoBehaviour
{
    public CurrencyManager currencyManager;
    
    private bool damageBought = false;
    private bool rangeBought = false;
    private bool speedBought = false;
    public int damagePrice = 30;
    public int rangePrice = 50;
    public int speedPrice = 20;
    public AffärsLjud ljud;
    public GameObject rangeUpgrade;
    public GameObject speedUpgrade;
    public GameObject damageUpgrade;

    void Awake()
    {
        currencyManager = CurrencyManager.Instance;
        
        
    }

    public void Start()
    {
        if (currencyManager.attackUpgraded)
        {
            damageUpgrade.SetActive(false);
            damageBought = true;
        }
        
        if (currencyManager.sppedUpgraded)
        {
            speedUpgrade.SetActive(false);
            speedBought = true;
        }

        if (currencyManager.rangeUpgraded)
        {
            rangeUpgrade.SetActive(false);
            rangeBought = true;
        }
    }
    
    public void BuyDamage()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= damagePrice) || damageBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.RemoveMoney(damagePrice);
        damageUpgrade.SetActive(false);
        ljud.SpelaKöpLjud();
        damageBought = true;
        currencyManager.attackUpgraded = true;
    }
    
    public void BuyRange()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= rangePrice) || rangeBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.RemoveMoney(rangePrice);
        rangeUpgrade.SetActive(false);
        ljud.SpelaKöpLjud();
        rangeBought = true;
        currencyManager.rangeUpgraded = true;
    }
    
    public void BuySpeed()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= speedPrice) || speedBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.RemoveMoney(speedPrice);
        speedUpgrade.SetActive(false);
        ljud.SpelaKöpLjud();
        speedBought = true;
        currencyManager.sppedUpgraded = true;
    }
}
