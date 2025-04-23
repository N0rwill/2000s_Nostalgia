using UnityEngine;

public class RResetMarble : MonoBehaviour
{
    [SerializeField] GameObject marble;
    Rigidbody rb;

    [SerializeField] GameObject respawnPoint1;
    [SerializeField] GameObject respawnPoint2;
    [SerializeField] GameObject respawnPoint3;

    public bool shouldRespawn1;
    public bool shouldRespawn2;
    public bool shouldRespawn3;

    void Start()
    {
        rb = marble.GetComponent<Rigidbody>();

        shouldRespawn1 = true;
        shouldRespawn2 = false;
        shouldRespawn3 = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("ResetMarble"))
        {
            ResetMarble();
        }
    }

    void ResetMarble()
    {
        if (shouldRespawn1)
        {
            rb.velocity = Vector3.zero;

            marble.transform.position = respawnPoint1.transform.position;
        }
        else if (shouldRespawn2)
        {
            rb.velocity = Vector3.zero;

            marble.transform.position = respawnPoint2.transform.position;
        }
        else if (shouldRespawn3)
        {
            rb.velocity = Vector3.zero;

            marble.transform.position = respawnPoint3.transform.position;
        }
    }
}
