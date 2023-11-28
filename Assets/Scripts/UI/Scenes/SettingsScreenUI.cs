using UI.Elements;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SettingsScreenUI : MonoBehaviour
{
	[Header("Audio")]
	public AudioMixer Mixer;

	private float _musicVolume;
	private float _soundVolume;

	private UIColor255 _colourBall;
	private UIColor255 _colourSpeed;
	private UIColor255 _colourForces;

	private Backdrop _deleteProgressBackdrop;

	private void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;
		var gm = GameManager.Instance;
		_musicVolume = gm.MusicVolume;
		_soundVolume = gm.SoundVolume;
		_colourBall = new UIColor255(gm.BallColour);
		_colourSpeed = new UIColor255(gm.SpeedColour);
		_colourForces = new UIColor255(gm.ForcesColour);

		SetUpVolumeSliders(root);
		SetUpColours(root);
		SetUpButtons(root);
		SetUpModal(root);

		Time.timeScale = 0f;
	}

	private void OnDestroy()
	{
		Time.timeScale = 1f;
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
		var musicVolume = root.Q<Slider>("volume-music");
		musicVolume.value = _musicVolume;
		musicVolume.RegisterValueChangedCallback(OnMusicVolumeChange);

		var soundVolume = root.Q<Slider>("volume-sound");
		soundVolume.value = _soundVolume;
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
		var red = root.Q<IntegerField>("red");
		red.value = color.Red;
		red.RegisterValueChangedCallback(@event => color.Red = @event.newValue);

		var green = root.Q<IntegerField>("green");
		green.value = color.Green;
		green.RegisterValueChangedCallback(@event => color.Green = @event.newValue);

		var blue = root.Q<IntegerField>("blue");
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
		gm.MusicVolume = _musicVolume;
		gm.SoundVolume = _soundVolume;
		gm.BallColour = _colourBall.Color;
		gm.SpeedColour = _colourSpeed.Color;
		gm.ForcesColour = _colourForces.Color;

		gm.LoadManager.Save();
		SceneManager.UnloadSceneAsync((int)GameScene.Id.Settings);
	}

	private void OnMusicVolumeChange(ChangeEvent<float> @event)
	{
		_musicVolume = @event.newValue;
		Mixer.SetFloat("musicVol", LinearVolumeToDecibels(@event.newValue));
	}

	private void OnSoundVolumeChange(ChangeEvent<float> @event)
	{
		_soundVolume = @event.newValue;
		Mixer.SetFloat("sfxVol", LinearVolumeToDecibels(@event.newValue));
	}

	private static float LinearVolumeToDecibels(float linearVolume)
	{
		return 20 * Mathf.Log10(linearVolume);
	}
}