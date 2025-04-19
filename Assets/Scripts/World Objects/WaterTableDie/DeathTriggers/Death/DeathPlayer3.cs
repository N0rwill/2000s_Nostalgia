using UnityEngine;

public class DeathPlayer3 : MonoBehaviour
{
    public WaterTableRespawns waterTableRespawns;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            waterTableRespawns.SpawnPlayerCheckPoint3();
        }

        else if (trigger.gameObject.CompareTag("Marble"))
        {
            waterTableRespawns.SpawnMarbleCheckPoint3();
        }
    }
}
