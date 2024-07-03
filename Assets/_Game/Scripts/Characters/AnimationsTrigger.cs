using UnityEngine;

public class AnimationsTrigger : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    private int isEating;
    private int isHappy;
    private int isBurbing;
    private int isAngry;
    void Start()
    {
        isEating=Animator.StringToHash("isEating");
        isHappy = Animator.StringToHash("isHappy");
        isBurbing = Animator.StringToHash("isBurbing");
        isAngry = Animator.StringToHash("isAngry");
    }

    public void TriggerHappy()
    {
        characterAnimator.SetTrigger(isHappy);
    }
    public void TriggerEating()
    {
        characterAnimator.SetTrigger(isEating);
    }
    public void TriggerBurbing() {
        characterAnimator.SetTrigger(isBurbing);
    }
    public void TriggerAngry() {
        characterAnimator.SetTrigger(isAngry);
    }
}
