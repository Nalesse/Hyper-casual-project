using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speedMultiplier = 0.2f;

    private Vector3 snapPos;
    private int moveIndex = 1;

    #region Serialized Fields
    [SerializeField] private float speed;
    [SerializeField] private float keyCount;

    [Header("Player Snap Positions")]
    [SerializeField] private float leftPos;
    [SerializeField] private float middlePos;
    [SerializeField] private float rightPos;

    [Header("Left and Right Smoothing")]
    [Range(0.0f, 1.0f)]
    [Tooltip("Closer to 0 adds more smoothing, closer to 1 adds less smoothing")]
    [SerializeField]
    private float smoothing;
    #endregion

    #region Touch Variables

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private bool stopTouch = false;
    [Header("Touch Variables")]
    [SerializeField] private float swipeRange;
    #endregion

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        PlayerInput();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 distance = currentPosition - startTouchPosition;


            if (!stopTouch)
            {
                if (distance.x < -swipeRange)
                {
                    Debug.Log("Swiped Left");
                    stopTouch = true;

                    // Decreases the move index by 1 and then sets the snap point to the one that is on the left of the current snap point 
                    moveIndex -= 1;
                    SetSnapPos();
                }
                else if (distance.x > swipeRange)
                {
                    Debug.Log("Swiped Right");
                    stopTouch = true;

                    // Increases the move index by 1 and then sets the snap point to the one that is on the right of the current snap point
                    moveIndex += 1;
                    SetSnapPos();
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
        }
    }

    private void SetSnapPos()
    {
        // Makes sure that the moveIndex is not outside the bounds of the array
        if (moveIndex < 0)
        {
            moveIndex = 0;
        }

        if (moveIndex > 2)
        {
            moveIndex = 2;
        }

        // Stores the x values for the  snap points 
        float[] posValues = { leftPos, middlePos, rightPos };

        // Sets the snap position to the players position then sets the x component to the current index of the position array to get the final snap point.
        var playerPos = transform;
        snapPos = playerPos.position;
        snapPos.x = posValues[moveIndex];
    }

    private void MovePlayer()
    {
        // Sets the players position to the current snap point by interpolating between the players current position and the snap point.
        // The reason for this is to smooth out the transition between snap points to make it less jarring. A new vector 3 is created so that only the x axis is affected by the lerp.
        var playerPos = transform.position;
        Vector3 xPos = playerPos;
        xPos.x = Mathf.Lerp(playerPos.x, snapPos.x, smoothing);
        transform.position = xPos;
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
