using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition1 : MonoBehaviour, IInteractable
{
    public bool canInteract()
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        SceneManager.LoadScene("WaterLevel");
    }
}
