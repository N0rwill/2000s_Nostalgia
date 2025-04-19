using UnityEngine;

public class DeathPlane2 : MonoBehaviour
{
    public WaterTableRespawns waterTableRespawns;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            waterTableRespawns.SpawnPlayerCheckPoint2();
        }

        else if (trigger.gameObject.CompareTag("Marble"))
        {
            waterTableRespawns.SpawnMarbleCheckPoint2();
        }
    }
}
