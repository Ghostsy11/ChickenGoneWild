using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationTesting : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] PlayerAttackState axe;
    [SerializeField] GameObject weaponPoint;
    [SerializeField] GameObject weaponTargetSowrd;
    [SerializeField] GameObject weaponTargetAxe;
    [SerializeField] SC_AxeRotation weaponTarget;
    [SerializeField] private List<GameObject> childrenList = new List<GameObject>(); // List to store children

    [SerializeField] GetFirstChild refchild;



    [SerializeField] GameObject weapon;

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
        axe = GetComponent<PlayerAttackState>();

        //weapon = axe.weaponSort;

    }
    private void Update()
    {

    }

    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.N))
    //    {
    //        InvokeWalkingAnimation();
    //    }
    //    //if (Input.GetKeyDown(KeyCode.M))
    //    //{
    //    //    NotInvokingWalkingAnimation();
    //    //}
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        InvokingHangingInAirAnimation();
    //    }
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        ThrowingLeftSideAxe();
    //    }
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        GettingAxeAnimation();
    //    }
    //}
    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("2");
        if (context.performed)
        {
            if (axe.playerAttackState == PlayerAttackState.AttackState.Thorwable)
            {
                InvokeAxeThrowerer();

            }
            if (axe.playerAttackState == PlayerAttackState.AttackState.Sowrd)
            {
                ThrowingRightSideAxe();
                SwordHit();
            }
            if (axe.playerAttackState == PlayerAttackState.AttackState.Axe)
            {
                HitSowrdAnimation();
                AxeeHit();
            }
        }
    }
    public void CallAxeBack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (axe.playerAttackState == PlayerAttackState.AttackState.Thorwable)
            {
                Debug.Log("........................................");
                refchild.BoolEnabler();
            }
        }
    }

    public void ReturningTheFkingAxe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Adding function bool here");

        }
    }
    public void InvokeWalkingAnimation()
    {
        animator.SetTrigger(Walking);
    }
    //public void NotInvokingWalkingAnimation()
    //{
    //    animator.SetBool(Walking, false);
    //}

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


    private void InvokeAxeThrowerer()
    {
        Debug.Log("testing");

        if (axe.playerAttackState == PlayerAttackState.AttackState.Thorwable)
        {

            StoreChildObjects();

        }

    }

    private void StoreChildObjects()
    {


        // Iterate through all children of the parent object
        foreach (Transform child in weaponPoint.transform)
        {
            if (!childrenList.Contains(child.gameObject))
            {
                childrenList.Add(child.gameObject); // Add the child GameObject to the list

            }

            foreach (Transform childinChild in child.transform)
            {
                if (!childrenList.Contains(childinChild.gameObject))
                {
                    childrenList.Add(childinChild.gameObject);
                }
                weaponTarget = childrenList[1].gameObject.GetComponent<SC_AxeRotation>();

                if (weaponTarget != null)
                {
                    weaponTarget.ThorwAxe();
                }
                else
                {
                    return;
                }
                //weaponTarget.ThorwAxe();
            }
        }

        Debug.Log($"Stored {childrenList.Count} children in the list.");
    }
    private void SwordHit()
    {
        if (axe.playerAttackState == PlayerAttackState.AttackState.Sowrd)
        {
            foreach (Transform child in weaponPoint.transform)
            {
                if (!childrenList.Contains(child.gameObject))
                {
                    childrenList.Add(child.gameObject); // Add the child GameObject to the list
                    child.transform.parent = weaponTargetSowrd.transform;


                }
            }
            Debug.Log("Sowrd");
        }
    }

    private void AxeeHit()
    {
        if (axe.playerAttackState == PlayerAttackState.AttackState.Axe)
        {
            foreach (Transform child in weaponPoint.transform)
            {
                if (!childrenList.Contains(child.gameObject))
                {
                    childrenList.Add(child.gameObject); // Add the child GameObject to the list
                    child.transform.parent = weaponTargetAxe.transform;

                }
            }
            Debug.Log("axe");
        }
    }
}
