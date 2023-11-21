using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreen : MonoBehaviour
{
	public int NextLevelScene { get; set; } = -1;

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
		// TODO: Hook this to the GameManager
		const int starCount = 1;

		var root = GetComponent<UIDocument>().rootVisualElement;
		var stars = root.Query<GroupBox>(name: "stars").Children<VisualElement>().ToList().Take(starCount);
		foreach (var star in stars)
		{
			star.AddToClassList("full");
		}

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
		if (NextLevelScene != -1) goToNextLevel.clicked += OnClickedNextLevel;
	}

	private void OnClickedRetry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnClickedNextLevel()
	{
		SceneManager.LoadScene(NextLevelScene);
	}

	private void OnClickedMainMenu()
	{
		SceneManager.LoadScene(MainMenuScene);
	}
}