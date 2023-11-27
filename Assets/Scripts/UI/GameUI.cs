using System.Xml.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIGame : MonoBehaviour
{
	private VisualElement root;

	public void Start()
	{
		root = GetComponent<UIDocument>().rootVisualElement;

		root.Q<Button>("button-exit").clicked += OnButtonExit;
		root.Q<Button>("button-settings").clicked += OnButtonSettings;
		root.Q<Button>("button-restart").clicked += OnButtonRestart;

		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	private void OnDestroy() {
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	private void OnButtonExit()
	{
		SceneManager.LoadScene("Main Menu");
	}

	private void OnButtonSettings()
	{
		SceneManager.LoadScene("Settings", LoadSceneMode.Additive);

		root.Q<Button>("button-exit").SetEnabled(false);
		root.Q<Button>("button-settings").SetEnabled(false);
		root.Q<Button>("button-restart").SetEnabled(false);

		foreach(DraggableItem item in FindObjectsOfType<DraggableItem>()) {
			item.canDrag = false;
		}
	}

	private void OnButtonRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnSceneUnloaded(Scene unloadedScene) {
		root.Q<Button>("button-exit").SetEnabled(true);
		root.Q<Button>("button-settings").SetEnabled(true);
		root.Q<Button>("button-restart").SetEnabled(true);

		foreach (DraggableItem item in FindObjectsOfType<DraggableItem>()) {
			item.canDrag = true;
		}
	}
}