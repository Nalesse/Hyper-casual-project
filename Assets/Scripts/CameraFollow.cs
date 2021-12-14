using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float cameraSpeed;

    [SerializeField] private Vector3 endOffset;
    [SerializeField] private float cameraLerpDelay;
   

    private Vector3 offset;
    private bool doLerp;



    private void Awake()
    {
        offset = transform.position - player.transform.position;
    }

    private void Start()
    {
        StartCoroutine(CameraLerp());
    }

    private void LateUpdate()
    {
        if (doLerp)
        {
            // Stars a transition from the far cam to the close up cam at the start of the game 
            var startPos = offset;
            var endPos = endOffset;
            offset.z = endOffset.z;

            offset = Vector3.MoveTowards(startPos, endPos, cameraSpeed * Time.deltaTime);
        }

        transform.position = player.transform.position + offset;

    }

    private IEnumerator CameraLerp()
    {
        // Adds a delay so the camera does not move immediately 
        yield return new WaitForSeconds(cameraLerpDelay);
        doLerp = true;
        
    }
}
