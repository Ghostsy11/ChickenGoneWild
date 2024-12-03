using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTesting : MonoBehaviour
{

    [SerializeField] Animator animator;

    [SerializeField] const string Walking = "Walking";
    [SerializeField] const string Jumping = "Hanging";
    [SerializeField] const string HitingSowrd = "HittingSowrd";
    [SerializeField] const string ThrowLeftSideAxe = "ThrowLeftSideAxe";
    [SerializeField] const string ThrowRightSideAxe = "ThrowRightSideAxe";
    [SerializeField] const string GettingAxeBack = "GettingAxeBack";
    [SerializeField] const string AxeHit = "AxeHit";


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            InvokeWalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            NotInvokingWalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            InvokingHangingInAirAnimation();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            HitSowrdAnimation();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ThrowingLeftSideAxe();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ThrowingRightSideAxe();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GettingAxeAnimation();
        }

    }

    public void InvokeWalkingAnimation()
    {
        animator.SetBool(Walking, true);
    }
    public void NotInvokingWalkingAnimation()
    {
        animator.SetBool(Walking, false);
    }

    public void InvokingHangingInAirAnimation()
    {
        animator.SetTrigger(Jumping);
    }

    public void HitSowrdAnimation()
    {
        animator.SetTrigger(HitingSowrd);
    }

    public void ThrowingLeftSideAxe()
    {
        animator.SetTrigger(ThrowLeftSideAxe);
    }

    public void ThrowingRightSideAxe()
    {
        animator.SetTrigger(ThrowRightSideAxe);
    }

    public void AxeHitAnimation()
    {
        animator.SetTrigger(AxeHit);
    }

    public void GettingAxeAnimation()
    {
        animator.SetTrigger(GettingAxeBack);
    }

}
