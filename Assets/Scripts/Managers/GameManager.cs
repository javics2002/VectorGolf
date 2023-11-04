using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    LoadManager loadManager;

	[Header("Gameplay State")]
	public bool isArrowVisible;
	public bool isDragging;
	public GameObject draggedObject;

	public const int numberOfLevels = 3;
    public enum LevelCompletion { Locked, Uncompleted, Completed, Par, HoleInOne };

	//[Space(10)]
	[Header("Progression")]
    public LevelCompletion[] levelCompletion;

	[Header("Settings")]
	public float musicVolume, soundsVolume;

    [Header("Hacks")]
    public bool unlockAllLevels;

	private void Awake()
	{
		if (Instance != null) {
			Destroy(gameObject);

            return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		Instance.Init();
	}

    private void Init()
	{
		isDragging = false;
        isArrowVisible = false;
		draggedObject = null;

        loadManager = gameObject.AddComponent<LoadManager>();

		loadManager.Load();

#if UNITY_EDITOR
        if(unlockAllLevels)
            for(int i = 0; i < numberOfLevels; i++)
                if (levelCompletion[i] == LevelCompletion.Locked)
                    levelCompletion[i] = LevelCompletion.Uncompleted;
#endif
    }

    public void Save()
    {
		loadManager.Save();
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