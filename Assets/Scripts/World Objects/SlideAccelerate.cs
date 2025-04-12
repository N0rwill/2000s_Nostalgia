using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAccelerate : MonoBehaviour
{
    [SerializeField] int speed;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Marble"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(speed * Vector3.left, ForceMode.Force);
        }
    }
}
