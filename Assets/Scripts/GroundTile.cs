using System;

using UnityEngine;

using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private GameObject keyPrefab;
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
        SpawnKeys();
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

    private void SpawnKeys()
    {
        int keysToSpawn = 2;
        for (int i = 0; i < keysToSpawn; i++)
        {
            var temp = Instantiate(keyPrefab,transform);
            temp.transform.position = GetRandomPoint();
        }
    }

    private Vector3 GetRandomPoint()
    {
        var col = transform.GetComponent<Collider>().bounds;
        Vector3 point = new Vector3(
            Random.Range(col.min.x, col.max.x),
            Random.Range(col.min.y, col.max.y),
            Random.Range(col.min.z, col.max.z));

        point.y = 1;
        return point;
    }


}
