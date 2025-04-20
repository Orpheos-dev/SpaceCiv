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
	
    public GameObject splashEffect;
    public FishManager fishManager;

    void Start()
    {
        originalHookPosition = hook.localPosition;
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
                castDirection = Vector3.zero;
                UIManager.Instance.ShowCastingUI(true); // 🔥 Show casting UI
                Debug.Log("Casting mode activated");
            }
        }

        if (isCastingMode && !hasCast)
        {
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
                    targetLocalPosition.y = 0f;

                    Vector3 worldTarget = rod.position + castDirection.normalized * castDistance;
                    worldTarget.y = 0f;
                    previewCircle.position = worldTarget;

                    Debug.Log("Preview updated at local: " + targetLocalPosition);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && castDirection != Vector3.zero)
            {
                Vector3 worldTarget = rod.position + castDirection.normalized * castDistance;
                worldTarget.y = 0f;

                StartCoroutine(CastHook(worldTarget));
                isCastingMode = false;
                hasCast = true;
                previewCircle.gameObject.SetActive(false);
                UIManager.Instance.ShowCastingUI(false); // ❌ Hide casting UI
                Debug.Log("Casting toward: " + worldTarget);
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
}
