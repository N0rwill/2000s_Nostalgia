using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClone : MonoBehaviour
{
    public GameObject clone;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clone.SetActive(!clone.activeSelf);
            Destroy(gameObject);
        }
    }
}
