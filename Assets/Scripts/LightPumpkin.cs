using UnityEngine;

public class LightPumpkin : MonoBehaviour, IInteractable
{
    //
    public bool CanInteract() => canInteract;
    public GameObject lightComponent;
    private Outline outline;
    [SerializeField] private bool canInteract = false;
    public bool isOn = false;

    public void Awake()
    {
        //Set the outline component to the object
        outline = gameObject.AddComponent<Outline>();
        //Initialize the outline component
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 3f;
        //Disable the outline component on Awake
        outline.enabled = false;
    }
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
        outline.enabled = true;
        canInteract = true;
    }
    public void OutofRange()
    {
        outline.enabled = false;
        canInteract = false;
    }
}
