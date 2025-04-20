using UnityEngine;
using TMPro;

public class Tips_Manager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject messageBox;          // Drag your UI panel or message box here
    public TextMeshProUGUI messageText;    // Or use TMPro.TextMeshProUGUI if using TextMeshPro

    [Header("Tip Content")]
    [TextArea(2, 5)]
    public string defaultTipMessage = "Default tip message";

    // Add these flags for conditional messages
    public bool rodMessage01 = false;     // Set this flag true when rod is picked up
    public bool boatMessage = false;      // Set this flag true when boat is equipped

    // Flags to prevent repeated messages
    public bool rodMessageShown = false;
    public bool boatMessageShown = false;

    [Header("References")]
    public GameObject player;              // Assign your player here

    private void Start()
    {
        if (messageBox != null)
            messageBox.SetActive(false);   // Start hidden
    }

    public void ShowTip()
    {
        if (messageBox != null && messageText != null)
        {
            // Check for rodMessage01 flag and prevent repeated display
            if (rodMessage01 && !rodMessageShown)
            {
                messageText.text = "Message 01: Rod picked up!";
                messageBox.SetActive(true);
                rodMessageShown = true;  // Mark message as shown
            }
            // Check for boatMessage flag and prevent repeated display
            else if (boatMessage && !boatMessageShown)
            {
                messageText.text = "Message 02: Boat equipped!";
                messageBox.SetActive(true);
                boatMessageShown = true;  // Mark message as shown
            }
            // Default message if no specific flag is set
            else if (!rodMessage01 && !boatMessage)
            {
                messageText.text = defaultTipMessage;
                messageBox.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("Missing messageBox or messageText reference.");
        }
    }

    public void HideTip()
    {
        if (messageBox != null)
            messageBox.SetActive(false);
    }

    // Reset flags when needed, for example when a new interaction occurs
    public void ResetMessages()
    {
        rodMessageShown = false;
        boatMessageShown = false;
    }
}
