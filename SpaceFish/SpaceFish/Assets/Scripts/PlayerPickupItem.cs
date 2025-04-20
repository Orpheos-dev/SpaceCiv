using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Pickup Settings")]
    public GameObject itemToCheck;
    public float pickupRange = 0.91f;
    public KeyCode pickupKey = KeyCode.P;

    [Header("References")]
    public InventoryManager inventoryManager; // Drag your InventoryManager (player) here

    [Header("Runtime Status")]
    public bool status = false;
    private bool hasPickedUp = false;

    void Update()
    {
        if (itemToCheck == null || hasPickedUp) return;

        float distance = Vector3.Distance(transform.position, itemToCheck.transform.position);
        status = distance <= pickupRange;

        if (status)
        {
            Debug.Log("Item in range for pickup!");

            if (Input.GetKeyDown(pickupKey))
            {
                itemToCheck.SetActive(false);
                hasPickedUp = true;
                Debug.Log("Item picked up!");

                // Let inventory know we collected it
                if (inventoryManager != null)
                {
                    inventoryManager.CollectRod();
                }
                else
                {
                    Debug.LogWarning("No InventoryManager assigned!");
                }
            }
        }
    }
}
