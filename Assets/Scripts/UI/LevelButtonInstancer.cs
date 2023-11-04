using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelButtonInstancer : MonoBehaviour
{
    public GameObject levelButtonPrefab;

	[Header("Colors")]
	public Material lockedMaterial;
	public Color completedColor, parColor, holeInOneColor;

	[Header("Level Images")]
	public Sprite[] levelImages;

	void Start()
    {
		if(levelImages.Length != GameManager.numberOfLevels)
			Debug.LogError("El numero de texturas para los niveles no coincide con el numero de niveles");

        for(int level = 1; level <= GameManager.numberOfLevels; level++) {
            LevelButton button = Instantiate(levelButtonPrefab, transform).GetComponent<LevelButton>();

			button.index = level;
			button.completion = GameManager.Instance.levelCompletion[level - 1];

			button.levelImage.sprite = levelImages[level - 1];

			if(button.completion == GameManager.LevelCompletion.Locked)
				button.levelImage.material = lockedMaterial;

			button.indexText.text = button.index.ToString();

			switch (button.completion) {
				case GameManager.LevelCompletion.Locked:
				case GameManager.LevelCompletion.Uncompleted:
					button.flagImage.enabled = false;
					break;
				case GameManager.LevelCompletion.Completed:
					button.flagImage.color = completedColor;
					break;
				case GameManager.LevelCompletion.Par:
					button.flagImage.color = parColor;
					break;
				case GameManager.LevelCompletion.HoleInOne:
					button.flagImage.color = holeInOneColor;
					break;
			}
		}
    }
}
