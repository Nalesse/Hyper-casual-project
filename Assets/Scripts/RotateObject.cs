using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float amountToRotate;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, amountToRotate, 0, Space.World);
    }
}
