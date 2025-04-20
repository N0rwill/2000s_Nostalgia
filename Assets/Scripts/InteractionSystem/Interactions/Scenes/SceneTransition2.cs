using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition2 : MonoBehaviour, IInteractable
{
    public bool canInteract()
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        SceneManager.LoadScene("StickyHandLevel");
    }
}
