using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition1 : MonoBehaviour, IInteractable
{
    public GameManager GameManager;

    public bool canInteract()
    {
        if (GameManager.Level1Complete == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Interact(Interactor interactor)
    {
        SceneManager.LoadScene("WaterLevel");
    }
}
