using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    CanvasGroup mainMenuElements;

	private void Start() {
		mainMenuElements = GetComponentInChildren<CanvasGroup>();

        SceneManager.sceneUnloaded += ShowMainMenu;
	}

	void OnDestroy() {
		SceneManager.sceneUnloaded -= ShowMainMenu;
	}

	public void Play() {
        SceneManager.LoadScene("Level Selection");
    }

    public void Settings() {
        mainMenuElements.alpha = 0;
        mainMenuElements.blocksRaycasts = false;
		SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
	}

    public void Exit() {
        Application.Quit();
    }

    void ShowMainMenu(Scene unloadedScene) {
		if(unloadedScene.name == "Settings") {
			mainMenuElements.alpha = 1;
			mainMenuElements.blocksRaycasts = true;
		}
	}
}
