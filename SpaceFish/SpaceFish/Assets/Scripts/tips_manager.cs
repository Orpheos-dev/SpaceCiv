using UnityEngine;
using TMPro;
using System.Collections;

public class Tips_Manager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject messageBox;
    public TextMeshProUGUI messageText;

    [Header("Tip Content")]
    [TextArea(2, 5)]
    public string defaultTipMessage = "Default tip message";
    [TextArea(2, 5)]
    public string movingTipMessage = "Use joystick to move around."; // 👈 new message

    [Header("Flags")]
    public bool rodMessage01 = false;
    public bool boatMessage = false;
    public bool rodMessageShown = false;
    public bool boatMessageShown = false;
    private bool movingMessageShown = false; // 👈 to prevent repeating

    [Header("References")]
    public GameObject player;

    private void Start()
    {
        if (messageBox != null)
            messageBox.SetActive(false);

        // Start tip coroutine
        StartCoroutine(ShowMovingTipAfterDelay(3f)); // 👈 run 3 sec after game start
    }

	IEnumerator ShowMovingTipAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
	
		if (!movingMessageShown && !rodMessage01 && !boatMessage)
		{
			messageText.text = movingTipMessage;
			messageBox.SetActive(true);
			movingMessageShown = true;
		}
	}
	

    public void ShowTip()
    {
        if (messageBox != null && messageText != null)
        {
            if (rodMessage01 && !rodMessageShown)
            {
                messageText.text = "You can Pickup items pressing P !";
                messageBox.SetActive(true);
                rodMessageShown = true;
            }
            else if (boatMessage && !boatMessageShown)
            {
                messageText.text = "You can equip the boat by pressing P !";
                messageBox.SetActive(true);
                boatMessageShown = true;
            }
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
	
	public bool IsCurrentTip(string tipText)
	{
		return messageText != null && messageText.text == tipText;
	}
	

    public void HideTip()
    {
        if (messageBox != null)
            messageBox.SetActive(false);
    }

    public void ResetMessages()
    {
        rodMessageShown = false;
        boatMessageShown = false;
        movingMessageShown = false;
    }
	
	public void OnCloseButtonPressed()
	{
		HideTip();
	}
	
}
