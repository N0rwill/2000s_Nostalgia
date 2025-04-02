using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player;
    public Transform teleportPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Teleporting player to " + teleportPoint.position);
            player.position = teleportPoint.position;
        }
    }
}
