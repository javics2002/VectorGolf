using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreen : MonoBehaviour
{
	public int Stars { get; set; } = 1;
	public int Hits { get; set; } = 0;

	private static int MainMenuScene { get; set; } = -1;

	private void Awake()
	{
		// Get the build index for the main menu scene if it hasn't been loaded yet:
		if (MainMenuScene == -1)
		{
			MainMenuScene = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Main Menu.unity");
		}
	}

	// Start is called before the first frame update
	private void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;
		if (root is null)
		{
			throw new NullReferenceException(nameof(root));
		}

		var stars = root.Query<GroupBox>(name: "stars").Children<VisualElement>().ToList().Take(Stars);
		foreach (var star in stars)
		{
			star.AddToClassList("full");
		}

		var retries = root.Query<Label>(name: "retries").First();
		retries.text = Hits switch
		{
			1 => "¡Te tomó 1 golpe!",
			_ => $"¡Te tomó {Hits} golpes!"
		};

		// buttons is a list with three elements:
		// 0: Retry
		// 1: Next Level
		// 2: Main Menu
		var buttons = root.Query<GroupBox>().Children<Button>().ToList();
		RegisterUIMenuCallbacks(buttons[0], buttons[1], buttons[2]);
	}

	private void RegisterUIMenuCallbacks(Button retry, Button goToNextLevel, Button goToMainMenu)
	{
		retry.clicked += OnClickedRetry;
		goToMainMenu.clicked += OnClickedMainMenu;

		if (GameManager.Instance.Level.HasNext()) goToNextLevel.clicked += OnClickedNextLevel;
		else goToNextLevel.SetEnabled(false);
	}

	private void OnClickedRetry()
	{
		// TODO: Move this elsewhere
		// GameManager.Instance.LoadManager.IncreaseRetries(GameManager.Instance.LevelIndex);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnClickedNextLevel()
	{
		SceneManager.LoadScene($"Level{GameManager.Instance.Level.Current + 1}");
	}

	private void OnClickedMainMenu()
	{
		SceneManager.LoadScene(MainMenuScene);
	}
}