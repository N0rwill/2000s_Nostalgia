using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameObject endUI;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = endUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = endUI.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        endUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(EndGame), 3f);
        }
    }

    void EndGame()
    {
        endUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(FadeInUI());
    }

    private System.Collections.IEnumerator FadeInUI()
    {
        float duration = 2f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSecondsRealtime(5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
