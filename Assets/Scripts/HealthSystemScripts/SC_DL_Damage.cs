using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DL_Damage : MonoBehaviour
{
    public event Idamage OnHit;
    public delegate void Idamage();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DoHitEvent();
        }
    }

    public void DoHitEvent() => OnHit?.Invoke();
}
