
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_OtherScript : MonoBehaviour
{
    SC_Event eventt;
    private void Start()
    {

        // Event scripts related.
        eventt = GetComponent<SC_Event>();
        eventt.OnSpacePressed += Eventt_OnSpacePressed;
        eventt.OnGettingHit += Onhit;
        eventt.OnFloatEvent += Eventt_OnFloatEvent;
        eventt.OnActionEvent += Eventt_OnActionEvent;

    }

    private void Eventt_OnActionEvent(bool arg1, int arg2, float arg3, string arg4)
    {
        Debug.Log("Action:" + " " + arg1 + " " + arg2 + " " + arg3 + " " + arg4);
    }

    private void Eventt_OnFloatEvent(float f)
    {
        Debug.Log("Float:" + f);
    }

    private void Eventt_OnSpacePressed(object sender, System.EventArgs e)
    {
        Debug.Log("TakeDamage");
        eventt.OnSpacePressed -= Eventt_OnSpacePressed;

    }


    public void Onhit(object sender, SC_Event.OnAlreadyGotHitted e)
    {
        Debug.Log("H");
        eventt.takeDamage -= 1;
        eventt.OnGettingHit -= Onhit;
    }

    public void UnityE()
    {
        Debug.Log("Testing Unity Event:)");
    }





}
