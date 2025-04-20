using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour, IInteractable
{
    public GameManager GameManager;

    public bool canInteract()
    {
        if (GameManager.Level2Complete == true && GameManager.Level3Complete == false)
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
        SceneManager.LoadScene("RoadRugLevel");
    }
}
