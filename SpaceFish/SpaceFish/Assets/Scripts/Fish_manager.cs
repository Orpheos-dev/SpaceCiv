using UnityEngine;

public class FishManager : MonoBehaviour
{
    public GameObject fishPrefab;  // assign in Inspector
    [Range(0f, 1f)] public float fishAppearChance = 0.5f;

    [Range(0f, 1f)]
    public float spawnChance = 0.4f;

    public bool TrySpawnFish(Vector3 position)
    {
        float chance = Random.value;
        if (chance < spawnChance)
        {
            if (fishPrefab != null)
            {
                // Instantiate the fish and make sure it's active
                GameObject fish = Instantiate(fishPrefab, position, Quaternion.identity);
                fish.SetActive(true); // Make sure fish is visible and active
                Destroy(fish, 3f); // Destroy after 3 seconds
                return true;
            }
        }

        return false;
    }

    private void SpawnFishAt(Vector3 position)
    {
        GameObject fish = Instantiate(fishPrefab, position, Quaternion.identity);
        fish.SetActive(true); // In case prefab is initially disabled
        Destroy(fish, 3f);    // Destroy after 3 seconds
    }
}
