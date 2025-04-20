using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Equipped Item Display")]
    public Image equippedItemImage;
    public Sprite defaultSprite;

    [Header("Camera Reference")]
    public Camera mainCamera;

    [Header("Fishing UI")]
    public RectTransform fishingIndicatorUI; // 👈 assign the indicator UI here (in canvas)
	
	[Header("Casting UI")]
	public GameObject castingUI; // Assign in inspector

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        UpdateEquippedItem(null);
        ShowFishingIndicator(false);
    }

    public void UpdateEquippedItem(Sprite itemSprite)
    {
        if (equippedItemImage == null) return;

        if (itemSprite != null)
        {
            equippedItemImage.sprite = itemSprite;
            equippedItemImage.enabled = true;
        }
        else
        {
            equippedItemImage.sprite = defaultSprite;
            equippedItemImage.enabled = defaultSprite != null;
        }
    }

    public void ShowFishingIndicator(bool show)
    {
        if (fishingIndicatorUI != null)
        {
            fishingIndicatorUI.gameObject.SetActive(show);
        }
    }

    public void UpdateFishingIndicatorPosition(Vector3 worldPosition)
    {
        if (fishingIndicatorUI != null && mainCamera != null)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPosition + Vector3.up * 0.5f); // slight offset
            fishingIndicatorUI.position = screenPos;
        }
    }
	
	public void ShowCastingUI(bool show)
	{
		if (castingUI != null)
		{
			castingUI.SetActive(show);
		}
	}
}
