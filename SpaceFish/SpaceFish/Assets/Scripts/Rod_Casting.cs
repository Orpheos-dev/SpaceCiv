using UnityEngine;
using System.Collections;

public class RodCasting : MonoBehaviour
{
    [Header("Casting References")]
    public Transform rod;
    public Transform hook;
    public Transform previewCircle;
    public float castSpeed = 5f;
    public float previewFollowSpeed = 10f; // 🌟 New: how fast the preview moves

    [Header("Casting Settings")]
    public LayerMask waterLayer; // 🌊 Set to Water layer
    public float maxCastDistance = 5f;

    [Header("Effects")]
    public GameObject splashEffect;
    public FishManager fishManager;

    private bool isCastingMode = false;
    private bool hasCast = false;
    private bool isReturning = false;
    private Vector3 originalHookPosition;
    private Vector3 targetCastPoint;
    private Camera cam;

    void Start()
    {
        originalHookPosition = hook.localPosition;
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InventoryManager.Instance.IsRodEquipped())
        {
            if (hasCast && !isReturning)
            {
                StartCoroutine(ReturnHook());
                Debug.Log("Returning hook to original position");
                return;
            }

            if (!hasCast && !isReturning)
            {
                isCastingMode = true;
                previewCircle.gameObject.SetActive(true);
                UIManager.Instance.ShowCastingUI(true);
                Debug.Log("Casting mode activated");
            }
        }

        if (isCastingMode && !hasCast)
        {
            UpdatePreviewCircle();

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartCoroutine(CastHook(targetCastPoint));
                isCastingMode = false;
                hasCast = true;
                previewCircle.gameObject.SetActive(false);
                UIManager.Instance.ShowCastingUI(false);
                Debug.Log("Casting toward: " + targetCastPoint);
            }
        }
    }

    void UpdatePreviewCircle()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, waterLayer))
        {
            Vector3 hitPoint = hitInfo.point;

            // Clamp the distance
            Vector3 dir = (hitPoint - rod.position);
            dir.y = 0f;
            if (dir.magnitude > maxCastDistance)
                dir = dir.normalized * maxCastDistance;

            targetCastPoint = rod.position + dir;
            targetCastPoint.y = hitPoint.y; // Keep water height

            // 🧡 Smoothly move preview circle toward the target
            previewCircle.position = Vector3.Lerp(previewCircle.position, targetCastPoint, Time.deltaTime * previewFollowSpeed);
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

        if (splashEffect != null)
        {
            GameObject splash = Instantiate(splashEffect, target, Quaternion.identity);
            Destroy(splash, 0.5f);
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
	
	public void TriggerReturn()
	{
		if (!isReturning && hasCast)
		{
			StartCoroutine(ReturnHook());
			Debug.Log("Triggered ReturnHook from Rod_Hook.");
		}
	}
	
}
