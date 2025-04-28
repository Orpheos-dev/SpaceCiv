using UnityEngine;
using TMPro;

public class SimpleHotkeyDisplay : MonoBehaviour
{
    [Header("References")]
    public PickupItem player;
    public Transform rodObject;
    public Transform boatObject;

    public TextMeshPro rodText;
    public TextMeshPro boatText;

    void Start()
    {
        if (rodText) rodText.gameObject.SetActive(false);
        if (boatText) boatText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!player) return;

        // Rod
        if (player.status && rodText != null)
        {
            rodText.gameObject.SetActive(true);
            rodText.text = $"{player.pickupKey}";
        }
        else if (rodText != null)
        {
            rodText.gameObject.SetActive(false);
        }

        // Boat
        if (player.equipStatus && boatText != null)
        {
            boatText.gameObject.SetActive(true);
            boatText.text = $"{player.equipKey}";
        }
        else if (boatText != null)
        {
            boatText.gameObject.SetActive(false);
        }
    }
}
