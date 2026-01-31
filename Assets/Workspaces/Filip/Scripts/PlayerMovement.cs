using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody; //TODO: Gör automagiskt i start
    public float movementForce = 1;
    public float dodgeForce = 1;
    private bool isPaused = false;
    
    public GameObject attackHitbox;
    
    void Start()
    {
        attackHitbox = GameObject.FindGameObjectWithTag("PlayerHitbox");
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
        
        rigidbody.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0) * dodgeForce, ForceMode.Impulse);
    }

    void OnJump()
    {
        //NOT a jump, attack... blame unity...
        
        //attackHitbox.active = true;
    }
    
    void OnPause()
    {
        if (!isPaused)
        {
            Debug.Log("Pause");
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isPaused)
        {
            Debug.Log("Resume");
            Time.timeScale = 1;
            isPaused = false;
        }
    }
}
