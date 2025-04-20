using UnityEngine;

public class Boat_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    [Header("Player Interaction")]
    public Transform boatSeat;                // Assign sitPosition
    public GameObject player;                 // Assign Player
    public PlayerMovement playerMovement;     // Assign PlayerMovement script
    public CharacterController characterController; // Assign CharacterController
    public KeyCode enterExitKey = KeyCode.T;

    public Transform originalParent;     

    private bool isPlayerInBoat = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        Debug.Log("Boat initialized with Rigidbody");
    }

    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            Debug.Log("T pressed");
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log("Distance from boat: " + distance);

            if (!isPlayerInBoat && distance < 10f)
            {
                Debug.Log("Attempting to enter boat...");
                EnterBoat();
            }
            else if (isPlayerInBoat)
            {
                Debug.Log("Attempting to exit boat...");
                ExitBoat();
            }
        }

        if (isPlayerInBoat)
        {
            HandleMovement();
            LockPlayerToSeat();
        }
    }

	void EnterBoat()
	{
		isPlayerInBoat = true;
		Debug.Log("Player is entering the boat.");
	
		// Disable controls
		if (playerMovement != null) playerMovement.enabled = false;
		if (characterController != null) characterController.enabled = false;
	
		// Parent and correct position
		player.transform.SetParent(boatSeat, worldPositionStays: false);
	
		// Offset player height to sit in the seat (adjust Y as needed)
		player.transform.localPosition = new Vector3(0f, -1f, 0f); // Lower Y = closer to the boat
		player.transform.localRotation = Quaternion.identity;
	
		Debug.Log("Player seated on boat");
	}
	
	
	
    void ExitBoat()
    {
        isPlayerInBoat = false;
        Debug.Log("Player is exiting the boat.");

        // Unparent the player and set back to the original parent
        if (originalParent != null)
        {
            player.transform.SetParent(originalParent);  // Set the player's parent back to the original parent
            Debug.Log("Player is now parented back to: " + originalParent.name);
        }
        else
        {
            Debug.LogWarning("Original parent is not assigned!");
        }

        // Set player's position beside the boat
        player.transform.position = transform.position + transform.right * 2f;
        player.transform.rotation = Quaternion.identity;

        // Enable player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            Debug.Log("Player movement script re-enabled");
        }

        if (characterController != null)
        {
            characterController.enabled = true;
            Debug.Log("Character controller re-enabled");
        }
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        if (moveInput != 0 || turnInput != 0)
        {
            Debug.Log($"Boat input: Move={moveInput}, Turn={turnInput}");
        }

        Vector3 move = -transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        Quaternion turn = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }

    void LockPlayerToSeat()
    {
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
    }
}
