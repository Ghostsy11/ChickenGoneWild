using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AxeRotation : MonoBehaviour
{
    [Header("Genreal Settings")]
    [SerializeField] bool axeThrowenCheck;
    public bool returingAxe;
    [SerializeField] bool DoubleCheck;
    [SerializeField] float rotationAxeSpeed;
    [SerializeField] float RotationSpeedWhenItComesBack;
    [SerializeField] float forceOnXOrRedLineAxisRight;
    [SerializeField] float forceUpOnGreenLineaAxis;
    [SerializeField] SC_ActionOnTime timerTeGetAxeBackAfter;


    [Header("Set Up")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform curvPoint, targetPlayerHand;
    [SerializeField] Vector3 oldAxePos;
    [SerializeField] float time = 0.0f;

    [SerializeField] GameObject boolCheck;
    [SerializeField] float ItsTimeComBack;

    private void Start()
    {
        timerTeGetAxeBackAfter = gameObject.GetComponent<SC_ActionOnTime>();
    }
    void Update()
    {
        //rotate2();

        //ThorwAxe();

        if (axeThrowenCheck == true && DoubleCheck == true)
        {

            ApplyForceXAxis();
        }
        ReturingAxe();

    }

    public void GetITT()
    {
        timerTeGetAxeBackAfter.SetTimer(ItsTimeComBack, () => returingAxe = true);
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
        rb.isKinematic = true;
        DoubleCheck = false;
        if (DoubleCheck == false)
        {


            axeThrowenCheck = false;
        }
        else
        {
        }


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


    public void ThorwAxe()
    {
        DoubleCheck = true;
        axeThrowenCheck = true;
        GetITT();

    }

    //public void ReturnAxeEnabler()
    //{
    //    axeThrowenCheck = false;
    //    DoubleCheck = true;
    //    returingAxe = true;
    //}
    //public void ReturnAxeBoolEnabler()
    //{
    //    returingAxeEnabled = true;
    //}

}
