using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[Header("Audio")]
	public AudioMixer Mixer;

	public Slider MusicSlider;
	public Slider SoundsSlider;

	private void Start()
	{
		MusicSlider.value = GameManager.Instance.MusicVolume;
		SoundsSlider.value = GameManager.Instance.SoundVolume;
	}

	public void SaveAndClose()
	{
		GameManager.Instance.LoadManager.SaveVolume(GameManager.Instance.MusicVolume, GameManager.Instance.SoundVolume);
		SceneManager.UnloadSceneAsync("Settings");
	}

	public void SetMusicVolume(float musicVolume)
	{
		Mixer.SetFloat("musicVol", LinearVolumeToDecibels(musicVolume));
		GameManager.Instance.MusicVolume = musicVolume;
	}

	public void SetSoundsVolume(float soundsVolume)
	{
		Mixer.SetFloat("sfxVol", LinearVolumeToDecibels(soundsVolume));
		GameManager.Instance.SoundVolume = soundsVolume;
	}

	private static float LinearVolumeToDecibels(float linearVolume)
	{
		return 20 * Mathf.Log10(linearVolume);
	}
}