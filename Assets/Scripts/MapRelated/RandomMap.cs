using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMap : MonoBehaviour
{

    [SerializeField] List<int> scenes;

    void Start()
    {
        Debug.Log(scenes.Count);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GetRandomNumber();
            Debug.Log(GetRandomNumber());
        }
    }

    public int GetRandomNumber()
    {

        int randomNumber = Random.Range(0, scenes.Count);
        return randomNumber;

    }
}
