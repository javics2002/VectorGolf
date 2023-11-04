using UnityEngine;

public class LoadManager : MonoBehaviour
{
    public void Load()
    {
        //Añadir settings

        GameManager.Instance.levelCompletion = new GameManager.LevelCompletion[GameManager.numberOfLevels];

		for (int level = 0; level < GameManager.numberOfLevels; level++)
            GameManager.Instance.levelCompletion[level] =
                (GameManager.LevelCompletion) PlayerPrefs.GetInt("level" + level + "completion",
                (int) (level == 0 ? GameManager.LevelCompletion.Uncompleted : GameManager.LevelCompletion.Locked));
    }

    public void Save()
    {
        for (int level = 0; level < GameManager.numberOfLevels; level++)
            PlayerPrefs.SetInt("level" + level + "completion", 
                (int) GameManager.Instance.levelCompletion[level]);

        PlayerPrefs.Save();
    }
}
