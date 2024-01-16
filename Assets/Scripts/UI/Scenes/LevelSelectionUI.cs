using System;
using System.Linq;
using UI.Elements;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class LevelSelectionUI : MonoBehaviour
{
	private GroupBox _buttons;
	private LevelSelectionGroup _group;

	private void OnEnable()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		StartProgress(root);
		root.Q<Button>("button-back").clicked += OnButtonBack;

		var content = root.Q<VisualElement>("content");
		StartGroup(content);
		StartButtons(content);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) OnButtonBack();
	}

	private void OnButtonBack()
	{
		GameScene.LoadScene(GameScene.Id.MainMenu);
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

	private void StartButtons(VisualElement root)
	{
		_buttons = root.Q<GroupBox>("buttons");
		for (var i = 0; i < LevelSelectionGroup.GameSceneGroups.Length; i++)
		{
			var group = LevelSelectionGroup.GameSceneGroups[i];
			var button = (Button)_buttons[i];
			button.text = group.Name;
			button.clicked += OnGroupSelection(i);
		}
	}

	private Action OnGroupSelection(int groupIndex)
	{
		return () => PerformGroupSelection(groupIndex);
	}

	private void PerformGroupSelection(int groupIndex)
	{
		_group.Index = LevelSelectionGroup.GameSceneGroups[groupIndex].Id;

		for (var i = 0; i < _buttons.childCount; i++)
		{
			if (i == groupIndex) _buttons[i].RemoveFromClassList("unselected");
			else _buttons[i].AddToClassList("unselected");
		}
	}

	private void StartGroup(VisualElement root)
	{
		_group = root.Q<LevelSelectionGroup>("group");
		_group.Index = GameSceneGroup.GroupId.Vector;
	}
}