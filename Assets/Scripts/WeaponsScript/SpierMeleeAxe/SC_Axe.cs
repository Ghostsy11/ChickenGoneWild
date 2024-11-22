using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class SC_Axe : MonoBehaviour
{
    [Header("Ref needs to be manually attached")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float force;
    [SerializeField] Transform holder;
    [SerializeField] GameObject axePrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ThrowAxe();
        }
    }
    private void ThrowAxe()
    {
        var axe = Instantiate(axePrefab, holder);
        //axe.GetComponent<Rigidbody>().AddForce(force, force, 0, ForceMode.Impulse);
        axe.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);

    }



}
