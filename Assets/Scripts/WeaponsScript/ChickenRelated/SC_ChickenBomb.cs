using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SC_ChickenBomb : MonoBehaviour
{
    private Collider[] colliders;

    [Header("Chicken Settings")]
    [Tooltip("List to store detected enemies")]
    [SerializeField] List<GameObject> detectedEnemies;
    [Tooltip("Radius to check for enemies")]
    [SerializeField] float sphereRadius;
    [Tooltip("Chieken Speed")]
    [SerializeField] float moveSpeed;
    [Tooltip("Chieken Speed")]
    [SerializeField] float timeToSelfBlowUp;
    [SerializeField] bool chickenIsProvoked;

    [Header("Script reference")]
    [Tooltip("when player throw the bomb and no one in range timer will be activated. If no one enter the range on time the chicken will blow itself up.")]
    [SerializeField] SC_ActionOnTime onTime;

    private void Start()
    {
        onTime = gameObject.GetComponent<SC_ActionOnTime>();
    }

    private void Update()
    {
        ObjectsInOrOutRange();
    }

    private void ObjectsInOrOutRange()
    {
        // In range:
        // Find all colliders within the sphere radius around the ChickenBomb
        colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in colliders)
        {
            // Check if the object has the tag "Enemy"
            if (collider.CompareTag("Enemy"))
            {
                // Temporary variable that hold an gameobject
                GameObject enemy = collider.gameObject;

                // Check if the enemy is already in the list
                if (!detectedEnemies.Contains(enemy))
                {
                    // Add the enemy to the list
                    detectedEnemies.Add(enemy);
                    Debug.Log("Enemy added: " + enemy.name);
                }
            }
        }

        // Out range:
        // Remove enemies that have left the sphere radius
        RemoveEnemyNotInRange();

        // When more than one enemy in range is:
        // Move towards the closest enemy
        MoveTorwadsClosestEnemy();
    }

    // To activite timer of the bomb after being provoked
    private void WaitingForTarget()
    {
        if (chickenIsProvoked)
        {
            onTime.SetTimer(2f, () => Debug.Log("Timer complete!"));
            Debug.Log("Blow up when chicken is thrown and no one in rage after certain amount of time");
            Debug.Log("Player Effects");
        }

    }

    // OnTouching enemy kill
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnTochingEnemy();
        }
    }

    // To Play Effect...
    private void OnTochingEnemy()
    {
        Debug.Log("Play Effects:" + " " + "Game Over");
    }

    // To move torwards the closest enemy
    private void MoveTorwadsClosestEnemy()
    {
        if (detectedEnemies.Count > 0)
        {
            GameObject closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                // Move towards the closest enemy
                Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }
    }

    // To find the closest enemy in the detectedEnemies list
    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in detectedEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }


    // To remvoe enemy from list
    private void RemoveEnemyNotInRange()
    {
        // Looping in reverse order
        for (int i = detectedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = detectedEnemies[i];

            // distance beteen the chieken and enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            // Check if the enemy is outside the detection radius
            if (distance > sphereRadius)
            {
                detectedEnemies.RemoveAt(i);
                Debug.Log("Enemy removed: " + enemy.name);
            }
        }
    }

    // To draw spherewires
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
