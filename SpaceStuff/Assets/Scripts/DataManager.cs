using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; // Singleton instance

    public int playerScore = 0; // Example of persistent data
    public string lastSceneName; // Track the last scene loaded

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate managers
        }
    }

    //public void LoadScene(string sceneName)
    //{
    //    lastSceneName = SceneManager.GetActiveScene().name; // Track the current scene
    //    SceneManager.LoadScene(sceneName);
    //}
}
