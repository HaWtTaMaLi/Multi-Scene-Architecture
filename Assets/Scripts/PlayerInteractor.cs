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

    public void OnEnable() => _inputManager.OnInteracted += Interact;
    public void OnDisable() => _inputManager.OnInteracted -= Interact;

    public void Interact()
    {
        currentInteractable?.Interact();
    }

    public void Update()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            // Check if the hit object has an IInteractable component
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.InRange();


                currentInteractable = interactable;
            }
        }
        else
        {
            currentInteractable.OutofRange();
        }
    }
}
