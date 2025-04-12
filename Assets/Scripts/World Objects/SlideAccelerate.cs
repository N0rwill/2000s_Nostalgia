using UnityEngine;

public class SlideAccelerate : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Marble"))
        {
            Rigidbody marbleRb = other.GetComponent<Rigidbody>();
            if (marbleRb != null)
            {
                // Get the current velocity direction and apply acceleration
                Vector3 accelerationDirection = marbleRb.velocity.normalized;
                marbleRb.AddForce(accelerationDirection * accelerationForce, ForceMode.Impulse);
            }
        }
    }
}
