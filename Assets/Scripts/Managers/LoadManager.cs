using UnityEngine;

public class LoadManager : MonoBehaviour
{
    /// <summary>
    ///     The GameManager instance for the game
    /// </summary>
    public static LoadManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Instance.Init();
    }

    private void Init()
    {
        
    }

    public void Load()
    {
        //Añadir settings

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
