using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LoadManager LoadManager { get; private set; }
    
    /// <summary>
    /// The data for the current level.
    /// </summary>
    public LevelData LevelData;
    
    /// <summary>
    /// The level this goal is for, calculated from the scene name.
    /// </summary>
    public GameLevel Level { get; private set; } = GameLevel.Invalid;

	[Header("Gameplay State")]
	public bool isArrowVisible;
	public bool isDragging;
	public GameObject draggedObject;
    public GameObject mouseOverObject;

    [Header("Gameplay Settings")]
    public bool seeVelocity;
    public bool seeForces;
    public bool seeVectorLabels;
    public bool seeVectorValues;
    public bool seeAnimations;
    public bool vectorDecomposition;

    [Header("Force Limiters")]
    public float minForce = 1.0f; // Valor m�nimo de la fuerza original
    public float maxForce = 20.0f; // Valor m�ximo de la fuerza original
    public float minLinearValue = 0.0f; // Valor m�nimo del rango lineal
    public float maxLinearValue = 5.0f; // Valor m�ximo del rango lineal
    
	public const int NumberOfLevels = 12;

    public enum LevelCompletionStatus { Locked, Uncompleted, Completed, Par, HoleInOne };

    public struct LevelProgress
    {
	    public LevelCompletionStatus Status { get; set; }
    }

	[Header("Progression")]
    public readonly LevelProgress[] progress = new LevelProgress[NumberOfLevels];

    [Header("Settings")]
    [Range(0, 1)]
    public float MusicVolume;
    [Range(0, 1)]
	public float SoundVolume;

	public Color BallColour;
	public Color SpeedColour;
	public Color ForcesColour;

    [Header("Transitions")]
    public Animator left;
    public Animator right;
    public Animator icon;
    public float transitionTime;
    
    [Header("References")]
	[SerializeField]
	public GameObject linePrefab;

	private void Awake()
	{
		if (Instance is not null) {
			Instance.OnNewScene();
			Instance.LevelData = LevelData;
			Destroy(gameObject);
            return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);

		Instance.OnNewScene();
		Instance.Init();
	}

	private void LateUpdate() {
        KinematicArrow[] arrows = FindObjectsOfType<KinematicArrow>();

        for(int i = 0; i < arrows.Length; i++) {
			Bounds iBounds = arrows[i].labelText.textBounds;
			iBounds.center = arrows[i].labelText.rectTransform.position;

			for (int j = i + 1; j < arrows.Length; j++) {
				Bounds jBounds = arrows[j].labelText.textBounds;
				jBounds.center = arrows[j].labelText.rectTransform.position;

				while (jBounds.Intersects(iBounds) && arrows[j].labelText.rectTransform.position != arrows[i].labelText.rectTransform.position) {
					jBounds.center = arrows[j].labelText.rectTransform.position = arrows[j].labelText.rectTransform.position
						+ (arrows[j].labelText.rectTransform.position - arrows[i].labelText.rectTransform.position).normalized * .1f;
				}
			}
        }
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


    public void changeScene(int sceneBuildIndex)
    {
        StartCoroutine(loadLevel(sceneBuildIndex));
    }

    IEnumerator loadLevel(int sceneBuildIndex)
    {
        // Play anim
        left.SetBool("End", false);
        right.SetBool("End", false);
        icon.SetBool("End", false);

        left.SetBool("Start", true);
        right.SetBool("Start", true);
        icon.SetBool("Start", true);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneBuildIndex);

        left.SetBool("Start", false);
        right.SetBool("Start", false);
        icon.SetBool("Start", false);

        left.SetBool("End", true);
        right.SetBool("End", true);
        icon.SetBool("End", true);
    }
}