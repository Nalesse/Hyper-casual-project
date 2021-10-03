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
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            SpawnTile();
        }
    }

}
