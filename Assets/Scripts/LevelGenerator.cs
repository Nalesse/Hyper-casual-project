using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundTile;

    [SerializeField] private int tilesToPreSpawn;

    private Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
        GameObject spawnedTile = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = spawnedTile.transform.GetChild(1).transform.position;


    }

    // Start is called before the first frame update
    private void Start()
    {
        // Spawns tiles at the start of the game so that the player can't see the tiles being spawned in front of them.
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            SpawnTile();
        }
    }

}
