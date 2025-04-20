using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheeleyPowerup : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }
}
