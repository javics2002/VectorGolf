using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Progress")]
    public int index;
	public GameManager.LevelCompletion completion;

    //[Header("Flags")]
    //public Sprite completedFlag;
    //public Sprite parFlag, holeInOneFlag;

    [Header("References")]
    public Image backgroundImage; 
    public Image levelImage, flagImage;
    public TextMeshProUGUI indexText;

    Button button;

	private void Start() {
		button = GetComponent<Button>();

        if (completion == GameManager.LevelCompletion.Locked)
            button.interactable = false;
	}

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + index);
    }
}
