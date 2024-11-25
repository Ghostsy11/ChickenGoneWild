using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_EventManager : MonoBehaviour
{
    public static SC_EventManager instance;
    public delegate void OnEnableControls(InputAction action);
    public OnEnableControls onEnableControls;

    public delegate void OnDisableControls(InputAction action);
    public OnDisableControls onDisableControls;

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
    public void EnableControls(InputAction action)
    {
        onEnableControls?.Invoke(action);
    }
    public void DisableControls(InputAction action)
    {
        onDisableControls?.Invoke(action);
    }
}
