using UnityEngine;

public class MinigameBall : MonoBehaviour
{
    public MinigameManager manager;           // Assign in Inspector
    public Collider2D hitZoneCollider;        // Assign the hitZone Collider here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == hitZoneCollider)
        {
            Debug.Log("🎯 Ball entered hit zone!");
            manager.SetBallInZone(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == hitZoneCollider)
        {
            Debug.Log("🏃‍♂️ Ball exited hit zone!");
            manager.SetBallInZone(false);
        }
    }
}
