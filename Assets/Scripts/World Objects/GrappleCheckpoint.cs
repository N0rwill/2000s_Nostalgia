using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleCheckpoint : MonoBehaviour
{
    public GameObject deathTrigger;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            deathTrigger.GetComponent<Teleport>().teleportPoint = gameObject.transform;
        }
    }
}
