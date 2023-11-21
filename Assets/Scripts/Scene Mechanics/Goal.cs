using UnityEngine;
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

		// Get the number of retries for this level:
		var levelIndex = GameManager.Instance.Level.CurrentIndex;
		var retries = GameManager.Instance.progress[levelIndex].Retries;

		// Save the level completion:
		GameManager.Instance.LoadManager.Save(levelIndex, GameManager.LevelCompletionStatus.Completed, 0);

		// Find the GameObject named "WinScreen":
		var winScreen = FindObjectOfType<WinScreen>(true);
		if (winScreen is null) return;

		winScreen.Retries = retries;
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
}