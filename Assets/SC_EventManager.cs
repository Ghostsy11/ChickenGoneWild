using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_EventManager : MonoBehaviour
{
    public static SC_EventManager instance;

    public delegate void OnCustomizeLoaded();
    public static event OnCustomizeLoaded onCustomizeLoaded;

    public delegate void OnCustomizeDone();
    public static event OnCustomizeDone onCustomizeDone;

    public delegate void OnGameLoaded();
    public static event OnGameLoaded onGameLoaded;

    public delegate void OnGameDone();
    public static event OnGameDone onGameDone;


    private void Awake()
    {
        if (instance == null || instance == this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void CustomizeLoaded()
    {
        onCustomizeLoaded?.Invoke();
    }
    public void CustomizeDone()
    {
        onCustomizeDone?.Invoke();
    }
    public void GameLoaded()
    {
        onGameLoaded?.Invoke();
    }
    public void GameDone()
    {
        onGameDone?.Invoke();
    }
}
