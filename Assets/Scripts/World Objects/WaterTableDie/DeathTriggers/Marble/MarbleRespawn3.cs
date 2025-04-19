using UnityEngine;

public class MarbleRespawn3 : MonoBehaviour
{
    public WaterTableRespawns waterTableRespawns;

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Marble"))
        {
            waterTableRespawns.SpawnMarbleCheckPoint3();
        }
    }
}
