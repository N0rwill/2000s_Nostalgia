using UnityEngine;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayers;
    
    public Animator animator;
    
    // list of interactables in range of player
    private List<IInteractable> nearbyInteractables = new List<IInteractable>();
    private IInteractable currentInteractable;

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            animator.SetTrigger("Interact");
            if (currentInteractable != null && currentInteractable.canInteract())
            {
                currentInteractable.Interact(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // adds interactable to list
        if (((1 << other.gameObject.layer) & interactableLayers) != 0)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null && !nearbyInteractables.Contains(interactable))
            {
                nearbyInteractables.Add(interactable);
                UpdateCurrentInteractable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // removes interactable from list when the interacable is out of range
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            nearbyInteractables.Remove(interactable);
            UpdateCurrentInteractable();
        }
    }

    private void UpdateCurrentInteractable()
    {
        // Find the closest interactable
        float closestDistance = float.MaxValue;
        currentInteractable = null;

        foreach (IInteractable interactable in nearbyInteractables)
        {
            if (interactable.canInteract())
            {
                MonoBehaviour mono = interactable as MonoBehaviour;
                if (mono != null)
                {
                    float distance = Vector3.Distance(transform.position, mono.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        currentInteractable = interactable;
                    }
                }
            }
        }
    }
    
    // Call this if any nearby interactable's state changes
    public void RefreshInteractables()
    {
        UpdateCurrentInteractable();
    }
}
