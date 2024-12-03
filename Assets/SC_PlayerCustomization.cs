using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_PlayerCustomization : MonoBehaviour
{
    [Header("Controls")]
    public InputActionReference customizeMove, customizeConfirm, customizeCancel;
    [Header("Colors")]
    [SerializeField] private List<Color> customizeColors = new();
    [SerializeField] private List<Renderer> customizeRenderer = new();
    private int currentColor = 0;
    [Header("ReadyToPlay")]
    bool ready = false;
    public void ChangeColor(InputAction.CallbackContext context)
    {
        if (!ready)
        {
            if (context.performed && customizeMove.action.enabled)
            {
                currentColor += (int)context.action.ReadValue<Vector2>().x;
                if (currentColor < 0)
                {
                    currentColor = customizeColors.Count - 1;
                }
                else if (currentColor >= customizeColors.Count)
                {
                    currentColor = 0;
                }
                for (int i = 0; i < customizeRenderer.Count; i++)
                {
                    customizeRenderer[i].material.color = customizeColors[currentColor];
                }
            }
        }
    }
    public void Confirm(InputAction.CallbackContext context)
    {
        if (context.performed && customizeConfirm.action.enabled)
        {
            if (!ready)
            {
                ready = true;
                SC_PlayerInputManager.instance.AddReady(ready);
            }
        }
    }
    public void Cancel(InputAction.CallbackContext context)
    {
        if (context.performed && customizeCancel.action.enabled)
        {
            if (ready)
            {
                ready = false;
                SC_PlayerInputManager.instance.AddReady(ready);
            }
        }
    }
}
