using UnityEngine;
using UnityEngine.Events;

public class AnimationMaster : MonoBehaviour
{
    [Header("Animators")]
    public Animator leftHand, rightHand, hands;
    public UnityEvent OnAnimationStart;
    
    public void PlayPunchAnimation()
    {
        leftHand.SetBool("Punch", true);
        rightHand.SetBool("Punch", true);
        hands.SetBool("Punch", true);
    }

    public void StopPunchAnimation()
    {
        leftHand.SetBool("Punch", false);
        rightHand.SetBool("Punch", false);
        hands.SetBool("Punch", false);
    }
}
