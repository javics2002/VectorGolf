using System;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
	public void Load()
	{
		// Añadir settings
		GameManager.Instance.levelCompletion = new GameManager.LevelCompletion[GameManager.numberOfLevels];

		for (var level = 0; level < GameManager.numberOfLevels; level++)
			GameManager.Instance.levelCompletion[level] =
				(GameManager.LevelCompletion)PlayerPrefs.GetInt(MakeLevelKey(level),
					(int)(level == 0 ? GameManager.LevelCompletion.Uncompleted : GameManager.LevelCompletion.Locked));
	}

	public void Save()
	{
		for (var level = 0; level < GameManager.numberOfLevels; level++)
			PlayerPrefs.SetInt(MakeLevelKey(level),
				(int)GameManager.Instance.levelCompletion[level]);

		PlayerPrefs.Save();
	}

	/// <summary>
	/// Saves the level completion for the given level.
	/// </summary>
	/// <remarks>
	/// This method does not normalize 1-based level indices (1, 2, ...) to 0-based level indices (0, 1, ...).
	/// </remarks>
	/// <param name="level">The 0-based level index.</param>
	/// <param name="completion">The completion progress for the level.</param>
	/// <exception cref="IndexOutOfRangeException">If <c>level</c> is lower than zero or higher or equals than <see cref="GameManager.levelCompletion"/>.</exception>
	public void Save(int level, GameManager.LevelCompletion completion)
	{
		if (level < 0 || level >= GameManager.Instance.levelCompletion.Length)
		{
			throw new IndexOutOfRangeException("Cannot save to a level that does not exist");
		}

		GameManager.Instance.levelCompletion[level] = completion;
		PlayerPrefs.SetInt(MakeLevelKey(level), (int)completion);
		PlayerPrefs.Save();
	}

	private static string MakeLevelKey(int level) => $"level{level}completion";
}