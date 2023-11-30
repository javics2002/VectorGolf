using System.Drawing;
using System.Linq;
using UI.Elements;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SettingsScreenUI : MonoBehaviour
{
	[Header("Audio")]
	public AudioMixer Mixer;

	private UIColor255 _colourBall;
	private UIColor255 _colourSpeed;
	private UIColor255 _colourForces;

	private Backdrop _deleteProgressBackdrop;

	private VisualElement _colourBallVE;
	private VisualElement _colourSpeedVE;
	private VisualElement _colourForcesVE;

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

		Time.timeScale = 0f;
	}

	private void OnDestroy()
	{
		Time.timeScale = 1f;
	}

	private void Update()
	{
		ClampColors(_colourBallVE);
		ClampColors(_colourSpeedVE);
		ClampColors(_colourForcesVE);

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
			_deleteProgressBackdrop.Focus();
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
		_colourBallVE = root.Q("colour-ball");
        SetUpColour(_colourBall, _colourBallVE);

		_colourSpeedVE = root.Q("colour-speed");
        SetUpColour(_colourSpeed, _colourSpeedVE);

		_colourForcesVE = root.Q("colour-forces");
        SetUpColour(_colourForces, _colourForcesVE);
	}

	private void SetUpColour(UIColor255 color, VisualElement root)
	{
		var red = root.Q<IntegerField>("red");
        red.value = color.Red;
        red.maxLength = 3;

        red.RegisterValueChangedCallback(@event => color.Red = @event.newValue);

        var green = root.Q<IntegerField>("green");
        green.value = color.Green;
        green.maxLength = 3;
        green.RegisterValueChangedCallback(@event => color.Green = @event.newValue);

		var blue = root.Q<IntegerField>("blue");
        blue.value = color.Blue;
        blue.maxLength = 3;
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

    private void ClampColors(VisualElement root)
    {
        var red = root.Q<IntegerField>("red");
        var blue = root.Q<IntegerField>("blue");
        var green = root.Q<IntegerField>("green");

        if (red.value > 255)
            red.value = 255;
        else if (red.value < 0)
            red.value = 0;

        if (green.value > 255)
            green.value = 255;
        else if (green.value < 0)
            green.value = 0;

        if (blue.value > 255)
            blue.value = 255;
        else if (blue.value < 0)
            blue.value = 0;
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
		Debug.Log("Se borra el progreso");

		var progress = GameManager.Instance.progress;
		for (var i = 0; i < GameManager.NumberOfLevels; i++)
		{
			progress[i].Status = i == 0
				? GameManager.LevelCompletionStatus.Uncompleted
				: GameManager.LevelCompletionStatus.Locked;

			progress[i].tutorialCardShown = false;
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