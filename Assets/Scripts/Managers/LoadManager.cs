using System;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
	private const int LoaderVersion = 1;
	private const string VolumeMusicKey = "volume:music";
	private const string VolumeSoundKey = "volume:sound";
	private const string ColourBallKey = "colour:ball";
	private const string ColourSpeedKey = "colour:speed";
	private const string ColourForcesKey = "colour:forces";

	private const string FirstTimeEnteringLevelKey = "level:firsttime";

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
			progress[level].Status = (GameManager.LevelCompletionStatus)PlayerPrefs.GetInt(MakeLevelCompletionKey(level),
				(int)(level == 0 ? GameManager.LevelCompletionStatus.Uncompleted : GameManager.LevelCompletionStatus.Locked));

            progress[level].tutorialCardShown = PlayerPrefs.GetInt(MakeLevelTutorialShownKey(level), 0) == 1;
        }

		gm.Audio.MusicVolume = PlayerPrefs.GetFloat(VolumeMusicKey, 1f);
		gm.Audio.SoundVolume = PlayerPrefs.GetFloat(VolumeSoundKey, 1f);

		gm.BallColour = GetColour(ColourBallKey, gm.BallColour);
		gm.SpeedColour = GetColour(ColourSpeedKey, gm.SpeedColour);
		gm.ForcesColour = GetColour(ColourForcesKey, gm.ForcesColour);

		gm.firstTimeEnteringLevel = PlayerPrefs.GetInt(FirstTimeEnteringLevelKey, 0) == 0;
	}

	public void Save()
	{
		var gm = GameManager.Instance;
		var progress = gm.progress;
		for (var level = 0; level < progress.Length; level++)
		{
			SaveLevelProgress(level, progress[level]);
		}

		PlayerPrefs.SetFloat(VolumeSoundKey, gm.Audio.SoundVolume);
		PlayerPrefs.SetFloat(VolumeMusicKey, gm.Audio.MusicVolume);

		SetColour(ColourBallKey, gm.BallColour);
		SetColour(ColourSpeedKey, gm.SpeedColour);
		SetColour(ColourForcesKey, gm.ForcesColour);

		PlayerPrefs.Save();
	}

	/// <summary>
	/// Saves the level completion for the given level.
	/// </summary>
	/// <param name="level">The 0-based level index.</param>
	/// <param name="progress">The progress for the level.</param>
	/// <exception cref="IndexOutOfRangeException">If <c>level</c> is lower than zero or higher or equals than <see cref="GameManager.progress"/>.</exception>
	public void Save(int level, GameManager.LevelProgress progress)
	{
		CheckOutOfBounds(level);

		var levelProgress = GameManager.Instance.progress;

        levelProgress[level].Status = progress.Status;
        levelProgress[level].tutorialCardShown = progress.tutorialCardShown;

		SaveLevelProgress(level, levelProgress[level]);
        PlayerPrefs.Save();
	}

    public void SaveFirstTimeEnteringLevel()
	{
		PlayerPrefs.SetInt(FirstTimeEnteringLevelKey, 1);
		PlayerPrefs.Save();
	}

    /// <summary>
    /// Checks whether or not the given level is out of bounds.
    /// </summary>
    /// <param name="level">The 0-based level index to check.</param>
    /// <exception cref="IndexOutOfRangeException">If <c>level</c> is lower than zero or higher or equals than <see cref="GameManager.progress"/>.</exception>
    private static void CheckOutOfBounds(int level)
	{
		if (level is >= 0 and < GameManager.NumberOfLevels) return;

		throw new IndexOutOfRangeException("Cannot load a level that does not exist");
	}

	private static string MakeLevelCompletionKey(int level) => $"level:{level}:completion";

	private static string MakeLevelTutorialShownKey(int level) => $"level:{level}:tutorial";

	private static void SaveLevelProgress(int level, GameManager.LevelProgress progress)
	{
		PlayerPrefs.SetInt(MakeLevelCompletionKey(level), (int)progress.Status);
        PlayerPrefs.SetInt(MakeLevelTutorialShownKey(level), progress.tutorialCardShown ? 1 : 0);
    }

	private static void SetColour(string name, Color colour)
	{
		PlayerPrefs.SetFloat($"{name}:r", colour.r);
		PlayerPrefs.SetFloat($"{name}:g", colour.g);
		PlayerPrefs.SetFloat($"{name}:b", colour.b);
	}

	private static Color GetColour(string name, Color defaultValue)
	{
		return new Color(
			PlayerPrefs.GetFloat($"{name}:r", defaultValue.r),
			PlayerPrefs.GetFloat($"{name}:g", defaultValue.g),
			PlayerPrefs.GetFloat($"{name}:b", defaultValue.b));
	}
}