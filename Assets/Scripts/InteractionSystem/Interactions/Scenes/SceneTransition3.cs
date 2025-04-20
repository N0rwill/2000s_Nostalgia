using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour, IInteractable
{
    public bool canInteract()
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        SceneManager.LoadScene("RoadRugLevel");
    }
}
