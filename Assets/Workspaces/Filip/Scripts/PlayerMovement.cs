using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody; //TODO: Gör automagiskt i start
    public float movementForce = 1;
    public float dodgeForce = 1;
    public float attackCooldown = 1f;
    public float scaleModifier = 1;
    public float dmgAmount = 1;
    
    private float attackBetween = 0.1F;
    private AnimationController animationController;
    
    private bool isPaused = false;
    private bool isAttacking = false;
    
    public CurrencyManager currencyManager;
    
    public GameObject attackHitbox;
    private HitboxDmg hitboxDmg;
    
    [Header("Ljud")]
    [SerializeField] public MaskLjud maskLjud;

    void Awake()
    {
        currencyManager = CurrencyManager.Instance;
    }
    
    void Start()
    {
        animationController = GetComponentInChildren<AnimationController>();

        if(currencyManager.attackUpgraded)
            dmgAmount += 3;

        if (currencyManager.rangeUpgraded)
            attackHitbox.transform.localScale += new Vector3(scaleModifier, scaleModifier, scaleModifier);
        
        if (currencyManager.sppedUpgraded) 
            attackCooldown -= 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
        //Pushes the player
        
        float inputX = Input.GetAxis("Horizontal");

        if (inputX == 0)
            return;
        
        if (inputX == 1)
        {
            rigidbody.AddForce(transform.right * movementForce);
        }
        else if (inputX == -1)
        {
            rigidbody.AddForce(-transform.right * movementForce);
        }
    }
    
    void OnAttack()
    {
        //NOT an attack, makes the player dodge based on left stick position
        
        Debug.Log("Attacked");
        maskLjud.SpelaSlag();
        rigidbody.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0) * dodgeForce, ForceMode.Impulse);
    }

    void OnJump()
    {
        //NOT a jump, attack... blame unity...
        if (isAttacking) return;
        StartCoroutine(AttackCooldown());
    }
    
    IEnumerator AttackCooldown()
    {
        Debug.Log("sup");
        isAttacking = true;
        animationController.DoAnim(true);
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackBetween);
        animationController.DoAnim(false);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(attackBetween);
        animationController.DoAnim(true);
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackBetween);
        animationController.DoAnim(false);
        attackHitbox.SetActive(false);
        yield return new WaitForSeconds(attackBetween);
        animationController.DoAnim(true);
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(attackBetween);
        attackHitbox.SetActive(false);
        animationController.DoAnim(false);
        yield return new WaitForSeconds(attackCooldown);
        
        isAttacking = false;
    }
    
    void OnPause()
    {
        if (!isPaused)
        {
            Debug.Log("Pause");
            Time.timeScale = 0;
            isPaused = true;
            LjudChef.Instans.PausaMusiken(true);
        }
        else if (isPaused)
        {
            Debug.Log("Resume");
            Time.timeScale = 1;
            isPaused = false;
            LjudChef.Instans.PausaMusiken(false);
        }
    }
}
