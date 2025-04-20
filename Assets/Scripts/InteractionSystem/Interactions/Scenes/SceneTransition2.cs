using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition2 : MonoBehaviour, IInteractable
{
    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool canInteract()
    {
        if (gameManager.Level1Complete == true && gameManager.Level2Complete == false)
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
        SceneManager.LoadScene("StickyHandLevel");
    }
}
