using UnityEngine;

public class LightPumpkin : MonoBehaviour, IInteractable
{
    //
    public bool CanInteract() => canInteract;
    public GameObject lightComponent;
    [SerializeField] private bool canInteract = false;
    public bool isOn = false;

    public void LightOn()
    {
        isOn = true;
        lightComponent.SetActive(true);
    }

    public void LightOff()
    {
        isOn = false;
        lightComponent.SetActive(false);
    }

    public void TogglePumpkinLight()
    {
        if(isOn)
        {
            LightOff();
        }
        else
        {
            LightOn();
        }
    }

    public void Interact()
    {
        if (CanInteract())
        {
            TogglePumpkinLight();
        }
        else
        {
            Debug.Log("Can't Interact");
        }
    }

    public void InRange()
    {
        canInteract = true;
    }
    public void OutofRange()
    {
        canInteract = false;
    }
}
