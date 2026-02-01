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

    void Awake()
    {
        currencyManager = CurrencyManager.Instance;
    }
    
    public void BuyDamage()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= damagePrice) || damageBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.RemoveMoney(damagePrice);
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
        
        currencyManager.sppedUpgraded = true;
    }
}
