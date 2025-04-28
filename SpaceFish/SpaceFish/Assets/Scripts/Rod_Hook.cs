using UnityEngine;
using System.Collections;

public class Rod_Hook : MonoBehaviour
{
    public float bobSpeed = 2f;
    public float bobHeight = 0.1f;

    private Vector3 startPos;
    private bool isBobbing = false;
    private Coroutine fishRoutine;

    public FishManager fishManager;
    public MinigameManager minigameManager;
    public RodCasting rodCasting; // ⬅️ ADD THIS (Reference to RodCasting!)

    public void StartBobbing()
    {
        startPos = transform.position;
        isBobbing = true;

        UIManager.Instance?.ShowFishingIndicator(true);

        if (fishRoutine == null)
        {
            fishRoutine = StartCoroutine(BobbingFishRoutine());
        }
    }

    public void StopBobbing()
    {
        isBobbing = false;

        UIManager.Instance?.ShowFishingIndicator(false);

        if (fishRoutine != null)
        {
            StopCoroutine(fishRoutine);
            fishRoutine = null;
        }
    }

    void Update()
    {
        if (!isBobbing) return;

        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobHeight;
    }

    IEnumerator BobbingFishRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (fishManager != null)
            {
                bool success = fishManager.TrySpawnFish(transform.position);

                if (success)
                {
                    Debug.Log("🎣 A fish was successfully 'hooked', starting minigame.");
                    StopBobbing();
                    minigameManager.StartMinigame(OnFishCaught, OnFishFailed);
                    yield break;
                }
            }
        }
    }

    void OnFishCaught()
    {
        Debug.Log("🐟 You caught the fish with skill!");

        if (fishManager != null)
            fishManager.ConfirmSpawn();

        ReturnHookProperly();
    }
	
    void OnFishFailed()
    {
        Debug.Log("🐟 The fish got away...");
        ReturnHookProperly();
    }
	
    void ReturnHookProperly()
    {
        if (rodCasting != null)
        {
            rodCasting.TriggerReturn(); // ⬅️ Call RodCasting to do the proper smooth return!
        }
        else
        {
            Debug.LogWarning("RodCasting reference missing, hook instantly reset.");
            transform.position = startPos; // Fallback
        }
    }
}
