using UnityEngine;
using System.Collections;

public class Level2Win : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject winItem;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.Level2Complete = true;
            winItem.GetComponent<Collider>().enabled = false;
            winItem.GetComponent<Renderer>().enabled = false;
            StartCoroutine(LoadHubDelayed());
        }
    }

    private IEnumerator LoadHubDelayed()
    {
        yield return new WaitForSeconds(2f);
        gameManager.GoToHub2();
    }
}
