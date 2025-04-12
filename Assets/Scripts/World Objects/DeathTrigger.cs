using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject marble;

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform marbleSpawnPoint;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = trigger.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }
            player.transform.position = playerSpawnPoint.position;
        }

        else if (trigger.gameObject.CompareTag("Marble"))
        {
            Rigidbody marbleRb = trigger.GetComponent<Rigidbody>();
            if (marbleRb != null)
            {
                marbleRb.velocity = Vector3.zero;
                marbleRb.angularVelocity = Vector3.zero;
            }
            marble.transform.position = marbleSpawnPoint.position;
        }
    } 
}
