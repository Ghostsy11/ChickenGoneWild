using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] const string AnimationSpier = "Spier";
    [SerializeField] const string AnimationSwordAttack = "SwordAttack";
    [SerializeField] const string AnimationAxe = "Axe";
    [SerializeField] const string AnimationThrow = "Throw";
    [SerializeField] const string AnimationBlock = "Block";
    [SerializeField] const string AnimationBlockBool = "BlockBool";
    [SerializeField] const string AnimationJump = "Jump";
    [SerializeField] const string AnimationHang = "Hang";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayerSpierAnimation()
    {
        animator.SetTrigger(AnimationSpier);

    }

    public void PlayerAnimationSwordAttack()
    {
        animator.SetTrigger(AnimationSwordAttack);

    }

    public void PlayerAnimationAxe()
    {
        animator.SetTrigger(AnimationAxe);
    }

    public void PlayerAnimationThrow()
    {
        animator.SetBool(AnimationThrow, true);
        // Condation
        animator.SetBool(AnimationThrow, false);
    }

    public void PlayBlockAnimation()
    {
        animator.SetTrigger(AnimationBlock);
        //animator.SetBool(AnimationBlockBool, true);
    }

}
