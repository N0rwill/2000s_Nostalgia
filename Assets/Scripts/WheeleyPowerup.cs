using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheeleyPowerup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component from the colliding object
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.hasWheely = true;
            }
            Destroy(gameObject);
        }
    }
}