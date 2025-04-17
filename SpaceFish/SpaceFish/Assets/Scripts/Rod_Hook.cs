using UnityEngine;

public class Rod_Hook : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.1f;

    private Vector3 startPos;
    private bool isBobbing = false;

    public void StartBobbing()
    {
        startPos = transform.position;
        isBobbing = true;
    }

    public void StopBobbing()
    {
        isBobbing = false;
    }

    void Update()
    {
        if (!isBobbing) return;

        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }
}
