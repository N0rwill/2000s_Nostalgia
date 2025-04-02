using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionScript : MonoBehaviour
{
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 5f;
    [SerializeField]
    private LayerMask UseLayers;




    private void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
            {
                if (hit.collider.TryGetComponent<OpenDoor>(out OpenDoor door))
                {
                    if (door.IsOpen)
                    {
                        door.Close();
                    }
                    else
                    {
                        door.Open(transform.position);
                    }
                }
            }
        }

    }
}