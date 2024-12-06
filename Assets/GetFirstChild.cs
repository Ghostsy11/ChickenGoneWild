using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetFirstChild : MonoBehaviour
{
    [SerializeField] PlayerAttackState axe;
    [SerializeField] private GameObject parentObject; // Assign the parent object in the Inspector
    [SerializeField] private List<GameObject> childrenList = new List<GameObject>(); // List to store references to child GameObjects
    [SerializeField] public Transform pressQ; // Field for a Transform reference
    //[SerializeField] public GameObject anmationtestingref;

    private void Awake()
    {
        GetChildren();

        // Automatically assign the first child to pressQ if available
        if (childrenList.Count > 0 && pressQ == null)
        {
            pressQ = childrenList[0].transform;
            Debug.Log($"Automatically assigned {pressQ.name} to pressQ.");
        }
        else if (pressQ == null)
        {
            Debug.LogWarning("No children found, and pressQ is unassigned.");
        }
    }


    private void GetChildren()
    {
        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {
                AddGameObjectAndSubChildren(child);
            }
            Debug.Log($"Total GameObjects found: {childrenList.Count}");
        }
        else
        {
            Debug.LogError("Parent is null");
        }
    }

    private void AddGameObjectAndSubChildren(Transform current)
    {
        // Add the current GameObject to the list if not already added
        if (!childrenList.Contains(current.gameObject))
        {
            childrenList.Add(current.gameObject);
            Debug.Log($"Added GameObject: {current.name}");
        }

        // Recursively add all sub-children
        foreach (Transform child in current)
        {
            AddGameObjectAndSubChildren(child);
        }
    }

    public void AccessFirstChildComponents()
    {
        if (childrenList.Count > 0)
        {
            var firstChild = childrenList[0];
            Debug.Log($"Accessing components of: {firstChild.name}");

            // Example: Get specific components
            var rigidbodyComponent = firstChild.GetComponent<Rigidbody>();
            if (rigidbodyComponent != null)
            {
                Debug.Log($"{firstChild.name} has a Rigidbody component.");
                // Perform operations with the Rigidbody component
            }
            else
            {
                Debug.Log($"{firstChild.name} does not have a Rigidbody component.");
            }

            var colliderComponent = firstChild.GetComponent<Collider>();
            if (colliderComponent != null)
            {
                Debug.Log($"{firstChild.name} has a Collider component.");
                // Perform operations with the Collider component
            }
            else
            {
                Debug.Log($"{firstChild.name} does not have a Collider component.");
            }
        }
        else
        {
            Debug.LogWarning("childrenList is empty! Cannot access components.");
        }
    }


    public void BoolEnabler()
    {
        childrenList[0].gameObject.SetActive(false);
    }
}


