using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIGame : MonoBehaviour
{
	private Button _buttonExit;
	private Button _buttonSettings;
	private Button _buttonRestart;

	public void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		(_buttonExit = root.Q<Button>("button-exit")).clicked += OnButtonExit;
		(_buttonSettings = root.Q<Button>("button-settings")).clicked += OnButtonSettings;
		(_buttonRestart = root.Q<Button>("button-restart")).clicked += OnButtonRestart;

		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	private void OnSceneUnloaded(Scene scene)
	{
		EnableUI(scene.buildIndex == (int)GameScene.Id.Settings);
	}

	private void EnableUI(bool value)
	{
		_buttonExit.SetEnabled(value);
		_buttonSettings.SetEnabled(value);
		_buttonRestart.SetEnabled(value);

		foreach (var item in FindObjectsOfType<DraggableItem>())
		{
			item.canDrag = value;
		}
	}

	private void OnButtonExit()
	{
		GameManager.Instance.ChangeScene(GameScene.Id.MainMenu);
	}

	private void OnButtonSettings()
	{
		GameScene.LoadScene(GameScene.Id.Settings, LoadSceneMode.Additive);
		EnableUI(false);
	}

	private void OnButtonRestart()
	{
		GameManager.Instance.ChangeScene(GameScene.Current);
	}
}