using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerControllerEnabler : MonoBehaviour
{
    private SC_PlayerController sc_PlayerController;
    private SC_PlayerCustomization sc_PlayerCustomization;
    PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        sc_PlayerController = transform.GetComponent<SC_PlayerController>();
        sc_PlayerCustomization = transform.GetComponent<SC_PlayerCustomization>();
    }
    private void OnEnable()
    {
        SC_EventManager.onCustomizeLoaded += CustomizationControlsEnabler;
        SC_EventManager.onGameLoaded += GameControlsEnabler;
    }
    private void OnDisable()
    {
        SC_EventManager.onCustomizeLoaded -= CustomizationControlsEnabler;
        SC_EventManager.onGameLoaded -= GameControlsEnabler;
    }
    public void CustomizationControlsEnabler()
    {
        playerInput.SwitchCurrentActionMap("PlayerCustomization");
    }
    public void GameControlsEnabler()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
}
