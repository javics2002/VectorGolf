using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LoadManager LoadManager { get; private set; }
    
    /// <summary>
    /// The level this goal is for, calculated from the scene name.
    /// </summary>
    public GameLevel Level { get; private set; } = GameLevel.Invalid;

	[Header("Gameplay State")]
	public bool isArrowVisible;
	public bool isDragging;
	public GameObject draggedObject;
    public GameObject mouseOverObject;

    [Header("Force Limiters")]
    public float minForce = 1.0f; // Valor m�nimo de la fuerza original
    public float maxForce = 20.0f; // Valor m�ximo de la fuerza original
    public float minLinearValue = 0.0f; // Valor m�nimo del rango lineal
    public float maxLinearValue = 5.0f; // Valor m�ximo del rango lineal
    
	public const int NumberOfLevels = 3;

    public enum LevelCompletionStatus { Locked, Uncompleted, Completed, Par, HoleInOne };

    public struct LevelProgress
    {
	    public int Retries { get; set; }
	    public LevelCompletionStatus Status { get; set; }
    }

	[Header("Progression")]
    public readonly LevelProgress[] progress = new LevelProgress[NumberOfLevels];

    [Header("Settings")]
    [Range(0, 1)]
    public float MusicVolume;
    [Range(0, 1)]
	public float SoundVolume;

#if UNITY_EDITOR
    [Header("Hacks")]
    public bool UnlockAllLevels;
#endif

	private void Awake()
	{
		if (Instance is not null) {
			Destroy(gameObject);
			Instance.OnNewScene();
            return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		Instance.OnNewScene();
		Instance.Init();
	}

	private void OnNewScene()
	{
		// Get current scene name and verify it's formatted correctly with the format 'LevelN', where N is an integer:
		var sceneName = SceneManager.GetActiveScene().name;
		if (sceneName.Length > 5 && sceneName.StartsWith("Level") && int.TryParse(sceneName[5..], out var level))
		{
			Level = new GameLevel { Current = level };
		}
		else
		{
			Level = GameLevel.Invalid;
		}
	}

    private void Init()
	{
		isDragging = false;
        isArrowVisible = false;
		draggedObject = null;
        mouseOverObject = null;

        LoadManager = gameObject.AddComponent<LoadManager>();
		LoadManager.Load();
		        
#if UNITY_EDITOR
        if (UnlockAllLevels)
        {
	        for (var i = 0; i < NumberOfLevels; i++)
	        {
		        if (progress[i].Status == LevelCompletionStatus.Locked)
			        progress[i].Status = LevelCompletionStatus.Uncompleted;
	        }
        }
#endif
    }

    public void Save()
    {
		LoadManager.Save();
    }
    
	public void DragObject(GameObject dragObject)
	{
        draggedObject = dragObject;
		isDragging = true;
    }
    public void DropObject()
    {
        draggedObject = null;
        isDragging = false;
    }

    public float convertToScale(float originalForce)
    {
        // Aplicar regla de tres para escalar la fuerza
        float linealValue = Mathf.InverseLerp(minForce, maxForce, originalForce);
        float scaleValue = Mathf.Lerp(minLinearValue, maxLinearValue, linealValue);

        return scaleValue;
    }

    public Vector3 convertToScale(Vector3 vector)
    {
        // Aplicar regla de tres para escalar la fuerza
        float linealValueX = Mathf.InverseLerp(minForce, maxForce, vector.x);
        float scaleValueX = Mathf.Lerp(minLinearValue, maxLinearValue, linealValueX);

        float linealValueY = Mathf.InverseLerp(minForce, maxForce, vector.y);
        float scaleValueY = Mathf.Lerp(minLinearValue, maxLinearValue, linealValueY);

        float linealValueZ = Mathf.InverseLerp(minForce, maxForce, vector.z);
        float scaleValueZ = Mathf.Lerp(minLinearValue, maxLinearValue, linealValueZ);

        return new Vector3(scaleValueX, scaleValueY, scaleValueZ);
    }

    /// <summary>
    ///     Applies a force to the ball given a <paramref name="rotation" /> in degrees.
    /// </summary>
    /// <param name="rotation">The amount of degrees</param>
    /// <param name="force">The amount of force that should be applied</param>
    //   public void ApplyForceToBall(float rotation, float force)
    //{
    //	var radians = rotation * Mathf.Deg2Rad;
    //	_playerRigidBody.AddForce(new Vector2(Mathf.Cos(radians) * force, Mathf.Sin(radians) * force));
    //}
}