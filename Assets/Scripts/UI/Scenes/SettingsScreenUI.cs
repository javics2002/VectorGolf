using UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SettingsScreenUI : MonoBehaviour
{
	private UIColor255 _colourBall;
	private UIColor255 _colourSpeed;
	private UIColor255 _colourForces;

	private Backdrop _deleteProgressBackdrop;

	private void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;
		var gm = GameManager.Instance;
		_colourBall = new UIColor255(gm.BallColour);
		_colourSpeed = new UIColor255(gm.SpeedColour);
		_colourForces = new UIColor255(gm.ForcesColour);

		SetUpVolumeSliders(root);
		SetUpColours(root);
		SetUpButtons(root);
		SetUpModal(root);

		Time.timeScale = 0;
	}

	private void OnDestroy()
	{
		if (!GameManager.Instance.isTimeStopped)
		{
            GameManager.Instance.RestartTime();
        }
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) OnKeyDownCancel();
		else if (Input.GetKeyDown(KeyCode.Return)) OnKeyDownSubmit();
	}

	private void OnKeyDownCancel()
	{
		if (_deleteProgressBackdrop.Enabled)
		{
			_deleteProgressBackdrop.Enabled = false;
			OnDeleteProgressCancel();
		}
		else
		{
			OnBackAndSave();
		}
	}

	private void OnKeyDownSubmit()
	{
		if (_deleteProgressBackdrop.Enabled)
		{
			OnDeleteProgressConfirm();
		}
	}

	private void SetUpVolumeSliders(VisualElement root)
	{
		var audio = GameManager.Instance.Audio;
		var musicVolume = root.Q<Slider>("volume-music");
		musicVolume.value = audio.MusicVolume;
		musicVolume.RegisterValueChangedCallback(OnMusicVolumeChange);

		var soundVolume = root.Q<Slider>("volume-sound");
		soundVolume.value = audio.SoundVolume;
		soundVolume.RegisterValueChangedCallback(OnSoundVolumeChange);
	}

	private void SetUpColours(VisualElement root)
	{
		SetUpColour(_colourBall, root.Q("colour-ball"));
		SetUpColour(_colourSpeed, root.Q("colour-speed"));
		SetUpColour(_colourForces, root.Q("colour-forces"));
	}

	private void SetUpColour(UIColor255 color, VisualElement root)
	{
		var red = root.Q<EnhancedIntegerField>("red");
		red.value = color.Red;
		red.RegisterValueChangedCallback(@event => color.Red = @event.newValue);

		var green = root.Q<EnhancedIntegerField>("green");
		green.value = color.Green;
		green.RegisterValueChangedCallback(@event => color.Green = @event.newValue);

		var blue = root.Q<EnhancedIntegerField>("blue");
		blue.value = color.Blue;
		blue.RegisterValueChangedCallback(@event => color.Blue = @event.newValue);

		color.Element = root.Q<VisualElement>("preview");
	}

	private void SetUpButtons(VisualElement root)
	{
		var container = root.Q<GroupBox>("buttons");
		container.Q<Button>("delete-progress").clicked += OnDeleteProgress;
		container.Q<Button>("back").clicked += OnBackAndSave;

#if UNITY_EDITOR
		container.Q<Button>("unlock-all").clicked += OnUnlockAll;
#else
		container.Q<Button>("unlock-all").RemoveFromHierarchy();
#endif
	}

	private void OnDeleteProgress()
	{
		_deleteProgressBackdrop.Enabled = true;
		_deleteProgressBackdrop.Focus();
	}

	private void SetUpModal(VisualElement root)
	{
		_deleteProgressBackdrop = root.Q<Backdrop>("delete-progress-backdrop");
		_deleteProgressBackdrop.OnClick += OnDeleteProgressCancel;

		var dialog = _deleteProgressBackdrop.Q<ConfirmationDialog>("dialog");
		dialog.OnConfirm += OnDeleteProgressConfirm;
		dialog.OnCancel += OnDeleteProgressCancel;
	}

	private void OnDeleteProgressCancel()
	{
		_deleteProgressBackdrop.Enabled = false;
	}

	private void OnDeleteProgressConfirm()
	{
		var progress = GameManager.Instance.progress;
		for (var i = 0; i < GameManager.NumberOfLevels; i++)
		{
			progress[i].Status = i == 0
				? GameManager.LevelCompletionStatus.Uncompleted
				: GameManager.LevelCompletionStatus.Locked;

			progress[i].tutorialCardShown = false;

			GameManager.Instance.firstTimeEnteringLevel = true;
		}

		_deleteProgressBackdrop.Enabled = false;
	}

#if UNITY_EDITOR
	private void OnUnlockAll()
	{
		var progress = GameManager.Instance.progress;
		for (var i = 0; i < GameManager.NumberOfLevels; i++)
		{
			if (progress[i].Status == GameManager.LevelCompletionStatus.Locked)
				progress[i].Status = GameManager.LevelCompletionStatus.Uncompleted;
		}
	}
#endif

	private void OnBackAndSave()
	{
		var gm = GameManager.Instance;
		gm.BallColour = _colourBall.Color;
		gm.SpeedColour = _colourSpeed.Color;
		gm.ForcesColour = _colourForces.Color;

		gm.LoadManager.Save();
		SceneManager.UnloadSceneAsync((int)GameScene.Id.Settings);
	}

	private void OnMusicVolumeChange(ChangeEvent<float> evt)
	{
		GameManager.Instance.Audio.MusicVolume = evt.newValue;
	}

	private void OnSoundVolumeChange(ChangeEvent<float> evt)
	{
		GameManager.Instance.Audio.SoundVolume = evt.newValue;
	}
}