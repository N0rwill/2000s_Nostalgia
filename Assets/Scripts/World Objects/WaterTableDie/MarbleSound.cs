using UnityEngine;

public class MarbleSound : MonoBehaviour
{
    [SerializeField] private AudioSource marbleAudioSource;

    [SerializeField] private Rigidbody rb;

    private void OnCollisionStay(Collision collision)
    {
        // Check if the object is a marble
        if (collision.gameObject.CompareTag("Marble"))
        {
            // Check if the marble is moving
            if (rb.velocity.magnitude > 0.1f)
            {
                // Play the sound
                marbleAudioSource.Play();
            }
            else if (rb.velocity.magnitude < 0.5f)
            {
                // Stop the sound if the marble is not moving
                marbleAudioSource.Stop();
            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        // Check if the object is a marble
        if (collision.gameObject.CompareTag("Marble"))
        {
            // Stop the sound when the marble exits the collision
            marbleAudioSource.Stop();
        }
    }
}
