using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private GameManager gameManager;
    private Transform player;

    [SerializeField] private float maxDistance;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Moves the enemy forward and follows the player on the x axis
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        var enemyPos = transform.position;
        enemyPos.x = player.position.x;
        transform.position = enemyPos;

        // Maintains a distance between the enemy and player so that the player does not reach a point where being caught is impossible
        if (Vector3.Distance(transform.position, player.transform.position) > maxDistance)
        {
            enemyPos.z = player.transform.position.z - (maxDistance / 2);
            transform.position = enemyPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Caught Player");
            gameManager.GameOver();
        }
    }
}
