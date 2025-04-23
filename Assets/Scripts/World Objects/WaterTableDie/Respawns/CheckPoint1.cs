using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    [SerializeField] RResetMarble resetMarble;
    [SerializeField] GameObject deathTrigger1;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Marble"))
        {
            deathTrigger1.SetActive(false);
            
            resetMarble.shouldRespawn1 = false;
            resetMarble.shouldRespawn2 = true;
        }
    }
}
