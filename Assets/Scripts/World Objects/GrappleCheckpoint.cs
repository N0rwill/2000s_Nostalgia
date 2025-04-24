using UnityEngine;

public class GrappleCheckpoint : MonoBehaviour
{
    public GameObject deathTrigger;
    public Rigidbody rb;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            deathTrigger.GetComponent<Teleport>().teleportPoint = gameObject.transform;
            rb.velocity = Vector3.zero;
        }
    }
}
