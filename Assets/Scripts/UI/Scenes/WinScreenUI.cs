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

		root.Q<Button>("button-retry").clicked += OnClickedRetry;
		root.Q<Button>("button-menu").clicked += OnClickedMainMenu;

		var buttonNext = root.Q<Button>("button-next");
		if (GameManager.Instance.Level.HasNext()) buttonNext.clicked += OnClickedNextLevel;
		else buttonNext.SetEnabled(false);
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