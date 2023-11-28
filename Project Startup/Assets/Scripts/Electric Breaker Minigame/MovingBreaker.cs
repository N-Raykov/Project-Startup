using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingBreaker : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float leftMovementBoundary = 2f;
    [SerializeField] float rightMovementBoundary = 2f;

    private bool isMovingLeft = true;

    private bool inPosition = false;

    private Vector3 emptySpotPosition;

    void Update()
    {
        MoveLeftAndRight();

        // Check for player input to click on the moving breaker
        if (inPosition == true && Input.GetMouseButtonDown(0))
        {
            BreakerManager breakerManager = FindObjectOfType<BreakerManager>();
            if (breakerManager != null)
            {
                breakerManager.HandleMovingBreakerPlaced(new Vector3(emptySpotPosition.x, emptySpotPosition.y, emptySpotPosition.z + 0.5f));
            }

            Destroy(gameObject);
        }
    }

    void MoveLeftAndRight()
    {
        float movement = movementSpeed * Time.deltaTime;

        if (transform.position.x <= leftMovementBoundary)
        {
            isMovingLeft = false;
        }
        else if (transform.position.x >= rightMovementBoundary)
        {
            isMovingLeft = true;
        }

        if (isMovingLeft)
        {
            transform.Translate(Vector3.left * movement);
        }
        else
        {
            transform.Translate(Vector3.right * movement);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the moving breaker enters the empty spot left by the previous breaker
        if (other.CompareTag("EmptySpot"))
        {
            emptySpotPosition = other.transform.position;
            inPosition = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Reset the clickability when leaving the empty spot
        if (other.CompareTag("EmptySpot"))
        {
            inPosition = false;
        }
    }
}

