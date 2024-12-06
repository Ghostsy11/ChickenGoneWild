using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rb.AddForce(0, 300, 0, ForceMode.Impulse);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
