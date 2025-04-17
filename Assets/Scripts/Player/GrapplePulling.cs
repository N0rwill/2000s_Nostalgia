using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePulling : MonoBehaviour
{
    public GameObject player;
    public bool pull = false;
    public float pullForce;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (pull == true) 
        {
            //If the player grapples the object, move the object toward the player.
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * pullForce);
        }
    }
}
