using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    public event Action OnXboxGamepadDetected;
    public event Action OnPlayStationGamepadDetected;
    public event Action OnKeyboardMouseDetected;

    private string _lastControlScheme = "";
    private InputSystem_Actions _actions;
    
    public InputSystem_Actions Actions => _actions;

    public void Initialize()
    {
        _actions = new InputSystem_Actions();
        _actions.Enable(); 
    }

    private void OnEnable()
    {
        InputSystem.onActionChange += OnInputActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= OnInputActionChange;
    }

    private void OnDestroy()
    {
        OnXboxGamepadDetected -= null;
        OnPlayStationGamepadDetected -= null;
        OnKeyboardMouseDetected -= null;
    }

    private void OnInputActionChange(object obj, InputActionChange change)
    {
        if (change != InputActionChange.ActionPerformed) return;

        if (obj is InputAction action && action.activeControl != null)
        {
            var device = action.activeControl.device;

            if (device is Gamepad gamepad)
            {
                string manufacturer = device.description.manufacturer?.ToLower() ?? "";
                string product = device.description.product?.ToLower() ?? "";

                // Можно также анализировать gamepad.name или layout
                if ((manufacturer.Contains("microsoft") || product.Contains("xbox")) && _lastControlScheme != "Xbox")
                {
                    _lastControlScheme = "Xbox";
                    OnXboxGamepadDetected?.Invoke();
                }
                else if ((manufacturer.Contains("sony") || product.Contains("dualshock") || product.Contains("dualsense")) && _lastControlScheme != "PlayStation")
                {
                    _lastControlScheme = "PlayStation";
                    OnPlayStationGamepadDetected?.Invoke();
                }
            }
            else if (!(device is Gamepad) && _lastControlScheme != "KeyboardMouse")
            {
                _lastControlScheme = "KeyboardMouse";
                OnKeyboardMouseDetected?.Invoke();
            }
        }
    }
}
