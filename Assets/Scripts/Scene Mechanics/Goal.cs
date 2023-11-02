using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
    private int Level { get; set; } = -1;

    private void Awake()
    {
        // Get current scene name and verify it's formatted correctly with the format 'LevelN', where N is an integer:
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Length > 5 && sceneName.StartsWith("Level") && int.TryParse(sceneName[5..], out var level))
        {
            Level = level;
        }
        else
        {
            Debug.LogError("Current scene name is not formatted correctly, it must be named with the format 'LevelN', where N is an integer!'");
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Level == -1 || !collision.CompareTag("Player")) return;

        var index = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/Level{Level + 1}.unity");
        
        // If the next level doesn't exist, go back to the main menu:
        if (index == -1) SceneManager.LoadScene("Scenes/Main Menu");
        else SceneManager.LoadScene(index);
    }
}
