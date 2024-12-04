using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : MonoBehaviour
{

    public enum AttackState { Axe, Spier, Sowrd, Thorwable  /*Hammer, PickAxe*/};
    [SerializeField] List<AttackState> attackStates = new List<AttackState>();
    [SerializeField] List<GameObject> SelectedWeapon = new List<GameObject>();
    public AttackState playerAttackState;

    public GameObject weaponSort;


    [SerializeField] private Dictionary<AttackState, GameObject> weaponPrefabs; // Dictionary to map attack states to prefabs
    [SerializeField] private Transform weaponSpawnPoint; // Point where the weapon will be instantiated

    private void Awake()
    {
        // Initialize the attackStates list with all enum values
        foreach (AttackState state in Enum.GetValues(typeof(AttackState)))
        {
            attackStates.Add(state);
        }

        // Initialize the dictionary with your prefabs (Assign these in the Unity Inspector)
        weaponPrefabs = new Dictionary<AttackState, GameObject>();
        // Example: Assign prefabs manually or in the inspector
        // weaponPrefabs[AttackState.Axe] = axePrefab;
        // weaponPrefabs[AttackState.Sowrd] = swordPrefab;
        // Add other mappings...

        weaponPrefabs[AttackState.Axe] = SelectedWeapon[0];
        weaponPrefabs[AttackState.Spier] = SelectedWeapon[1];
        weaponPrefabs[AttackState.Sowrd] = SelectedWeapon[2];
        weaponPrefabs[AttackState.Thorwable] = SelectedWeapon[3];
        //weaponPrefabs[AttackState.Hammer] = SelectedWeapon[4];
        //weaponPrefabs[AttackState.PickAxe] = SelectedWeapon[5];

    }

    public void SetRandomAttackState()
    {
        if (attackStates.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, attackStates.Count);
            playerAttackState = attackStates[randomIndex];
        }
        else
        {
            Debug.LogWarning("Attack states list is empty!");
        }
    }

    public void InstantiateWeapon()
    {
        if (weaponPrefabs.TryGetValue(playerAttackState, out GameObject weaponPrefab))
        {
            weaponSort = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
            weaponSort.transform.parent = weaponSpawnPoint;
            Debug.Log($"Instantiated weapon for {playerAttackState}");
        }
        else
        {
            Debug.LogError($"No prefab assigned for attack state: {playerAttackState}");
        }
    }

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

    //private void PickAxe()
    //{
    //    playerAttackState = AttackState.PickAxe;
    //}

    //private void ThrowableHammer()
    //{
    //    playerAttackState = AttackState.Hammer;
    //}


    void Start()
    {
        SetRandomAttackState();
        InstantiateWeapon();
        Debug.Log($"Random Attack State: {playerAttackState}");
    }


}
