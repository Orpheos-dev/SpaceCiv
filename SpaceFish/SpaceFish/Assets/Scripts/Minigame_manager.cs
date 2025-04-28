using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameManager : MonoBehaviour
{
    public GameObject minigameUI;            // Parent canvas or panel
    public RectTransform rotatingBall;       // The ball that rotates
    public RectTransform hitZone;                    // UI Image representing hit area

    public float rotateSpeed = 180f;         // Degrees per second
    public int requiredHits = 3;
    public float hitAngleTolerance = 15f;    // Degrees from center of hit zone

    private bool isPlaying = false;
    private int currentHits = 0;
	private Action onWinCallback;
	private Action onFailCallback; 
	
	private float currentAngle = 0f;
	public float ballRadius = 100f; // Distance from center (in UI units)
	
	public KeyCode hitKey = KeyCode.Space; // 👈 Show in Inspector
	
	private bool ballInsideZone = false;
	
	public void SetBallInZone(bool inZone)
	{
		ballInsideZone = inZone;
	}
	

	private bool IsBallInHitZone()
	{
		return ballInsideZone;
	}
	
	
	void Update()
	{
		if (!isPlaying) return;
	
		// Update angle
		currentAngle += rotateSpeed * Time.deltaTime;
		if (currentAngle >= 360f) currentAngle -= 360f;
	
		// Calculate new position using local space
		Vector2 offset = new Vector2(
			Mathf.Cos(currentAngle * Mathf.Deg2Rad),
			Mathf.Sin(currentAngle * Mathf.Deg2Rad)
		) * ballRadius;
	
		rotatingBall.localPosition = offset; // ⬅️ Moves around circle center
	
		// Space key input
		if (Input.GetKeyDown(hitKey))
		{
			if (IsBallInHitZone())
			{
				currentHits++;
				Debug.Log("✅ Hit! Total: " + currentHits);
			}
			else
			{
				Debug.Log("❌ Missed!");
			}
		
			if (currentHits >= requiredHits)
			{
				WinMinigame();
			}
			else if (!IsBallInHitZone()) // Optional: fail on miss
			{
				FailMinigame(); // 👈 Add this
			}
		}
		
		
	}
	
	

	public void StartMinigame(Action onWin, Action onFail = null)
	{
		onWinCallback = onWin;
		onFailCallback = onFail;
		currentHits = 0;
		isPlaying = true;
		minigameUI.SetActive(true);
	}
	

    public void StopMinigame()
    {
        isPlaying = false;
        minigameUI.SetActive(false);
    }

    private void WinMinigame()
    {
        StopMinigame();
        Debug.Log("🎉 Minigame WON!");
        onWinCallback?.Invoke();
    }
	
	private void FailMinigame()
	{
		StopMinigame();
		Debug.Log("💔 Minigame FAILED.");
		onFailCallback?.Invoke();
	}
	

    private float GetAngle(RectTransform target)
    {
        Vector2 dir = target.position - target.parent.position;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}
