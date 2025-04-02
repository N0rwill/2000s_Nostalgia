using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePulling : MonoBehaviour
{
    public GameObject player;
    public bool pull = false;
    public float pullForce;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (pull == true) 
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rigidbody.AddForce(direction * pullForce);
        }
    }
}
