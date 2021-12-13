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
            var startPos = offset;
            var endPos = endOffset;
            offset.z = endOffset.z;

            //transform.position = Vector3.MoveTowards(startPos, endPos, cameraSpeed * Time.deltaTime);
            offset = Vector3.MoveTowards(startPos, endPos, cameraSpeed * Time.deltaTime);
        }

        transform.position = player.transform.position + offset;

    }

    private IEnumerator CameraLerp()
    {
        yield return new WaitForSeconds(cameraLerpDelay);
        doLerp = true;
        
    }
}
