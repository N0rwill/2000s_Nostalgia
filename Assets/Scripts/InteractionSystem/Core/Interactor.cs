using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private float maxUseDistance = 5f;
    [SerializeField] private LayerMask useLayers;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Interact button pressed");
            if (DoInteractionTest(out IInteractable interactable))
            {
                Debug.Log("Interactable found: " + interactable);
                if (interactable.canInteract())
                {
                    interactable.Interact(this);
                }
            }
        }
    }

    private bool DoInteractionTest(out IInteractable interactable)
    {
        interactable = null;

        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, maxUseDistance, useLayers))
        {
            interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                Debug.Log("Hit interactable: " + interactable);
                return true;
            }

            return false;
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 rayStart = Camera.position;
        Vector3 rayEnd = rayStart + (Camera.forward * maxUseDistance);
        
        // Draw the ray line
        Gizmos.DrawLine(rayStart, rayEnd);
        // Draw a small sphere at the end of the ray for better visualization
        Gizmos.DrawWireSphere(rayEnd, 0.1f);
    }
}
