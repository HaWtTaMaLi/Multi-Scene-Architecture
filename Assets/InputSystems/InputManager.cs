using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManager", menuName = "Input/InputManager")]
public class InputManager : ScriptableObject, InputSystem.IPlayerActions
{
    //Input managerrrrr

    //
    public InputSystem _inputSystem;

    //Events
    public event Action<Vector2> OnMoved;
    public event Action OnPaused;

    //Const checks, values that are constantly changed
    public Vector2 Direction => _inputSystem.Player.Move.ReadValue<Vector2>();

    //
    public void OnMove(InputAction.CallbackContext context)
    { 
        OnMoved?.Invoke(context.ReadValue<Vector2>());
        //Debug.Log("On Input Move Called");
    }

    //
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnPaused?.Invoke();
            Debug.Log("On Input Pause Called");
        }
    }

    //Make sure the input system is created
    public void EnsureGameInputWasCreated()
    {
        //if the input system is already created, return.. do nothing
        if (_inputSystem != null) return;

        //else, create a new input system
        _inputSystem = new InputSystem();

        //and set it to this scriptable object
        _inputSystem.Player.SetCallbacks(this);

        //enable input system
        //_inputSystem.Enable(); //to help toggle input on and off for menus and gameplay

        //load rebinds if they exist
    }

    //enable and disable the input systems
    #region Enable and Disable Inputs
    //Enable Player Input
    public void EnablePlayerInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.Player.Enable();
    }
    //Disable Player Input
    public void DisablePlayerInput() => _inputSystem.Player.Disable(); 

    //Enable UI Input
    public void EnableUIInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.UI.Enable();
    }
    //Disable UI Input
    public void DisableUIInput() => _inputSystem.UI.Disable();
    #endregion
}
