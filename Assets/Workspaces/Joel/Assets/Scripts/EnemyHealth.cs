using UnityEngine;
using Workspaces.Joel.Assets.Scripts;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;

    private Enemy enemy;

    //public TMP_Text healthText;
    
    //private GameManager _managerScript;
    //private GameObject _gameManager;
    
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        
        health = maxHealth;
        
        UpdateHealthTxt();
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
        UpdateHealthTxt();
        if (health <= 0)
        {
            Death();
        }
        else
        {
            enemy.fiskLjud.SpelaTräffLjud();
        }
    }

    private void Death()
    {
        enemy.fiskLjud.SpelaDöLjud();
        //Note from programmer: inte okej...
        Destroy(gameObject);
    }

    private void UpdateHealthTxt()
    {
        //healthText.text = string.Format("{0}/{1}", health, maxHealth + " HP");
    }
}
