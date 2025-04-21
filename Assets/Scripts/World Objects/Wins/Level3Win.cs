using UnityEngine;
using System.Collections;

public class Level3Win : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject winItem;

    [SerializeField] private AudioSource winAudioSource;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.Level3Complete = true;
            winItem.GetComponent<Collider>().enabled = false;
            winItem.GetComponent<MeshRenderer>().enabled = false;
            winItem.GetComponent<Animator>().enabled = false;

            winAudioSource.Play();

            StartCoroutine(LoadHubDelayed());
        }
    }

    private IEnumerator LoadHubDelayed()
    {
        yield return new WaitForSeconds(3f);
        gameManager.GoToHub3();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            gameManager.GoToHub3();
        }
    }
}
