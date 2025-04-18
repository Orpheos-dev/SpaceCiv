using UnityEngine;
using System.Collections;

public class RodCasting : MonoBehaviour
{
    public Transform rod;
    public Transform hook;
    public Transform previewCircle;
    public float castDistance = 5f;
    public float castSpeed = 5f;

    private bool isCastingMode = false;
    private bool hasCast = false;
    private bool isReturning = false;
    private Vector3 castDirection = Vector3.zero;
    private Vector3 originalHookPosition;
	
	public GameObject splashEffect; // assign in Inspector


    void Start()
    {
        originalHookPosition = hook.localPosition;
    }

    void Update()
    {
        // First E = cast mode, Second E = return hook
        if (Input.GetKeyDown(KeyCode.E) && InventoryManager.Instance.IsRodEquipped())
        {
            if (hasCast && !isReturning)
            {
                // Return hook
                StartCoroutine(ReturnHook());
                Debug.Log("Returning hook to original position");
                return; // Don't re-enter casting mode
            }

            if (!hasCast && !isReturning)
            {
                // Activate casting mode
                isCastingMode = true;
                previewCircle.gameObject.SetActive(true);
                castDirection = Vector3.zero;
                Debug.Log("Casting mode activated");
            }
        }

        // While in casting mode
        if (isCastingMode && !hasCast)
        {
            // Choose direction
            if (castDirection == Vector3.zero)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow)) castDirection = Vector3.forward;
                if (Input.GetKeyDown(KeyCode.DownArrow)) castDirection = Vector3.back;
                if (Input.GetKeyDown(KeyCode.LeftArrow)) castDirection = Vector3.left;
                if (Input.GetKeyDown(KeyCode.RightArrow)) castDirection = Vector3.right;

                if (castDirection != Vector3.zero)
                {
                    Vector3 localDir = rod.InverseTransformDirection(castDirection.normalized);
                    Vector3 targetLocalPosition = localDir * castDistance;

                    // Force to water level
                    targetLocalPosition.y = 0f;

                    Vector3 worldTarget = rod.position + castDirection.normalized * castDistance;
					worldTarget.y = 0f; // Ensure it's at water level
					previewCircle.position = worldTarget;

                    Debug.Log("Preview updated at local: " + targetLocalPosition);
                }
            }

            // Space to cast
            if (Input.GetKeyDown(KeyCode.Space) && castDirection != Vector3.zero)
            {
                Vector3 localDir = rod.InverseTransformDirection(castDirection.normalized);
                Vector3 targetLocalPosition = localDir * castDistance;

                // Force to water level
                targetLocalPosition.y = 0f;

                Vector3 worldTarget = rod.position + castDirection.normalized * castDistance;
				worldTarget.y = 0f; // Force to water level

				StartCoroutine(CastHook(worldTarget));

                isCastingMode = false;
                hasCast = true;
                previewCircle.gameObject.SetActive(false);
                Debug.Log("Casting toward: " + targetLocalPosition);
            }
        }
    }

	IEnumerator CastHook(Vector3 target)
	{
		Vector3 start = hook.position;
		float t = 0f;
	
		while (t < 1f)
		{
			t += Time.deltaTime * castSpeed;
			hook.position = Vector3.Lerp(start, target, t);
			yield return null;
		}
	
		hook.position = target;
	
		// 💦 Splash effect with auto-destroy
		if (splashEffect != null)
		{
			GameObject splash = Instantiate(splashEffect, target, Quaternion.identity);
			Destroy(splash, 0.5f); // destroy after 2 seconds
		}
	
		hook.GetComponent<Rod_Hook>()?.StartBobbing();
		Debug.Log("Hook landed at: " + target);
	}
	

    IEnumerator ReturnHook()
    {
        isReturning = true;

        Vector3 start = hook.localPosition;
        hook.GetComponent<Rod_Hook>()?.StopBobbing();
        Vector3 target = originalHookPosition;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * castSpeed;
            hook.localPosition = Vector3.Lerp(start, target, t);
            yield return null;
        }

        hook.localPosition = target;
        hasCast = false;
        isReturning = false;
        Debug.Log("Hook returned to original position");
    }
}
