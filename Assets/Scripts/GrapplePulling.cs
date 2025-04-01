using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePulling : MonoBehaviour
{
    public GameObject player;
    public bool pull = false;
    public float pullForce;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pull == true) 
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rigidbody.AddForce(direction * pullForce);
        }
    }
}
