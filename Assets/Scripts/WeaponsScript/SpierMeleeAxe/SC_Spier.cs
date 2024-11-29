using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Spier : MonoBehaviour
{
    //[Header("Script reference")]

    [Tooltip("Force power")]
    [SerializeField] float force;
    [SerializeField] float Pushforce;

    [SerializeField] GameObject spier;
    [SerializeField] Transform spierPos;
    [SerializeField] bool spierIsThrowns;


    void Update()
    {
        Debug.Log(spierIsThrowns);

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpierAttackRight();

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpierAttackLeft();

        }
    }

    private void ApplyForceToTheRightSide(Rigidbody rb)
    {
        rb.AddForce(10, force, 0, ForceMode.Impulse);

    }

    private void ApplyForceToTheLeftSide(Rigidbody rb)
    {
        rb.AddForce(-10, force, 0, ForceMode.Impulse);

    }

    public void SpierAttackRight()
    {
        if (spierIsThrowns == false)
        {
            spierIsThrowns = true;
            var spier1 = Instantiate(spier, spierPos);
            spier1.transform.parent = null;

            ApplyForceToTheRightSide(spier1.GetComponent<Rigidbody>());

            Destroy(spier1, 2.5f);
        }
    }

    public void SpierAttackLeft()
    {
        if (!spierIsThrowns)
        {

            spierIsThrowns = true;
            var spier1 = Instantiate(spier, spierPos.position, Quaternion.Euler(0f, 0f, 90f));
            spier1.transform.parent = null;

            ApplyForceToTheLeftSide(spier1.GetComponent<Rigidbody>());

            Destroy(spier1, 2.5f);
        }
    }



}
