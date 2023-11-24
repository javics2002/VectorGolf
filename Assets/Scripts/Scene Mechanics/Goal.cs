using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Goal : MonoBehaviour
{
	[SerializeField]
	private SpriteAtlas Atlas;

	private const string AtlasFlagDefault = "game-flag-default";
	private const string AtlasFlagRed = "game-flag-red";
	private const string AtlasFlagGold = "game-flag-gold";
	private const string AtlasFlagPlatinum = "game-flag-platinum";

	private void Start()
	{
		// Set the sprite to the correct flag sprite from the atlas:
		GetComponent<SpriteRenderer>().sprite = Atlas.GetSprite(GetGoalSpriteName());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Player")) return;

		var gm = GameManager.Instance;
		Assert.IsNotNull(gm);

		var ball = collision.transform.GetComponentInChildren<Ball>();
		Assert.IsNotNull(ball);

		var stars = 1;
		if (ball.Hits <= gm.LevelData.Gold) stars++;
		if (ball.Hits <= gm.LevelData.Platinum) stars++;

		// Get the number of retries for this level:
		var levelIndex = gm.Level.CurrentIndex;

		// Save the level completion:
		gm.LoadManager.Save(levelIndex, GetLevelCompletionStatus(stars));

		// Find the GameObject named "WinScreen":
		var winScreen = FindObjectOfType<WinScreen>(true);
		if (winScreen is null) return;

		winScreen.Stars = stars;
		winScreen.Hits = ball.Hits;
		winScreen.gameObject.SetActive(true);
	}

	private static string GetGoalSpriteName()
	{
		var gm = GameManager.Instance;

		// If the current level is 0 or greater than the number of levels, return the default flag sprite:
		var index = gm.Level;
		if (!index.IsValid()) return AtlasFlagDefault;

		return gm.progress[index.CurrentIndex].Status switch
		{
			GameManager.LevelCompletionStatus.HoleInOne => AtlasFlagPlatinum,
			GameManager.LevelCompletionStatus.Par => AtlasFlagGold,
			GameManager.LevelCompletionStatus.Completed => AtlasFlagRed,
			_ => AtlasFlagDefault
		};
	}

	private static GameManager.LevelCompletionStatus GetLevelCompletionStatus(int stars)
	{
		return stars switch
		{
			1 => GameManager.LevelCompletionStatus.Completed,
			2 => GameManager.LevelCompletionStatus.Par,
			3 => GameManager.LevelCompletionStatus.HoleInOne,
			_ => GameManager.LevelCompletionStatus.Uncompleted
		};
	}
}