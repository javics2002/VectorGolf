using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
	[SerializeField]
	private SpriteAtlas Atlas;

	/// <summary>
	/// The level this goal is for, calculated from the scene name.
	/// </summary>
	private int Level { get; set; } = -1;

	private int NextScene { get; set; } = -1;
	private int LevelIndex => Level == -1 ? -1 : Level - 1;
	private static int MainMenuScene { get; set; } = -1;

	private const string AtlasFlagDefault = "game-flag-default";
	private const string AtlasFlagRed = "game-flag-red";
	private const string AtlasFlagGold = "game-flag-gold";
	private const string AtlasFlagPlatinum = "game-flag-platinum";

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
			Level = level;
			NextScene = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/Level{level + 1}.unity") switch
			{
				-1 => MainMenuScene,
				var v => v
			};
		}
		else
		{
			Debug.LogError(
				"Current scene name is not formatted correctly, it must be named with the format 'LevelN', where N is an integer!'");
			enabled = false;
		}
	}

	private void Start()
	{
		// Set the sprite to the correct flag sprite from the atlas:
		GetComponent<SpriteRenderer>().sprite = Atlas.GetSprite(GetGoalSpriteName());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;

		// Save the level completion:
		// TODO: Determine the correct level completion
		GameManager.Instance.LoadManager.Save(LevelIndex, GameManager.LevelCompletion.Completed);
		
		// If the next level doesn't exist, go back to the main menu:
		SceneManager.LoadScene(NextScene);
	}

	private string GetGoalSpriteName()
	{
		// If the current level is 0 or greater than the number of levels, return the default flag sprite:
		if (LevelIndex < 0 || LevelIndex >= GameManager.Instance.levelCompletion.Length) return AtlasFlagDefault;

		return GameManager.Instance.levelCompletion[LevelIndex] switch
		{
			GameManager.LevelCompletion.HoleInOne => AtlasFlagPlatinum,
			GameManager.LevelCompletion.Par => AtlasFlagGold,
			GameManager.LevelCompletion.Completed => AtlasFlagRed,
			_ => AtlasFlagDefault
		};
	}
}