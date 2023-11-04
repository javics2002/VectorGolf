using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    ///     The GameManager instance for the game
    /// </summary>
    public static GameManager Instance { get; private set; }

    public bool isArrowVisible;
	public bool isDragging;
	public GameObject draggedObject;
    public GameObject mouseOverObject;

    public float minForce = 1.0f; // Valor mínimo de la fuerza original
    public float maxForce = 20.0f; // Valor máximo de la fuerza original
    public float minLinearValue = 0.0f; // Valor mínimo del rango lineal
    public float maxLinearValue = 5.0f; // Valor máximo del rango lineal
    #region Progreso
    public enum LevelCompletion { Locked, Uncompleted, Completed, Par, HoleInOne };

    public const int numberOfLevels = 100;
    public LevelCompletion[] levelCompletion;
    #endregion

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
		isDragging = false;
        isArrowVisible = false;
		draggedObject = null;
        mouseOverObject = null;

        LoadManager.Instance.Load();
    }

    public void Save()
    {
        LoadManager.Instance.Save();
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