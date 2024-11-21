using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartMenuScriptManager : MonoBehaviour
{

    public void StartMenu(int whichScene)
    {
        LoadScene(whichScene);
    }

    public void LocalMultiPlayer()
    {
        Debug.Log("Open Other Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
