using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Pickup Settings (Rod)")]
    public GameObject itemToCheck;
    public float pickupRange = 0.91f;
    public KeyCode pickupKey = KeyCode.P;

    [Header("Equipped Settings (Boat)")]
    public GameObject itemToEquip;
    public float equipRange = 0.91f;
    public KeyCode equipKey = KeyCode.B;

    [Header("References")]
    public InventoryManager inventoryManager; // Drag your InventoryManager (player) here
    public Tips_Manager TipsManager;          // Drag your Tips_Manager here

    [Header("Runtime Status")]
    public bool status = false;
    public bool equipStatus = false;
    private bool hasPickedUp = false;
    private bool hasEquipped = false;

    void Update()
    {
        HandleRodPickup();
        HandleBoatEquip();
    }

    private void HandleRodPickup()
    {
        if (itemToCheck == null || hasPickedUp) return;

        float distance = Vector3.Distance(transform.position, itemToCheck.transform.position);
        status = distance <= pickupRange;

        // If the player is within range, set rodMessage01 to true in TipsManager
        if (status)
        {
            // Activate rodMessage01 in TipsManager to display the message
            if (TipsManager != null)
            {
                TipsManager.rodMessage01 = true; // Set the flag to show the message
                TipsManager.ShowTip();           // Call ShowTip() to display the message
            }

            Debug.Log("Item rod in range for pickup!");

            if (Input.GetKeyDown(pickupKey))
            {
                itemToCheck.SetActive(false);
                hasPickedUp = true;
                Debug.Log("Rod picked up!");

                if (inventoryManager != null)
                {
                    inventoryManager.CollectRod();
                }
                else
                {
                    Debug.LogWarning("No InventoryManager assigned!");
                }

                // Optionally, hide the message box after picking up the rod
                if (TipsManager != null)
                {
                    TipsManager.messageBox.SetActive(false);
                }
            }
        }
        else
        {
			// Only hide if the message was the rod tip
			if (TipsManager.rodMessage01 && TipsManager.messageText.text == "Message 01: Rod picked up!")
			{
				TipsManager.HideTip();
			}
        }
    }

    private void HandleBoatEquip()
    {
        if (itemToEquip == null) return;

        float distance = Vector3.Distance(transform.position, itemToEquip.transform.position);
        equipStatus = distance <= equipRange;

        // If the player is within range, show the boat message
        if (equipStatus && !TipsManager.boatMessage) // Check if the message is already shown
        {
            if (TipsManager != null)
            {
                TipsManager.boatMessage = true; // Set the flag to show the message
                TipsManager.ShowTip();          // Call ShowTip() to display the message
            }
		Debug.Log("Item boat in range for equip!");
        }

        // Handle the boat equip action
        if (equipStatus && Input.GetKeyDown(equipKey))
        {
            Boat_Movement boatMovement = itemToEquip.GetComponent<Boat_Movement>();
            if (boatMovement == null)
            {
                Debug.LogWarning("No Boat_Movement component found on boat!");
                return;
            }

            // Flag in inventory only once
            if (!hasEquipped)
            {
                hasEquipped = true;

                if (inventoryManager != null)
                {
                    inventoryManager.CollectBoat();
                }
                else
                {
                    Debug.LogWarning("No InventoryManager assigned!");
                }
            }

            // Toggle enter/exit
            if (!boatMovement.IsPlayerInBoat())
            {
                boatMovement.EnterBoat();
            }
            else
            {
                boatMovement.ExitBoat();
            }
        }
    }
}
