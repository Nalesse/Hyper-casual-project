using System;

using UnityEngine;

using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    private GameObject spawnedObstacle;
    private LevelGenerator levelGenerator;

    [SerializeField] private GameObject obstaclePrefab;

    // Start is called before the first frame update
    private void Awake()
    {
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
    }

    private void Start()
    {
        SpawnObstacle();
    }

    private void OnTriggerExit(Collider other)
    {
        levelGenerator.SpawnTile();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SpawnObstacle()
    {
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform parent = transform;
        Transform spawnPoint = parent.GetChild(obstacleSpawnIndex).transform;
        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, parent);

    }

}
