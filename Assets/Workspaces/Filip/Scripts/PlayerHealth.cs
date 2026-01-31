using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] private float maxHealth;

    //public TMP_Text healthText;
    
    //private GameManager _managerScript;
    private GameObject _gameManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        
        UpdateHealthTxt();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    private void Death()
    {
        
    }

    private void UpdateHealthTxt()
    {
        //healthText.text = string.Format("{0}/{1}", health, maxHealth + " HP");
    }
}
