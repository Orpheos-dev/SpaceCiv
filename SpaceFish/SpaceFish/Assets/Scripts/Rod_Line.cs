using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rod_line : MonoBehaviour
{
    [Header("References")]
    public Transform rodTip; // Tip of the rod (world position)
    public Transform hook;   // The hook object (can be moving)

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Set up basic line properties (optional)
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.useWorldSpace = true; // Important when objects are moving in world
    }

    void LateUpdate()
    {
        if (!rodTip || !hook)
            return;

        // Update the line positions every frame
        lineRenderer.SetPosition(0, rodTip.position); // Start at rod tip
        lineRenderer.SetPosition(1, hook.position);   // End at hook
    }
}
