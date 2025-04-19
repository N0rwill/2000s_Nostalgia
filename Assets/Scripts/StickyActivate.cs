using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyActivate : MonoBehaviour
{
    public AudioClip splat;

    void OnTriggerEnter(Collider _other) 
    { 
        if (_other.tag == "Player") 
        {
            _other.gameObject.GetComponent<AudioSource>().clip = splat;
            _other.gameObject.GetComponent<AudioSource>().Play();
            _other.gameObject.GetComponent<GrappleMovement>().enabled = true;
            _other.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            _other.gameObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
