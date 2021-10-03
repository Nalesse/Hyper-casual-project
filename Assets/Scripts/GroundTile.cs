using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private LevelGenerator levelGenerator;

    // Start is called before the first frame update
    void Awake()
    {
        levelGenerator = GameObject.FindObjectOfType<LevelGenerator>();
    }

    private void OnTriggerExit(Collider other)
    {
        levelGenerator.SpawnTile();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
