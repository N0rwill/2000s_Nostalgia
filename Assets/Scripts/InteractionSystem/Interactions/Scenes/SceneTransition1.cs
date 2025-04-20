using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition1 : MonoBehaviour, IInteractable
{
    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public bool canInteract()
    {
        if (gameManager.Level1Complete == false)
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
