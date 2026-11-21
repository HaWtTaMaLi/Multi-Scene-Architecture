using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManager", menuName = "Input/InputManager")]
public class InputManager : ScriptableObject, InputSystem.IPlayerActions
{
    //
    public static InputManager Instance;
    public InputSystem _inputSystem;

    //Events
    public event Action<Vector2> OnMoved;
    public event Action<Vector2> OnLooked;

    public event Action OnPaused;
    public event Action OnNext;
    public event Action OnJumped;
    public event Action OnInteracted;

    //Const checks, values that are constantly changed
    public Vector2 Direction => _inputSystem.Player.Move.ReadValue<Vector2>();

    public void OnEnable() => EnsureGameInputWasCreated();
    public void OnDisable() => DisablePlayerInput();

    public void OnMove(InputAction.CallbackContext context)
    { 
        OnMoved?.Invoke(context.ReadValue<Vector2>());
        //Debug.Log("On Input Move Called");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Debug.Log("InputManager recived Look"); //Works

        OnLooked?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnPaused?.Invoke();
            Debug.Log("On Input Pause Called");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log("InputManager recived Jump"); //Works

        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log("Invoking OnJumped"); //Works

            OnJumped?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //Debug.Log("Invoking OnInteract"); //Works

            OnInteracted?.Invoke();
        }
    }

    //TEMP
    public void OnNextMatch(InputAction.CallbackContext context)
    {
        //temp till i set up a way to end the match
        if(context.phase == InputActionPhase.Performed)
        {
            OnNext?.Invoke();
            Debug.Log("On Input Next Match");
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
        _inputSystem.Player.SetCallbacks(instance: this);

        //enable input system
        _inputSystem.Player.Enable(); //to help toggle input on and off for menus and gameplay

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
