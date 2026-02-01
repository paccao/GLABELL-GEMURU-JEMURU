using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] private float maxHealth;
    private PlayerMovement playerMovement;
    private bool canDamage = true;
    
    public UnityEvent OnDeath;

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
        if (!canDamage)
            return;
        health -= damageAmount; 
        StartCoroutine(DmgDelay());
        Debug.Log(health);
        playerMovement.maskLjud.PlayDamageSound();
        UpdateHealthTxt();
        if (health <= 0)
        {
            Death();
        }
    }

    private IEnumerator DmgDelay()
    {
        canDamage = false;
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
    

    private void Death()
    {
        playerMovement.maskLjud.SpelaDöLjud();
        OnDeath.Invoke();
    }

    private void UpdateHealthTxt()
    {
        //healthText.text = string.Format("{0}/{1}", health, maxHealth + " HP");
    }
}
