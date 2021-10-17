using System;

using UnityEngine;
using UnityEngine.Serialization;

using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    private LevelGenerator levelGenerator;

    [SerializeField] private GameObject[] items;

    // Start is called before the first frame update
    private void Awake()
    {
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
    }

    private void Start()
    {
        SpawnItems();
    }

    private void OnTriggerExit(Collider other)
    {
        levelGenerator.SpawnTile();
        Destroy(gameObject, 2);
    }

    public void SpawnItems()
    {
        int itemSpawnPosIndex = Random.Range(2, 5);
        int itemIndex = Random.Range(0, items.Length);
        Transform parent = transform;
        Transform spawnPoint = parent.GetChild(itemSpawnPosIndex).transform;
        Instantiate(items[itemIndex], spawnPoint.position, Quaternion.identity, parent);

    }


}
