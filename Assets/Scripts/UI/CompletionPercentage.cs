using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CompletionPercentage : MonoBehaviour
{
	private void Start()
	{
		var progress = GameManager.Instance.progress;
		var completedLevels = progress.Count(t => t.Status >= GameManager.LevelCompletionStatus.Completed);
		GetComponent<TextMeshProUGUI>().text = $"{100 * (float)completedLevels / progress.Length:0}% completed";
	}
}