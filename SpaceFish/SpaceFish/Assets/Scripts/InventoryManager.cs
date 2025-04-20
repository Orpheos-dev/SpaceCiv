using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Status")]
    public bool rodEquipped = false;

    [Header("UI Sprites")]
    public Sprite rodIconSprite; // Assign this in the Inspector
	
	[Header("Inventory Collected Items")]
	public bool haveRod = false;
	
	[Header("UI References")]
    public GameObject buttonToShowOnRodPickup; // Drag the UI button in Inspecto
	
	public void CollectRod()
	{
		haveRod = true;
		Debug.Log("✅ Rod collected and added to inventory!");
		
		// Show the button when rod is picked up
        if (buttonToShowOnRodPickup != null)
        {
            buttonToShowOnRodPickup.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No UI button assigned for rod pickup.");
        }
	}
	

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        // Press R to equip/unequip the rod
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!rodEquipped)
            {
                EquipRod();
            }
            else
            {
                UnequipRod();
            }
        }
    }

    public void EquipRod()
    {
        rodEquipped = true;
        UIManager.Instance.UpdateEquippedItem(rodIconSprite);
        Debug.Log("🎣 Rod equipped!");
    }

    public void UnequipRod()
    {
        rodEquipped = false;
        UIManager.Instance.UpdateEquippedItem(null);
        Debug.Log("❌ Rod unequipped!");
    }

    public bool IsRodEquipped()
    {
        return rodEquipped;
    }
}
