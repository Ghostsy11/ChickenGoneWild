
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_OtherScript : MonoBehaviour
{
    private void Start()
    {
        SC_Event eventt = GetComponent<SC_Event>();
        eventt.OnSpacePressed += Eventt_OnSpacePressed;
    }

    private void Eventt_OnSpacePressed(object sender, System.EventArgs e)
    {
        Debug.Log("TakeDamage");
        SC_Event eventt = GetComponent<SC_Event>();
        eventt.OnSpacePressed -= Eventt_OnSpacePressed;

    }
}
