using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningCutScene : MonoBehaviour
{
    public GameObject cutSceneUI;

    void Start()
    {
        cutSceneUI.SetActive(true);

        StartCoroutine(WaitAndMoveToHub(7f));
    }

    private IEnumerator WaitAndMoveToHub(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("HubLevel");
    }
}
