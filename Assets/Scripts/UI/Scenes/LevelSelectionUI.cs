using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class LevelSelectionUI : MonoBehaviour
{
	[Header("Level Images")]
	[SerializeField]
	private Sprite[] LevelImages = new Sprite[GameManager.NumberOfLevels];

	private void OnEnable()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		StartRows(root);
		StartProgress(root);
		root.Q<Button>("button-back").clicked += () => SceneManager.LoadScene("Main Menu");
	}

	private static void StartProgress(VisualElement root)
	{
		var gm = GameManager.Instance;
		var completed = gm.progress.Count(t => t.Status >= GameManager.LevelCompletionStatus.Completed);
		var percentage = completed / (float)gm.progress.Length * 100f;

		var progress = root.Q<ProgressBar>("progress-bar");
		progress.value = percentage;
		progress.title = $"{percentage:0}% completed";
	}

	private void StartRows(VisualElement root)
	{
		var rows = root.Q<GroupBox>("rows");
		for (var i = 0; i < GameManager.NumberOfLevels;)
		{
			rows.Add(CreateRow(ref i));
		}
	}

	private GroupBox CreateRow(ref int i)
	{
		var root = new GroupBox { name = $"row-{i / 4}" };
		root.AddToClassList("level-row");

		var gm = GameManager.Instance;
		for (var max = Math.Min(i + 4, GameManager.NumberOfLevels); i < max; i++)
		{
			root.Add(CreateButton(i + 1, gm.progress[i]));
		}

		return root;
	}

	private VisualElement CreateButton(int level, GameManager.LevelProgress progress)
	{
		var root = CreateButtonRoot(level, progress);

		var thumbnail = new VisualElement
			{ name = "thumbnail", style = { backgroundImage = LevelImages[level - 1].texture } };
		thumbnail.AddToClassList("button-icon");
		thumbnail.AddToClassList("level-thumbnail");
		root.Add(thumbnail);

		var flagFg = new VisualElement { name = "flag-fg" };
		flagFg.AddToClassList("level-flag-container");
		root.Add(flagFg);

		var image = new VisualElement { name = "image" };
		image.AddToClassList("level-flag-image");
		flagFg.Add(image);

		var number = new Label { name = "number", text = $"{level}" };
		number.AddToClassList("level-number");
		root.Add(number);

		return root;
	}

	private static Button CreateButtonRoot(int level, GameManager.LevelProgress progress)
	{
		var root = new Button { name = $"level-{level}" };
		root.AddToClassList("level-button");
		root.AddToClassList("animate-scale");

		switch (progress.Status)
		{
			case GameManager.LevelCompletionStatus.Locked:
				root.SetEnabled(false);
				return root;
			case GameManager.LevelCompletionStatus.Uncompleted:
				break;
			case GameManager.LevelCompletionStatus.Completed:
				root.AddToClassList("red");
				break;
			case GameManager.LevelCompletionStatus.Par:
				root.AddToClassList("gold");
				break;
			case GameManager.LevelCompletionStatus.HoleInOne:
				root.AddToClassList("platinum");
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

		root.clicked += OnLevelSelected(level);
		return root;
	}

	private static Action OnLevelSelected(int level) => () => SceneManager.LoadScene($"Level{level}");
}