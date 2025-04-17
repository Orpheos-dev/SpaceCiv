using UnityEngine;

public class Rod_Hook : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}