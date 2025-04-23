using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    [SerializeField] RResetMarble resetMarble;
    [SerializeField] GameObject deathTrigger2;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Marble"))
        {
            deathTrigger2.SetActive(false);

            resetMarble.shouldRespawn2 = false;
            resetMarble.shouldRespawn3 = true;
        }
    }
}
