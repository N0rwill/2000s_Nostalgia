using System.Collections;
using UnityEngine;

public class LevelStartUI : MonoBehaviour
{
    public GameObject levelUI;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = levelUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = levelUI.AddComponent<CanvasGroup>();

        levelUI.SetActive(true);
        canvasGroup.alpha = 0f;
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade In
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Wait
        yield return new WaitForSeconds(2.5f);

        // Fade Out
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        levelUI.SetActive(false);
    }
}
