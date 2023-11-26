using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIGame : MonoBehaviour
{
	public void Start()
	{
		var root = GetComponent<UIDocument>().rootVisualElement;

		root.Q<Button>("button-exit").clicked += OnButtonExit;
		root.Q<Button>("button-settings").clicked += OnButtonSettings;
		root.Q<Button>("button-restart").clicked += OnButtonRestart;
	}

	private void OnButtonExit()
	{
		SceneManager.LoadScene("Main Menu");
	}

	private void OnButtonSettings()
	{
		SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
	}

	private void OnButtonRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}