using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Elements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class WinScreen : MonoBehaviour
{
	public int Stars { get; set; } = 1;
	public int Hits { get; set; } = 0;

	// Start is called before the first frame update
	private void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;
		if (root is null)
		{
			throw new NullReferenceException(nameof(root));
		}

		var stars = root.Query<GroupBox>(name: "stars").Children<VisualElement>().ToList();
		StartCoroutine(FillStars(stars));

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
		GameManager.Instance.ChangeScene(GameScene.Current);
	}

	private void OnClickedNextLevel()
	{
		GameManager.Instance.ChangeScene(GameScene.NextLevel);
	}

	private void OnClickedMainMenu()
	{
		GameManager.Instance.ChangeScene(GameScene.Id.MainMenu);
	}

	private IEnumerator FillStars(IEnumerable<VisualElement> stars)
	{
		foreach (var star in stars.Select(star => star.Q<ToggleableVisualElement>()).Take(Stars))
		{
			yield return new WaitForSeconds(0.5f);
			star.Enabled = true;
		}
	}
}