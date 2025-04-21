using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    public static GameManager Instance { get; private set; }

    [Header("Level Completion Flags")]
    public bool Level1Complete;
    public bool Level2Complete;
    public bool Level3Complete;

    [Header("Particle Effects")]
    [SerializeField] private GameObject level1Particle;
    [SerializeField] private GameObject level2Particle;
    [SerializeField] private GameObject level3Particle;

    [Header("Ending Sequence")]
    [SerializeField] RevealStuff revealStuff;

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

        ParticleLogic();

        SceneManager.sceneLoaded += OnSceneLoaded; // Add this line
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Clean up event subscription
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        revealStuff = FindObjectOfType<RevealStuff>();
    }

    public void GoToHub1()
    {
        Level1Complete = true;
        SceneManager.LoadScene("HubLevel");

        ParticleLogic();

        Debug.Log("Going to Hub Level.");
    }

    public void GoToHub2()
    {
        Level2Complete = true;
        SceneManager.LoadScene("HubLevel");

        ParticleLogic();

        Debug.Log("Going to Hub Level.");
    }

    public void GoToHub3()
    {
        Level3Complete = true;
        SceneManager.LoadScene("HubLevel");

        ParticleLogic();
        StartCoroutine(WaitAndEndGame());

        Debug.Log("Going to Hub Level.");
    }

    private void ParticleLogic()
    {
        if (Level1Complete)
        {
            level1Particle.SetActive(false);
            level2Particle.SetActive(true);
            level3Particle.SetActive(false);
        }

        if (Level2Complete && Level1Complete)
        {
            level1Particle.SetActive(false);
            level2Particle.SetActive(false);
            level3Particle.SetActive(true);
        }
        if (Level3Complete && Level2Complete && Level1Complete)
        {
            level1Particle.SetActive(false);
            level2Particle.SetActive(false);
            level3Particle.SetActive(false);
        }
    }
    private IEnumerator WaitAndEndGame()
    {
        yield return new WaitForSeconds(1f);
        if (revealStuff != null)
        {
            revealStuff.Reveal();
        }
        else
        {
            Debug.LogWarning("SwitchCam reference not set in GameManager!");
        }
    }
}
