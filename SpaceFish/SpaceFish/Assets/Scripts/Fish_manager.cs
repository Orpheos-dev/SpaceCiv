using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;
    [Range(0f, 1f)] public float fishAppearChance = 0.5f;
    [Range(0f, 1f)] public float spawnChance = 0.4f;

    private Vector3 pendingSpawnPosition;
    private bool hasPendingFish = false;

    public bool TrySpawnFish(Vector3 position)
    {
        float chance = Random.value;
        if (chance < spawnChance)
        {
            pendingSpawnPosition = position;
            hasPendingFish = true;

            // ⬇️ Don't show the fish yet
            // GameObject fish = Instantiate(fishPrefab, position, Quaternion.identity);
            // fish.SetActive(true);
            // Destroy(fish, 3f);

            return true;
        }

        return false;
    }

    public void ConfirmSpawn()
    {
        if (hasPendingFish && fishPrefab != null)
        {
            GameObject fish = Instantiate(fishPrefab, pendingSpawnPosition, Quaternion.identity);
            fish.SetActive(true);
            Destroy(fish, 3f);
        }

        hasPendingFish = false;
    }
}
