using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Cashed Animator parameters 
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int LeftStrafe = Animator.StringToHash("LeftStrafe");
    private static readonly int RightStrafe = Animator.StringToHash("RightStrafe");
    private static readonly int Stumble = Animator.StringToHash("Stumble");

    /// <summary>
    /// How much to increase or decrease the speed
    /// </summary>
    private float speedIncrease = 0.5f;

    /// <summary>
    /// Reference to the GameManager script
    /// </summary>
    private GameManager gameManager;

    /// <summary>
    /// Reference to the Enemy script 
    /// </summary>
    private Enemy enemy;

    /// <summary>
    /// The current snap position, this values changes depending on the moveIndex variable 
    /// </summary>
    private Vector3 snapPos;

    /// <summary>
    /// the index for the array containing the snap positions. Dictates where to move the player.
    /// </summary>
    private int moveIndex = 1;

    private Animator animator;

    #region Serialized Fields
    [SerializeField] private float speed;

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
    private bool stopTouch;
    [Header("Touch Variables")]
    [SerializeField] private float swipeRange;


    #endregion

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        enemy = GameObject.FindObjectOfType<Enemy>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        PlayerInput();
        animator.SetFloat(Speed, speed);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region PlayerMovement

    private void PlayerInput()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                {
                    startTouchPosition = Input.GetTouch(0).position;
                    break;
                }

            case TouchPhase.Moved:
                {
                    if (stopTouch)
                    {
                        break;
                    }

                    currentPosition = Input.GetTouch(0).position;
                    Vector2 distance = currentPosition - startTouchPosition;

                    if (distance.x < -swipeRange)
                    {
                        stopTouch = true;

                        // Decreases the move index by 1 and then sets the snap point to the one that is on the left of the current snap point
                        moveIndex -= 1;
                        animator.SetTrigger(LeftStrafe);
                        SetSnapPos();
                    }
                    else if (distance.x > swipeRange)
                    {
                        stopTouch = true;

                        // Increases the move index by 1 and then sets the snap point to the one that is on the right of the current snap point
                        moveIndex += 1;
                        animator.SetTrigger(RightStrafe);
                        SetSnapPos();
                    }

                    break;
                }

            case TouchPhase.Ended:
                {
                    stopTouch = false;
                    break;
                }
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

    #endregion


    private void OnTriggerEnter(Collider other)
    {
        // if statements are to avoid issues with negative numbers, other issues
        if (other.gameObject.CompareTag("key"))
        {
            gameManager.keyCount += 1;
            if (gameManager.keyCount > gameManager.keyWinAmount)
            {
                gameManager.keyCount = gameManager.keyWinAmount;
            }

            speed += speedIncrease;
            if (speed > gameManager.MaxSpeed)
            {
                speed = gameManager.MaxSpeed;
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            animator.SetTrigger(Stumble);
            gameManager.keyCount -= 2;
            enemy.speed += 1;
            if (gameManager.keyCount <= 0)
            {
                gameManager.keyCount = 0;
            }

            speed -= speedIncrease;
            if (speed < 8)
            {
                speed = 8;
            }

        }
    }

}
