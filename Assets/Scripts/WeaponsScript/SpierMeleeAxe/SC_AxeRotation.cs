using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AxeRotation : MonoBehaviour
{
    [Header("Genreal Settings")]
    [SerializeField] bool axeThrowenCheck;
    [SerializeField] bool returingAxe;
    [SerializeField] float rotationAxeSpeed;
    [SerializeField] float RotationSpeedWhenItComesBack;
    [SerializeField] float forceOnXOrRedLineAxisRight;
    [SerializeField] float forceUpOnGreenLineaAxis;

    [Header("Set Up")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform curvPoint, targetPlayerHand;
    [SerializeField] Vector3 oldAxePos;
    [SerializeField] float time = 0.0f;

    void Update()
    {
        //rotate2();
        if (axeThrowenCheck == true)
        {

            ApplyForceXAxis();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            axeThrowenCheck = false;
            returingAxe = true;
        }
        ReturingAxe();

    }



    private void ApplyForceXAxis()
    {
        if (axeThrowenCheck)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.transform.parent = null;
            rb.AddForce(transform.right * forceOnXOrRedLineAxisRight, ForceMode.Impulse);
            rb.AddForce(transform.up * forceUpOnGreenLineaAxis, ForceMode.Impulse);
            transform.Rotate(-transform.forward * rotationAxeSpeed * Time.deltaTime);
            Debug.DrawLine(transform.position, rb.position, Color.red);

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touched:" + collision.gameObject);
        axeThrowenCheck = false;
        rb.isKinematic = true;
    }

    // Link to the formule
    // https://www.youtube.com/watch?v=Xwj8_z9OrFw&ab_channel=RyanZehm
    // https://www.youtube.com/watch?v=dXECQRlmIaE&ab_channel=CodingMath
    private Vector3 CalAxeReturingValue(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);

        return p;
    }

    private void GetAxeBack()
    {
        oldAxePos = transform.position;
        //        rb.velocity = Vector3.zero;
    }

    private void ReturingAxe()
    {
        if (returingAxe == true)
        {
            GetAxeBack();
            if (time <= 1.0f)
            {
                transform.position = CalAxeReturingValue(time, oldAxePos, curvPoint.position, targetPlayerHand.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetPlayerHand.rotation, RotationSpeedWhenItComesBack * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                time = 0.0f;
                returingAxe = false;
                transform.parent = targetPlayerHand;
                transform.position = targetPlayerHand.position;
                transform.rotation = targetPlayerHand.rotation;


            }
        }


    }


}
