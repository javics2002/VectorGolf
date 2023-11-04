using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	[Header("Audio")]
    public AudioMixer mixer;
	public Slider musicSlider, soundsSlider;

	private void Start() {
		LoadValues();
	}

	public void LoadValues() {
		musicSlider.value = GameManager.Instance.musicVolume;
		soundsSlider.value = GameManager.Instance.soundsVolume;
	}

	public void SaveAndClose() {
        GameManager.Instance.Save();

        SceneManager.UnloadSceneAsync("Settings");
    }

    public void SetMusicVolume(float musicVolume) {
		mixer.SetFloat("musicVol", LinearVolumeToDecibels(musicVolume));
        GameManager.Instance.musicVolume = musicVolume;
	}

    public void SetSoundsVolume(float soundsVolume) {
		mixer.SetFloat("sfxVol", LinearVolumeToDecibels(soundsVolume));
		GameManager.Instance.soundsVolume = soundsVolume;
	}

    float LinearVolumeToDecibels (float linearVolume) {
        return 20 * Mathf.Log10(linearVolume);
	}
}
