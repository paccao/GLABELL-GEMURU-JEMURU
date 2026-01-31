using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] private float maxHealth;
    private PlayerMovement playerMovement;

    //public TMP_Text healthText;
    
    //private GameManager _managerScript;
    //private GameObject _gameManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
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
        playerMovement.maskLjud.PlayDamageSound();
        UpdateHealthTxt();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        playerMovement.maskLjud.SpelaDöLjud();
    }

    private void UpdateHealthTxt()
    {
        //healthText.text = string.Format("{0}/{1}", health, maxHealth + " HP");
    }
}
