using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition3 : MonoBehaviour, IInteractable
{
    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool canInteract()
    {
        if (gameManager.Level2Complete == true && gameManager.Level3Complete == false)
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
