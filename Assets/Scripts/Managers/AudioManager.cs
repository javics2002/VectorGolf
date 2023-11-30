using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class to manage audio settings and playback in the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
	/// <summary>
	/// The volume of the music, ranging from 0 (silent) to 1 (full volume).
	/// </summary>
	private float _musicVolume = 1f;

	/// <summary>
	/// The volume of the sound effects, ranging from 0 (silent) to 1 (full volume).
	/// </summary>
	private float _soundVolume = 1f;

	/// <summary>
	/// Key for accessing the music volume in the audio mixer.
	/// </summary>
	private const string VolumeMusicKey = "MusicVolume";

	/// <summary>
	/// Key for accessing the sound volume in the audio mixer.
	/// </summary>
	private const string VolumeSoundKey = "SoundVolume";

	/// <summary>
	/// The audio mixer used to control the volume of the audio sources.
	/// </summary>
	[Header("Audio")]
	public AudioMixer Mixer;

	/// <summary>
	/// The audio source used for playing music.
	/// </summary>
	[Header("Sources")]
	[SerializeField]
	private AudioSource MusicSource;

	/// <summary>
	/// The audio clips used for the music.
	/// </summary>
	[SerializeField]
	private AudioClip[] MusicClips;

	/// <summary>
	/// Gets the current music volume or updates the volume and the audio mixer.
	/// </summary>
	public float MusicVolume
	{
		get => _musicVolume;
		set => SetMusicVolume(_musicVolume = value);
	}

	/// <summary>
	/// Gets the current sound volume or updates the volume and the audio mixer.
	/// </summary>
	public float SoundVolume
	{
		get => _soundVolume;
		set => SetSoundVolume(_soundVolume = value);
	}

	/// <summary>
	/// Sets the initial music and sound volumes.
	/// </summary>
	public void Start()
	{
		// This needs to be called on `Start` because the audio mixer is not available in `Awake`.
		// See: https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.SetFloat.html

		SetMusicVolume(MusicVolume);
		SetSoundVolume(SoundVolume);

		if (MusicClips.Length != 0) StartCoroutine(nameof(PlayMusic));
	}

	private IEnumerator PlayMusic()
	{
		while (true)
		{
			var clip = MusicClips[Random.Range(0, MusicClips.Length)];
			MusicSource.clip = clip;
			MusicSource.Play();
			yield return new WaitForSecondsRealtime(clip.length);
		}

		// ReSharper disable once IteratorNeverReturns
	}

	/// <summary>
	/// Sets the music volume in the audio mixer.
	/// </summary>
	/// <param name="value">The new music volume.</param>
	private void SetMusicVolume(float value)
	{
		Mixer.SetFloat(VolumeMusicKey, LinearVolumeToDecibels(value));
	}

	/// <summary>
	/// Sets the sound volume in the audio mixer.
	/// </summary>
	/// <param name="value">The new sound volume.</param>
	private void SetSoundVolume(float value)
	{
		Mixer.SetFloat(VolumeSoundKey, LinearVolumeToDecibels(value));
	}

	/// <summary>
	/// Converts a linear volume value to decibels.
	/// </summary>
	/// <param name="linearVolume">The linear volume value to convert.</param>
	/// <returns>The converted volume in decibels.</returns>
	private static float LinearVolumeToDecibels(float linearVolume) => Mathf.Max(-80, 20 * Mathf.Log10(linearVolume));
}