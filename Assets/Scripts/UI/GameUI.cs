using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIGame : MonoBehaviour
{
	private Button _buttonExit;
	private Button _buttonSettings;
	private Button _buttonRestart;
	private Button _buttonPause;

	[SerializeField]
	private Sprite _pauseIconSprite;

    [SerializeField]
    private Sprite _playIconSprite;

    public void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		(_buttonExit = root.Q<Button>("button-exit")).clicked += OnButtonExit;
		(_buttonSettings = root.Q<Button>("button-settings")).clicked += OnButtonSettings;
		(_buttonRestart = root.Q<Button>("button-restart")).clicked += OnButtonRestart;
		(_buttonPause = root.Q<Button>("button-pause")).clicked += OnButtonPause;

		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	private void Update()
	{
		if (_buttonExit.enabledSelf && Input.GetKeyDown(KeyCode.Escape))
		{
			OnButtonSettings();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			OnButtonPause();
		}
	}

	private void OnSceneUnloaded(Scene scene)
	{
		EnableUI(scene.buildIndex == (int)GameScene.Id.Settings);
	}

	public void EnableUI(bool value)
	{
		_buttonExit.SetEnabled(value);
		_buttonSettings.SetEnabled(value);
		_buttonRestart.SetEnabled(value);
		_buttonPause.SetEnabled(value);

		foreach (var item in FindObjectsOfType<DraggableItem>())
		{
			item.canDrag = value;
		}
	}

	private void OnButtonExit()
	{
		GameManager.Instance.RestartTime();
		GameManager.Instance.ChangeScene(GameScene.Id.MainMenu);
	}

	private void OnButtonSettings()
	{
        GameManager.Instance.RestartTime();
        GameScene.LoadScene(GameScene.Id.Settings, LoadSceneMode.Additive);
		EnableUI(false);
	}

	private void OnButtonRestart()
	{
        GameManager.Instance.RestartTime();
        GameManager.Instance.ChangeScene(GameScene.Current);
	}

	private void OnButtonPause()
	{
		if (!_buttonPause.enabledSelf)
		{
			return;
		}


		if (GameManager.Instance.isTimeStopped) 
		{
			GameManager.Instance.RestartTime();
			SetPauseIcon();
		}
		else
		{
			GameManager.Instance.StopTime();
            SetPlayIcon();
        }
	}

	public void SetPauseIcon()
	{
        foreach (VisualElement visualElement in _buttonPause.Children())
        {
            visualElement.style.backgroundImage = new StyleBackground(_pauseIconSprite);
        }
    }

	public void SetPlayIcon()
	{
        foreach (VisualElement visualElement in _buttonPause.Children())
        {
            visualElement.style.backgroundImage = new StyleBackground(_playIconSprite);
        }
    }
}