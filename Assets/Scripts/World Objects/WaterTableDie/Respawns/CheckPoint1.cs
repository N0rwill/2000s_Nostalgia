using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    [SerializeField] GameObject deathTrigger1;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Marble"))
        {
            deathTrigger1.SetActive(false);
        }
    }
}
