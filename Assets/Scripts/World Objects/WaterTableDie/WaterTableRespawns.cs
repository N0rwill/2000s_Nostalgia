using UnityEngine;

public class WaterTableRespawns : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject marble;

    [SerializeField] Transform playerRespawnPoint1;
    [SerializeField] Transform playerRespawnPoint2;
    [SerializeField] Transform playerRespawnPoint3;

    [SerializeField] Transform marbleRespawnPoint1;
    [SerializeField] Transform marbleRespawnPoint2;
    [SerializeField] Transform marbleRespawnPoint3;

    void Start()
    {
        player.transform.position = playerRespawnPoint1.position;
    }

    public void SpawnPlayerCheckPoint1()
    {
        player.transform.position = playerRespawnPoint1.position;
    }

    public void SpawnPlayerCheckPoint2()
    {
        player.transform.position = playerRespawnPoint2.position;
    }

    public void SpawnPlayerCheckPoint3()
    {
        player.transform.position = playerRespawnPoint3.position;
    }

    public void SpawnMarbleCheckPoint1()
    {
        marble.transform.position = marbleRespawnPoint1.position;
    }

    public void SpawnMarbleCheckPoint2()
    {
        marble.transform.position = marbleRespawnPoint2.position;
    }

    public void SpawnMarbleCheckPoint3()
    {
        marble.transform.position = marbleRespawnPoint3.position;
    }
}
