using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator punchAnimator;

   // public Animator punchRight;

    //public Animator punchLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DoAnim(bool isPunching)
    {
        punchAnimator.SetBool("Punch", isPunching);
    }
    
    
}
