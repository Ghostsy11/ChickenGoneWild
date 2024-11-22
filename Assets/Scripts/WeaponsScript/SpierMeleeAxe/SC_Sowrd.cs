using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Sowrd : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            Debug.Log("Kill");

        }
        if (collision.gameObject.tag == "SowrdCollision")
        {
            Debug.Log("Collision");
        }
    }

}
