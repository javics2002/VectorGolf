using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreen : MonoBehaviour
{
	public int NextLevelScene { get; set; } = -1;
	public SpriteAtlas Atlas;

	private static int MainMenuScene { get; set; } = -1;
	private const string StarEmpty = "star-empty";
	private const string StarFull = "star-full";

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

		var background = new StyleBackground(Atlas.GetSprite(StarFull));

		var root = GetComponent<UIDocument>().rootVisualElement;
		var stars = root.Query<GroupBox>(name: "stars").Children<VisualElement>().ToList().Take(starCount);
		foreach (var star in stars)
		{
			star.style.backgroundImage = background;
		}

		var buttons = root.Query<GroupBox>().Children<Button>().ToList();
		if (NextLevelScene == -1)
		{
			buttons[0].SetEnabled(false);
		}
		else
		{
			buttons[0].clicked += OnClickedNextLevel;
		}

		buttons[1].clicked += OnClickedMainMenu;
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