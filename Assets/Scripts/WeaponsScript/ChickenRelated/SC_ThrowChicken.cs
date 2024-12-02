using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ThrowChicken : MonoBehaviour
{
    [Header("Script reference")]
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] PickUpChicken pickUpChicken;
    [SerializeField] SC_ChickenBomb sC_ChickenBomb;
    [Tooltip("Force power")]
    [SerializeField] float force;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sC_ChickenBomb = GetComponent<SC_ChickenBomb>();
        pickUpChicken = GetComponent<PickUpChicken>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpChicken.ThorwAfterPickUp();
            ApplyForceToTheRightSide();
            sC_ChickenBomb.chickenIsProvoked = true;
            sC_ChickenBomb.WaitingForTarget();
            GetComponent<SC_ThrowChicken>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            pickUpChicken.ThorwAfterPickUp();
            ApplyForceToTheLeftSide();
            sC_ChickenBomb.chickenIsProvoked = true;
            sC_ChickenBomb.WaitingForTarget();
            GetComponent<SC_ThrowChicken>().enabled = false;

        }
    }

    private void ApplyForceToTheRightSide()
    {
        rigidbody.AddForce(force, force, 0, ForceMode.Impulse);

    }

    private void ApplyForceToTheLeftSide()
    {
        rigidbody.AddForce(-force, force, 0, ForceMode.Impulse);

    }

}
