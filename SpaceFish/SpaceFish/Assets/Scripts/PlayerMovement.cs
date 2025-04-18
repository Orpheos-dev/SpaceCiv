using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
	
		// Fix input for camera with Y=45 rotation
		Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;
	
		// Rotate input direction by 45 degrees to match isometric camera
		Quaternion rotation = Quaternion.Euler(0, 45, 0);
		Vector3 moveDir = rotation * inputDir;
	
		controller.Move(moveDir * moveSpeed * Time.deltaTime);
	}
	
}
