public interface IInteractable
{
    bool CanInteract();
    void Interact();
    void InRange();
    void OutofRange();
}