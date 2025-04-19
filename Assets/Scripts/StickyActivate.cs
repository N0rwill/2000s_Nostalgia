using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyActivate : MonoBehaviour
{

    void OnTriggerEnter(Collider _other) 
    { 
        if (_other.tag == "Player") 
        {
            Destroy(gameObject);
            _other.gameObject.GetComponent<GrappleMovement>().enabled = true;
            _other.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
    }
}
