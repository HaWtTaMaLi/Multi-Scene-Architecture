using UnityEngine;

public class InputManager : ScriptableObject, InputSystem.IPlayerActions
{
    //Input managerrrrr

    //
    public InputSystem _inputSystem;

    //
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void EnsureGameInputWasCreated()
    {
        //if the input system is already created, return.. do nothing
        if (_inputSystem != null) return;

        //else, create a new input system
        _inputSystem = new InputSystem();
        //and set it to this scriptable object
        _inputSystem.Player.SetCallbacks(this);
        _inputSystem.Enable(); //to help toggle input on and off for menus and gameplay
    }
}
