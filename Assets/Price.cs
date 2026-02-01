using TMPro;
using UnityEngine;

public class Price : MonoBehaviour
{
    private GameObject parent;
    private TMP_Text priceText;
    [SerializeField] private Shop shop;

    void Start()
    {
        priceText = GetComponent<TMP_Text>();
        
        parent = transform.parent.gameObject;

        if (parent.name == "DMG-upgrade")
        {
            priceText.text = shop.damagePrice.ToString();
        }
        else if (parent.name == "SPEED-upgrade")
        {
            priceText.text = shop.speedPrice.ToString();
        }
        else if (parent.name == "RANGE-upgrade")
        {
            priceText.text = shop.rangePrice.ToString();
        }
    }
    
    
}
