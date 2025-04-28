using UnityEngine;
using UnityEngine.UI; // 👈 Add this for Button

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Status")]
    public bool rodEquipped = false;

    [Header("UI Sprites")]
    public Sprite rodIconSprite; // Assign this in the Inspector

    [Header("Inventory Collected Items")]
    public bool haveRod = false;
    public bool haveBoat = false;

    [Header("UI References")]
    public GameObject buttonToShowOnRodPickup; // Drag the UI button in Inspector
    public Button equipRodButton; // 👈 New: Button reference for equipping rod

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

    private void Start()
    {
        if (equipRodButton != null)
        {
            equipRodButton.onClick.AddListener(ToggleRodEquip);
        }
        else
        {
            Debug.LogWarning("Equip Rod Button is not assigned!");
        }
    }

    private void Update()
    {
        // OPTIONAL: You can REMOVE the R key detection if you want full button control
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRodEquip();
        }
    }

    private void ToggleRodEquip()
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

    public void CollectBoat()
    {
        haveBoat = true;
        Debug.Log("✅ Boat collected and added to inventory!");
    }

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
}
