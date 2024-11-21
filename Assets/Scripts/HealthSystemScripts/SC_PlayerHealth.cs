using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerHealth : MonoBehaviour
{
    SC_DL_Damage triggerEvent;
    [SerializeField] int health = 1;
    [SerializeField] int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        triggerEvent = GetComponent<SC_DL_Damage>();
        triggerEvent.OnHit += TakeDamage;
        triggerEvent.OnHit += PlayVFX;
        triggerEvent.OnHit += DisablePlayer;
        triggerEvent.OnHit += TriggerEvent_OnHit;

    }

    private void TriggerEvent_OnHit()
    {
        Debug.Log("OtherStuff");
    }

    private void TakeDamage()
    {
        health -= damage;
    }

    private void PlayVFX()
    {
        Debug.Log("BleedingOnHit");
    }

    private void DisablePlayer()
    {
        if (health < 0)
        {
            triggerEvent.OnHit -= DisablePlayer;
            triggerEvent.OnHit -= TakeDamage;
            triggerEvent.OnHit -= PlayVFX;

            Debug.Log("You Lost Disable UI");
        };
    }
}
