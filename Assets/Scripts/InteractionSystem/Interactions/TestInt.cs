using UnityEngine;

public class TestInt : MonoBehaviour, IInteractable
{
    public bool canInteract()
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        Debug.Log("Interacting with TestInt object!");
    }
}
