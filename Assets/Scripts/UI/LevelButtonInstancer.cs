using UnityEngine;
using UnityEngine.U2D;

public class LevelButtonInstancer : MonoBehaviour
{
	public GameObject levelButtonPrefab;

	[Header("Colors")]
	public Material lockedMaterial;

	[SerializeField]
	public SpriteAtlas Atlas;

	[Header("Level Images")]
	public Sprite[] levelImages;

	private const string AtlasFlagRed = "flag-level-red";
	private const string AtlasFlagGold = "flag-level-gold";
	private const string AtlasFlagPlatinum = "flag-level-platinum";

	private void Start()
	{
		if (levelImages.Length != GameManager.NumberOfLevels)
		{
			Debug.LogError("El numero de texturas para los niveles no coincide con el numero de niveles");
		}

		for (var level = 1; level <= GameManager.NumberOfLevels; level++)
		{
			var button = Instantiate(levelButtonPrefab, transform).GetComponent<LevelButton>();

			button.index = level;
			button.CompletionStatus = GameManager.Instance.progress[level - 1].Status;

			button.levelImage.sprite = levelImages[level - 1];

			if (button.CompletionStatus == GameManager.LevelCompletionStatus.Locked)
				button.levelImage.material = lockedMaterial;

			button.indexText.text = button.index.ToString();

			switch (button.CompletionStatus)
			{
				case GameManager.LevelCompletionStatus.Locked:
				case GameManager.LevelCompletionStatus.Uncompleted:
					button.flagImage.enabled = false;
					break;
				case GameManager.LevelCompletionStatus.Completed:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagRed);
					break;
				case GameManager.LevelCompletionStatus.Par:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagGold);
					break;
				case GameManager.LevelCompletionStatus.HoleInOne:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagPlatinum);
					break;
			}
		}
	}
}