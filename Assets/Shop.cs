using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool damageBought = false;
    private bool rangeBought = false;
    private bool speedBought = false;
    public int damagePrice = 30;
    public int rangePrice = 50;
    public int speedPrice = 20;
    public AffärsLjud ljud;

    public void BuyDamage()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= damagePrice) || damageBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.currentCurrency -= damagePrice;
        damageBought = true;
    }
    
    public void BuyRange()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= rangePrice) || rangeBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.currentCurrency -= rangePrice;
        rangeBought = true;
    }
    
    public void BuySpeed()
    {
        if (!(CurrencyManager.Instance.currentCurrency >= speedPrice) || speedBought)
        {
            ljud.SpelaNekatKöpLjud();
            return;
        }
        
        CurrencyManager.Instance.currentCurrency -= speedPrice;
        // Update ui element
        speedBought = true;
        
        // Implement player upgrades
    }
}
