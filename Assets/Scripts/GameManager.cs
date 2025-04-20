using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }
    
    private bool paused = false;

    [SerializeField] public GameObject pauseMenu;
    
    public bool Level1Complete;
    public bool Level2Complete;
    public bool Level3Complete;

    private void Awake()
    {
        // Keep GameManager between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void GoToHub1()
    {
        Level1Complete = true;
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Going to Hub Level.");
    }

    public void GoToHub2()
    {
        Level2Complete = true;
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Going to Hub Level.");
    }

    public void GoToHub3()
    {
        Level3Complete = true;
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Going to Hub Level.");
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
}
