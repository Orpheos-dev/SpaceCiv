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
    public Transform originalParent;          // Assign original parent (like the main player container)

    private bool isPlayerInBoat = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.constraints = RigidbodyConstraints.FreezePositionX | 
                         RigidbodyConstraints.FreezePositionY | 
                         RigidbodyConstraints.FreezeRotationX | 
                         RigidbodyConstraints.FreezeRotationZ;

        Debug.Log("Boat initialized with Rigidbody");
    }

    void Update()
    {
        if (isPlayerInBoat)
        {
            HandleMovement();
            LockPlayerToSeat();
        }
    }

    public void EnterBoat()
    {
        isPlayerInBoat = true;
        Debug.Log("Player is entering the boat.");

        // Disable player controls
        if (playerMovement != null) playerMovement.enabled = false;
        if (characterController != null) characterController.enabled = false;

        // Parent player to boat seat
        player.transform.SetParent(boatSeat, worldPositionStays: false);
        player.transform.localPosition = new Vector3(0f, -1f, 0f); // Adjust Y to sit position
        player.transform.localRotation = Quaternion.identity;

        Debug.Log("Player seated on boat");
    }

    public void ExitBoat()
    {
        isPlayerInBoat = false;
        Debug.Log("Player is exiting the boat.");

        // Unparent player
        if (originalParent != null)
        {
            player.transform.SetParent(originalParent);
            Debug.Log("Player is now parented back to: " + originalParent.name);
        }
        else
        {
            Debug.LogWarning("Original parent is not assigned!");
        }

        // Place player beside boat
        player.transform.position = transform.position + transform.right * 2f;
        player.transform.rotation = Quaternion.identity;

        // Re-enable movement
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

    public bool IsPlayerInBoat()
    {
        return isPlayerInBoat;
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
