using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
		if (levelImages.Length != GameManager.numberOfLevels)
			Debug.LogError("El numero de texturas para los niveles no coincide con el numero de niveles");

        for (var level = 1; level <= GameManager.numberOfLevels; level++) {
            var button = Instantiate(levelButtonPrefab, transform).GetComponent<LevelButton>();

			button.index = level;
			button.completion = GameManager.Instance.levelCompletion[level - 1];

			button.levelImage.sprite = levelImages[level - 1];

			if (button.completion == GameManager.LevelCompletion.Locked)
				button.levelImage.material = lockedMaterial;

			button.indexText.text = button.index.ToString();
			
			switch (button.completion) {
				case GameManager.LevelCompletion.Locked:
				case GameManager.LevelCompletion.Uncompleted:
					button.flagImage.enabled = false;
					break;
				case GameManager.LevelCompletion.Completed:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagRed);
					break;
				case GameManager.LevelCompletion.Par:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagGold);
					break;
				case GameManager.LevelCompletion.HoleInOne:
					button.flagImage.sprite = Atlas.GetSprite(AtlasFlagPlatinum);
					break;
			}
		}
    }
}
