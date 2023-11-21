using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Progress")]
    public int index;
	[FormerlySerializedAs("completion")]
	public GameManager.LevelCompletionStatus CompletionStatus;

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

        if (CompletionStatus == GameManager.LevelCompletionStatus.Locked)
            button.interactable = false;
	}

    public void LoadLevel() {
        SceneManager.LoadScene("Level" + index);
    }
}
