using UnityEngine;

public class ShadowCaught : MonoBehaviour
{
    public Transform player;
    public Transform teleportPoint;

    public void Caught()
    {
        Debug.Log("Teleporting player to " + teleportPoint.position);
        player.position = teleportPoint.position;
    }
}
