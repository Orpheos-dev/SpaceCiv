using UnityEngine;

public class Rod_SetPlayer : MonoBehaviour
{
    [Header("References")]
    public GameObject player;         // Assign the player GameObject
    public GameObject rodItem;        // Assign the rod GameObject
    public Transform attachPoint;     // Assign a child transform on the player (e.g., RodHolder)

    public void SnapRodToPlayer()
    {
        if (player == null || rodItem == null || attachPoint == null)
        {
            Debug.LogWarning("Missing references for snapping rod.");
            return;
        }

        // Make sure the rod is visible
        rodItem.SetActive(true);

        // Snap it to the attach point
        rodItem.transform.SetParent(attachPoint);
        rodItem.transform.localPosition = Vector3.zero;
        rodItem.transform.localRotation = Quaternion.identity;

        Debug.Log("🎯 Rod snapped and made visible on player.");
    }
}
