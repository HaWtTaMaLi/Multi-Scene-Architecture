using UnityEngine;

public class InputManager : ScriptableObject, InputSystem.IPlayerActions
{
    //Input managerrrrr

    //
    public InputSystem _inputSystem;

    //Enable
    //public void OnEnable => EnsureGameInputWasCreated();

    //public void OnDisable => _inputSystem?.Player.Disable();

    //
    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
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
        _inputSystem.Enable(); //to help toggle input on and off for menus and gameplay
    }

    //enable and disable the input systems
    #region Enable and Disable Inputs

    public void EnableAllInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.Player.Enable();
        _inputSystem.UI.Enable();
    }

    public void DisableAllInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.Player.Disable();
        _inputSystem.UI.Disable();
    }

    public void EnablePlayerInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.Player.Enable();
    }

    public void DisablePlayerInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.Player.Disable();
    }

    public void EnableUIInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.UI.Enable();
    }
    public void DisableUIInput()
    {
        EnsureGameInputWasCreated();
        _inputSystem.UI.Disable();
    }
    #endregion
}
