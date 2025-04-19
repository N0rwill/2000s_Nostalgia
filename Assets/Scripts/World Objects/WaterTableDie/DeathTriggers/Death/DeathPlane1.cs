using UnityEngine;

public class DeathPlane1 : MonoBehaviour
{
    public WaterTableRespawns waterTableRespawns;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            waterTableRespawns.SpawnPlayerCheckPoint1();
        }

        else if (trigger.gameObject.CompareTag("Marble"))
        {
            waterTableRespawns.SpawnMarbleCheckPoint1();
        }
    }
}
