using TMPro;
using UnityEngine;

public class CompletionPercentage : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        int completedLevels = 0;
        for (int i = 0; i < GameManager.numberOfLevels; i++) {
            if (GameManager.Instance.levelCompletion[i] >= GameManager.LevelCompletion.Completed)
                completedLevels++;
        }

        text.text = (100 * (float) completedLevels / GameManager.numberOfLevels).ToString("0") + "% completed";
    }
}
