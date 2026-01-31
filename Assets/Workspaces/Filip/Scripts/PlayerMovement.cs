using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody; //TODO: Gör automagiskt i start
    public float movementForce = 1;
    public float dodgeForce = 1;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    void DoMovement()
    {
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
        Debug.Log("Pressed");
        
        
        
        rigidbody.AddRelativeForce(new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0) * dodgeForce, ForceMode.Impulse);
        
        //float inputX = Input.GetAxis("Horizontal");

        //if (inputX == 1)
        //{
            //Debug.Log("Dashed Right");
            //rigidbody.AddForce(transform.right * dodgeForce, ForceMode.Impulse);
        //}
        //else if (inputX == -1)
        //{
          //  Debug.Log("Dashed Left");
            //rigidbody.AddForce(-transform.right * dodgeForce, ForceMode.Impulse);
        //}
    }
}
