using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speedMultiplier = 0.2f;

    #region Serialized Fields
    [SerializeField] private float speed;
    [SerializeField] private float keyCount;
    #endregion




    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
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
