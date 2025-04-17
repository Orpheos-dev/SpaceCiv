using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rod_line : MonoBehaviour
{
    public Transform rodTip; // Empty GameObject at the tip of your rod
    public Transform hook;   // Your hook object in the scene

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Start and end of the line
    }

    void Update()
    {
        if (rodTip == null || hook == null) return;

        // Update line positions every frame
        lineRenderer.SetPosition(0, rodTip.position); // Start at rod tip
        lineRenderer.SetPosition(1, hook.position);   // End at hook
    }
}
