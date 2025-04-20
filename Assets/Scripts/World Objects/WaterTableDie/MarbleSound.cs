using UnityEngine;

public class MarbleSound : MonoBehaviour
{
    [SerializeField] private AudioSource marbleAudioSource;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        //bool shouldPlay = isColliding && rb.velocity.magnitude > 0.5f;

        if (rb.velocity.magnitude > 0.5f && !marbleAudioSource.isPlaying)
        {
            marbleAudioSource.Play();
        }
        else if (rb.velocity.magnitude < 0.1f && marbleAudioSource.isPlaying)
        {
            marbleAudioSource.Stop();
        }
    }
}
