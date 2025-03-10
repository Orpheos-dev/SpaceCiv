using UnityEngine;
using System.Collections.Generic;

public class Swipe : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 swipeDelta;
    private Vector3 initialPosition;
    private int currentPhase = 0;
    private bool actionConfirmed = false;

    public float swipeSensitivity = 50f; // Adjust sensitivity
    private bool isDragging = false;
    public List<OptionGenerator> optionGenerators; // Supports multiple generators

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        HandleSwipe();
    }

    void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isDragging = true;
                    actionConfirmed = false;
                    break;

                case TouchPhase.Moved:
                    swipeDelta = touch.position - touchStartPos;
                    MoveObject(swipeDelta);
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    if (!actionConfirmed)
                    {
                        ProcessSelection();
                    }
                    ResetPosition();
                    break;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isDragging = true;
            actionConfirmed = false;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            swipeDelta = (Vector2)Input.mousePosition - touchStartPos;
            MoveObject(swipeDelta);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (!actionConfirmed)
            {
                ProcessSelection();
            }
            ResetPosition();
        }
    }

    void MoveObject(Vector2 swipe)
    {
        if (isDragging)
        {
            Vector3 newPosition = initialPosition;
            
            if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)) // Horizontal movement
            {
                newPosition.x = initialPosition.x + swipe.x * 0.01f; // Scale movement
            }
            else // Vertical movement
            {
                newPosition.y = initialPosition.y + swipe.y * 0.01f; // Scale movement
            }
            
            transform.position = newPosition;
        }
    }

    void ProcessSelection()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f);
        if (hit != null && !actionConfirmed)
        {
            foreach (var optionGenerator in optionGenerators)
            {
                int optionIndex = optionGenerator.phases[currentPhase].options.IndexOf(hit.gameObject);
                if (optionIndex != -1)
                {
                    string selectedOption = optionGenerator.phases[currentPhase].optionNames[optionIndex];
                    Debug.Log("Phase " + (currentPhase + 1) + ": " + selectedOption);
                    
                    actionConfirmed = true;
                    
                    if (currentPhase < optionGenerator.phases.Count - 1)
                    {
                        currentPhase++;
                        optionGenerator.NextPhase();
                    }
                    else
                    {
                        Debug.Log("Final Phase reached.");
                    }
                    return; // Exit loop once we process the valid selection
                }
            }
        }
    }

    void ResetPosition()
    {
        transform.position = initialPosition;
    }
}