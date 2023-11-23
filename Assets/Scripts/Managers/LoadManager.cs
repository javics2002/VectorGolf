using System;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
	private const int LoaderVersion = 1;
	private const string VolumeMusicKey = "volume:music";
	private const string VolumeSoundKey = "volume:sound";

	public void Load()
	{
		if (PlayerPrefs.GetInt("version", 0) != LoaderVersion)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("version", LoaderVersion);
		}

		var gm = GameManager.Instance;
		var progress = gm.progress;
		for (var level = 0; level < progress.Length; level++)
		{
			progress[level].Status = (GameManager.LevelCompletionStatus)PlayerPrefs.GetInt(
				MakeLevelCompletionKey(level),
				(int)(level == 0
					? GameManager.LevelCompletionStatus.Uncompleted
					: GameManager.LevelCompletionStatus.Locked));
		}

		gm.SoundVolume = PlayerPrefs.GetFloat(VolumeSoundKey, 1);
		gm.MusicVolume = PlayerPrefs.GetFloat(VolumeMusicKey, 1);
	}

	public void Save()
	{
		var progress = GameManager.Instance.progress;
		for (var level = 0; level < progress.Length; level++)
		{
			SaveLevelProgress(level, progress[level]);
		}

		PlayerPrefs.Save();
	}

	/// <summary>
	/// Saves the level completion for the given level.
	/// </summary>
	/// <param name="level">The 0-based level index.</param>
	/// <param name="completionStatus">The completion progress for the level.</param>
	/// <exception cref="IndexOutOfRangeException">If <c>level</c> is lower than zero or higher or equals than <see cref="GameManager.progress"/>.</exception>
	public void Save(int level, GameManager.LevelCompletionStatus completionStatus)
	{
		CheckOutOfBounds(level);

		var progress = GameManager.Instance.progress;
		progress[level].Status = completionStatus;

		SaveLevelProgress(level, progress[level]);
		PlayerPrefs.Save();
	}

	public void SaveVolume(float musicVolume, float soundVolume)
	{
		PlayerPrefs.SetFloat(VolumeMusicKey, musicVolume);
		PlayerPrefs.SetFloat(VolumeSoundKey, soundVolume);
		PlayerPrefs.Save();
	}

	/// <summary>
	/// Checks whether or not the given level is out of bounds.
	/// </summary>
	/// <param name="level">The 0-based level index to check.</param>
	/// <exception cref="IndexOutOfRangeException">If <c>level</c> is lower than zero or higher or equals than <see cref="GameManager.progress"/>.</exception>
	private static void CheckOutOfBounds(int level)
	{
		if (level < 0 || level >= GameManager.Instance.progress.Length)
		{
			throw new IndexOutOfRangeException("Cannot load a level that does not exist");
		}
	}

	private static string MakeLevelCompletionKey(int level) => $"level:{level}:completion";

	private static void SaveLevelProgress(int level, GameManager.LevelProgress progress)
	{
		PlayerPrefs.SetInt(MakeLevelCompletionKey(level), (int)progress.Status);
	}
}