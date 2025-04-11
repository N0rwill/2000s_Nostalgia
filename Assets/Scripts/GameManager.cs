using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool paused = false;

    [SerializeField] public GameObject pauseMenu;

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause(paused);
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("HubLevel");
        Debug.Log("Start game.");
    }

    public void Pause(bool paused)
    {
        if (paused == false)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (paused == true)
        {
            paused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
