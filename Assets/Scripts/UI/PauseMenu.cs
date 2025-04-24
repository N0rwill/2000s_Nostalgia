using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused = false;

    public GameObject pauseMenu;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OpeningCutScene");
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

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene("HubLevel");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
