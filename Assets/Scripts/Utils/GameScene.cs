using System;
using UnityEngine.SceneManagement;

public static class GameScene
{
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
	}

	public static Id Level(int number)
	{
		if (number is < 1 or > 12)
		{
			throw new ArgumentOutOfRangeException(nameof(number), number, "Level number must be between 1 and 12");
		}

		return (Id)number + 3;
	}

	public static Id Current => (Id)SceneManager.GetActiveScene().buildIndex;

	public static Id NextLevel =>
		Current switch
		{
			>= Id.Tutorial and <= Id.Level11 => Current + 1,
			_ => throw new IndexOutOfRangeException("The current level is not within the range of levels Tutorial-11")
		};
}