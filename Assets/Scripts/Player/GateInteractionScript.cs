using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionScript : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private float MaxUseDistance = 5f;
    [SerializeField] private LayerMask UseLayers;

    /*private void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
            {
                if (hit.collider.TryGetComponent<MoveGate>(out MoveGate moveGate))
                {
                    if (moveGate.isOpen)
                    {
                        moveGate.Close();
                    }
                    else
                    {
                        moveGate.Open();
                    }
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (Camera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(Camera.position, Camera.forward * MaxUseDistance);
        }
    }*/
}