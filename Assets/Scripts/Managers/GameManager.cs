using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    LoadManager loadManager;

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
    
	public const int numberOfLevels = 3;
    public enum LevelCompletion { Locked, Uncompleted, Completed, Par, HoleInOne };

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
        mouseOverObject = null;

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