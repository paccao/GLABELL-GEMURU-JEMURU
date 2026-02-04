using UnityEngine;
using UnityEngine.Events;
using Workspaces.Joel.Assets.Scripts;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;
    public bool isMasked;
    
    public CurrencyManager currencyManager;

    private Enemy enemy;
    
    public UnityEvent OnDeath, OnHit;

    //public TMP_Text healthText;
    
    //private GameManager _managerScript;
    //private GameObject _gameManager;

    private void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
    }
    
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        
        health = maxHealth;
        
        UpdateHealthTxt();

    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
		OnHit.Invoke();
        UpdateHealthTxt();

        if (health <= 0)
        {
            Death();
        }
        else
        {
            if (isMasked)
            {
                enemy.fiskLjud.SpelaMaskTräffLjud();
            }
            else
            {
                enemy.fiskLjud.SpelaTräffLjud();
            }
        }
    }

    private void Death()
    {
        if (isMasked)
            currencyManager.AddMoney(1);
        currencyManager.AddScore(1);
        enemy.fiskLjud.SpelaDöLjud();
        //Note from programmer: inte okej...
        Debug.Log("DÖD");
        Destroy(gameObject);
    }

    private void UpdateHealthTxt()
    {
        //healthText.text = string.Format("{0}/{1}", health, maxHealth + " HP");
    }
}
