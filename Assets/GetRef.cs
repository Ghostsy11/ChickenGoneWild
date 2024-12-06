using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRef : MonoBehaviour
{




    [SerializeField] private GameObject parentObject; // Assign the parent object in the Inspector
    [SerializeField] private List<GameObject> childrenList = new List<GameObject>(); // List to store references to child GameObjects
    [SerializeField] private Transform pressQ;

    private void Awake()
    {
        GetChilderen();
    }

    private void GetChilderen()
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

    public void testingRef()
    {
        Debug.Log(childrenList[0].name);
        pressQ.transform.position = childrenList[0].GetComponent<Transform>().position;

    }

}




