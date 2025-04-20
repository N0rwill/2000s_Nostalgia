using UnityEngine;

public class MarbleSound : MonoBehaviour
{
    [SerializeField] private AudioSource marbleAudioSource;

    [SerializeField] private Rigidbody rb;
    
    private bool isColliding = false;

    private void Update()
    {
        if (isColliding && rb.velocity.magnitude > 0.5f)
        {
            // Play the sound if moving and touching ground
            marbleAudioSource.Play();
        }
        else
        {
            // Stop the sound if not moving or not touching ground
            marbleAudioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object is a marble
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        // Check if the object is the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isColliding = false;
        }
    }
}
