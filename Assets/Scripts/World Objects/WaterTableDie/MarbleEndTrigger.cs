using UnityEngine;

public class MarbleEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endBlock1;
    [SerializeField] private GameObject endBlock2;
    [SerializeField] private GameObject endBlock3;
    [SerializeField] private GameObject endBlock4;
    [SerializeField] private GameObject endBlock5;
    [SerializeField] private GameObject endBlock6;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            endBlock1.SetActive(true);
            endBlock2.SetActive(true);
            endBlock3.SetActive(true);
            endBlock4.SetActive(true);
            endBlock5.SetActive(true);
            endBlock6.SetActive(true);
        }
    }
}
