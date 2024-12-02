using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ThrowChicken : MonoBehaviour
{
    [Header("Script reference")]
    [SerializeField] Rigidbody rigidbody;

    [Tooltip("Force power")]
    [SerializeField] float force;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ApplyForceToTheRightSide();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ApplyForceToTheLeftSide();
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
