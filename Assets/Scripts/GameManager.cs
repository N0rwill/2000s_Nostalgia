using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button returnButton;
    public Button quitButton;
    private bool paused = false;

    [SerializeField] public GameObject pauseMenu;

    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            paused = !paused;
            Pause(paused);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("WaterLevel");
        Debug.Log("Start game.");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
        Debug.Log("Returned to Menu.");
    }

    public void Pause(bool paused)
    {
        
        if (paused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (paused == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }
}
