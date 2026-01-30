using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rigidbody; //TODO: Gör automagiskt i start
    public float movementForce = 1;
    
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
}
