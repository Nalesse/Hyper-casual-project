using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float BufferDistance = 20.0f;
    private float repeatDistance;
    private Vector3 startPosition;

    [SerializeField] private float keyCount;
    private float speedMultiplier = 0.2f;

    #region Serialized Fields
    [SerializeField] private float speed;
    [SerializeField] private GameObject level;
    #endregion


    private void Awake()
    {
        repeatDistance = level.GetComponentInChildren<BoxCollider>().bounds.size.z - BufferDistance;
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

        if (transform.position.z >= repeatDistance)
        {
            transform.position = startPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            keyCount += 1;
            speed += speedMultiplier * keyCount;

            if (speed > 25)
            {
                speed = 25;
            }
            
        }
    }
}
