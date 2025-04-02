using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpWheely : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovement != null)
            {
                playerMovement.hasWheely = true;
                Destroy(gameObject);
            }
        }
    }
}
