using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
    private Scene? NextScene { get; set; }

    private void Awake()
    {
        // Get current scene name and verify it's formatted correctly with the format 'LevelN', where N is an integer:
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Length <= 5 || !sceneName.StartsWith("Level") || !int.TryParse(sceneName[5..], out var level))
        {
            Debug.LogError("Current scene name is not formatted correctly, it must be named with the format 'LevelN', where N is an integer!'");
            enabled = false;
            return;
        }

        var nextSceneName = $"Level{level + 1}";
        var scene = SceneManager.GetSceneByName(nextSceneName);
        if (scene.IsValid())
        {
            NextScene = scene;
        }
        else
        {
            Debug.Log($"Could not find scene {nextSceneName}, using main menu as next scene.");
            NextScene = SceneManager.GetSceneByName("Main Menu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal reached!");
        if (collision.CompareTag("Player") && NextScene.HasValue)
        {
            SceneManager.LoadScene(NextScene.Value.name);
        }
    }
}
