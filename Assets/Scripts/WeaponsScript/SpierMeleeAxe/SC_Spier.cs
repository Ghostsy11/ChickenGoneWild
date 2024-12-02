using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Spier : MonoBehaviour
{
    [Header("Script reference")]
    [SerializeField] Rigidbody rigidbody;

    [Tooltip("Force power")]
    [SerializeField] float force;
    [SerializeField] float Pushforce;


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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("kill");
            collision.gameObject.GetComponent<GameObject>();
            collision.rigidbody.AddForce(Vector3.up, ForceMode.Impulse);

        }
        if (collision.gameObject.tag == "SpierCollision")
        {
            Debug.Log("Collision VFX");
        }
    }

}
