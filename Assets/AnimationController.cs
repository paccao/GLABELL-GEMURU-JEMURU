using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator punchAnimator;

    public Animator punchRight;

    public Animator punchLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            punchAnimator.SetBool("Punch", true);
            punchLeft.SetBool("Punch", true);
            punchRight.SetBool("Punch", true);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            punchAnimator.SetBool("Punch", false);
            punchLeft.SetBool("Punch", false);
            punchRight.SetBool("Punch", false);
        }
        
    }
}
