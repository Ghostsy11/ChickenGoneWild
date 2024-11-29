using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_SceneManager : MonoBehaviour
{
    public static SC_SceneManager instance;
    private int sceneIndex = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);   
    }
    public void LoadScene(int i)
    {
        sceneIndex = i;
        SceneManager.LoadScene(sceneIndex);
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            SC_EventManager.instance.GameLoaded();
        }
        else if (level == 0)
        {
            SC_EventManager.instance.CustomizeLoaded();
        }
    }
}
