using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    [SerializeField] GameObject deathTrigger2;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Marble"))
        {
            deathTrigger2.SetActive(false);
        }
    }
}
