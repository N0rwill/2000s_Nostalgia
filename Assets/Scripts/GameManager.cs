using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }
    
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
}
