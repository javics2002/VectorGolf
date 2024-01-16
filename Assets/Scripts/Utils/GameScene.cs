﻿using System;
using UnityEngine.SceneManagement;

/// <summary>
/// A static class that represents the game scenes.
/// </summary>
public static class GameScene
{
	/// <summary>
	/// An enumeration that represents the IDs of the game scenes.
	/// </summary>
	public enum Id
	{
		MainMenu,
		LevelSelection,
		Settings,
		Tutorial,
		Level1,
		Level2,
		Level3,
		Level4,
		Level5,
		Level6,
		Level7,
		Level8,
		Level9,
		Level10,
		Level11,
		Level12,
		Level13,
		Level14
	}

	/// <summary>
	/// Returns the ID of the level corresponding to the given number.
	/// </summary>
	/// <param name="number">The number of the level.</param>
	/// <returns>The ID of the level.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when the level number is not between 1 and 12.</exception>
	public static Id Level(int number)
	{
		if (number is < 1 or > 14)
		{
			throw new ArgumentOutOfRangeException(nameof(number), number, "Level number must be between 1 and 14");
		}

		return number + Id.Level1 - 1;
	}

	/// <summary>
	/// Returns the level number corresponding to the given ID.
	/// </summary>
	/// <param name="number">The ID of the level.</param>
	/// <returns>The number of the level.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when the level ID is not between Level1 and Level12.</exception>
	public static int Level(Id number)
	{
		if (number is < Id.Level1 or > Id.Level14)
		{
			throw new ArgumentOutOfRangeException(nameof(number), number, "Level number must be between 1 and 14");
		}

		return number - Id.Level1 + 1;
	}

	public static Id LevelTutorial()
	{
		return Id.Tutorial;
	}

	/// <summary>
	/// Gets the ID of the current active scene.
	/// </summary>
	public static Id Current => (Id)SceneManager.GetActiveScene().buildIndex;

	/// <summary>
	/// Gets the ID of the next level.
	/// </summary>
	/// <exception cref="IndexOutOfRangeException">Thrown when the current level is not within the range of levels Tutorial-11.</exception>
	public static Id NextLevel =>
		Current switch
		{
			>= Id.Tutorial and <= Id.Level13 => Current + 1,
			_ => throw new IndexOutOfRangeException("The current level is not within the range of levels Tutorial-11")
		};

	/// <summary>
	/// Loads the Scene by its name or index in Build Settings.
	/// </summary>
	/// <param name="sceneId">Index of the Scene in the Build Settings to load.</param>
	/// <param name="mode">Allows you to specify whether or not to load the Scene additively. See SceneManagement.LoadSceneMode for more information about the options.</param>
	public static void LoadScene(Id sceneId, LoadSceneMode mode = LoadSceneMode.Single)
	{
		SceneManager.LoadScene((int)sceneId, mode);
	}
}