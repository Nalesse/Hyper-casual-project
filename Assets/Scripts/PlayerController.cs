using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject level;

    [SerializeField] private float repeatDistance;

    private Vector3 startPosition;


    private void Awake()
    {
        repeatDistance = level.GetComponentInChildren<BoxCollider>().bounds.size.z / 2;
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));

        Vector3 levelTransform = level.transform.position;

        if (transform.position.z >= repeatDistance)
        {
            transform.position = startPosition;
        }
    }

}
