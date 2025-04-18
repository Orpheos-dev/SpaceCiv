using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Status")]
    public bool rodEquipped = false;

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
        Debug.Log("🎣 Rod equipped!");
    }

    public void UnequipRod()
    {
        rodEquipped = false;
        Debug.Log("❌ Rod unequipped!");
    }

    public bool IsRodEquipped()
    {
        return rodEquipped;
    }
}
