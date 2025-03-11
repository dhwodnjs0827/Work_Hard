using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsSprint = Animator.StringToHash("IsSprint");
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(bool isMove)
    {
        animator.SetBool(IsMove, isMove);
    }

    public void Sprint(bool isSprint)
    {
        animator.SetBool(IsSprint,  isSprint);
    }
}
