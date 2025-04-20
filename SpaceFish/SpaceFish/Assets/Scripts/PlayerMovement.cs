using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;
        Quaternion rotation = Quaternion.Euler(0, 45, 0);
        Vector3 moveDir = rotation * inputDir;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);
    }
}
