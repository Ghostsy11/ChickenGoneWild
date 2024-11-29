using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DamagePlayer : MonoBehaviour
{
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