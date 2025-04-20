using UnityEngine;
using UnityEngine.UI; // Required for Button reference

public class ToggleCanvasAndDisableButton : MonoBehaviour
{
    // References to be assigned in the Unity Inspector
    public Canvas canvasToToggle;

    // This function will be called when the button is clicked
    public void OnButtonClick(Button button)
    {
        // Toggle the Canvas visibility
        if (canvasToToggle != null)
        {
            canvasToToggle.gameObject.SetActive(!canvasToToggle.gameObject.activeSelf);
        }

        // Hide the button that is being clicked
        if (button != null)
        {
            button.gameObject.SetActive(false); // This will hide the button
        }
    }

    // This function will be called to make the button visible again
    public void MakeButtonVisible(Button button)
    {
        // Make the button visible again
        if (button != null)
        {
            button.gameObject.SetActive(true); // This will show the button again
        }
    }
}
