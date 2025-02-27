using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private Vector2 swipeDelta;

    public float swipeSensitivity = 0.1f; // Adjust the sensitivity for drag distance
    public float moveSpeed = 5f; // Movement speed for the object

    private bool isDragging = false;

    void Update()
    {
        HandleSwipe();
    }

    void HandleSwipe()
    {
        if (Input.touchCount > 0) // For mobile touch input
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // When touch starts, record the initial position
                    touchStartPos = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    // When the touch moves, calculate the delta (movement)
                    swipeDelta = touch.position - touchStartPos;
                    MoveObject();
                    break;

                case TouchPhase.Ended:
                    // When touch ends, reset dragging
                    isDragging = false;
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0)) // For mouse input (testing in editor)
        {
            touchStartPos = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            swipeDelta = (Vector2)Input.mousePosition - touchStartPos;
            MoveObject();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void MoveObject()
    {
        // Check swipe direction and move the object
        if (swipeDelta.magnitude > swipeSensitivity)
        {
            // Move the object based on swipe direction
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
            {
                if (swipeDelta.x > 0) // Swipe right
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                else // Swipe left
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
            else // Vertical swipe
            {
                if (swipeDelta.y > 0) // Swipe up
                    transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                else // Swipe down
                    transform.position += Vector3.down * moveSpeed * Time.deltaTime;
            }

            // Reset the touch position to avoid continuous movement
            touchStartPos = Input.mousePosition;
        }
    }
}
