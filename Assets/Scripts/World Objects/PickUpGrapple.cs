using UnityEngine;

public class PickUpGrapple : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ThirdPersonCamera thirdPersonCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerMovement != null)
            {
                playerMovement.hasGrapple = true;
                thirdPersonCamera.SwitchCameraStyle(ThirdPersonCamera.CameraStyle.Combat);
                Destroy(gameObject);
            }
        }
    }
}
