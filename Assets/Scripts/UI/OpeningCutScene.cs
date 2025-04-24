using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningCutScene : MonoBehaviour
{
    public GameObject cutSceneUI;
    public CanvasGroup canvasGroup; 

    void Start()
    {
        cutSceneUI.SetActive(true);
        canvasGroup.alpha = 0f;
        StartCoroutine(WaitAndMoveToHub(13f));
    }

    private IEnumerator WaitAndMoveToHub(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(FadeToBlack(1f)); // 1 second fade
        SceneManager.LoadScene("HubLevel");
    }

    private IEnumerator FadeToBlack(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
