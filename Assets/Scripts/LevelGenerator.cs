using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private int tilesToPreSpawn;
    [SerializeField] private Transform startPoint;
    [SerializeField] private PlatformTile tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = startPoint.position;
        for(int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.startPoint.localPosition;
            PlatformTile spawnedtile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as PlatformTile;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
