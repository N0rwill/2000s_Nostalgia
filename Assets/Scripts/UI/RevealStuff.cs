using UnityEngine;
using System.Collections;

public class RevealStuff : MonoBehaviour
{
    public GameObject cameraFixed;
    public GameObject cameraFree;

    public GameObject invisibleCollider;

    public GameObject winUI;

    public GameObject hubMusic;
    public GameObject endSound;

    public void Reveal()
    {
        cameraFree.SetActive(true);
        cameraFixed.SetActive(false);

        invisibleCollider.SetActive(false);

        hubMusic.SetActive(false);
        endSound.SetActive(true);

        // make win ui pop up for 5 seconds and then disappear
        StartCoroutine(WinUI());
    }

    public void ResetReveal()
    {
        cameraFree.SetActive(false);
        cameraFixed.SetActive(true);

        invisibleCollider.SetActive(true);

        hubMusic.SetActive(true);
        endSound.SetActive(false);

        Time.timeScale = 1f;
    }

    private IEnumerator WinUI()
    {
        winUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        winUI.SetActive(false);
    }
}
