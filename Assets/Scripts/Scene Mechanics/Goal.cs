using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
    private int NextScene { get; set; } = -1;
    private static int MainMenuScene { get; set; } = -1;

    private void Awake()
    {
        // Get the build index for the main menu scene if it hasn't been loaded yet:
        if (MainMenuScene == -1)
        {
            MainMenuScene = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Main Menu.unity");
        }

        // Get current scene name and verify it's formatted correctly with the format 'LevelN', where N is an integer:
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Length > 5 && sceneName.StartsWith("Level") && int.TryParse(sceneName[5..], out var level))
        {
            NextScene = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/Level{level + 1}.unity") switch
            {
                -1 => MainMenuScene,
                var v => v
            };
        }
        else
        {
            Debug.LogError("Current scene name is not formatted correctly, it must be named with the format 'LevelN', where N is an integer!'");
            enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // If the next level doesn't exist, go back to the main menu:
        SceneManager.LoadScene(NextScene);
    }
}
