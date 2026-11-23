using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    //
    public InputManager _inputManager;
    public Transform playerCamera;
    public float interactionDistance = 5f;
    //set the layers in the inspector
    public LayerMask interactableLayer;
    public IInteractable currentInteractable;
    public GameObject lastHit;
    public float coolDownTime = 30;
    public TMP_Text displayInteractable;
    public string nothing = "";

    public void OnEnable() => _inputManager.OnInteracted += Interact;
    public void OnDisable() => _inputManager.OnInteracted -= Interact;

    public void Interact()
    {
        currentInteractable?.Interact();
    }

    public void FixedUpdate()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            // Check if the hit object has an IInteractable component
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.InRange();

                //store the hit object in a variable
                lastHit = hit.transform.gameObject;

                //print to log
                Debug.Log(lastHit.ToString());

                //display the hit object in the UI
                displayInteractable.text = lastHit.name.ToString();

                currentInteractable = interactable;
            }
        }
        else
        {
            //this is much more efficiant then the outline so i scrapped it for this instead
            displayInteractable.text = nothing.ToString();

            //
            currentInteractable.OutofRange();
        }
    }
}
