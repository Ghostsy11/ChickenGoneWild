using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour
{
    public enum AttackState { Axe, Spier, Sowrd, Thorwable };
    [SerializeField] AttackState playerAttackState;

    private void AttackAxe()
    {
        playerAttackState = AttackState.Axe;
    }

    private void AttackSpier()
    {
        playerAttackState = AttackState.Spier;
    }

    private void AttackSowrd()
    {
        playerAttackState = AttackState.Sowrd;
    }

    private void ThrowableAttack()
    {
        playerAttackState = AttackState.Thorwable;
    }

}
