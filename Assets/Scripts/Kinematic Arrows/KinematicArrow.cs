using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class KinematicArrow : MonoBehaviour
{
	[System.Serializable]
	public class ArrowProperties {
		public bool visible;
		public int priority;
		public Color color;
		public float stemLength, stemWidth;
		public float tipLegth, tipWidth;
		public bool forceSmooth;
	}

	public static ArrowType CreateArrow<ArrowType>(string name, Transform target, ArrowProperties properties) 
		where ArrowType : KinematicArrow {
		GameObject arrowGameObject = new GameObject(name);
		arrowGameObject.layer = LayerMask.NameToLayer("UI");
		ArrowType arrow = arrowGameObject.AddComponent<ArrowType>();

		arrow.isVisible = properties.visible;
		arrow.target = target;
		arrow.color = properties.color;
		arrow.stemWidth = properties.stemWidth;
		arrow.tipLength = properties.tipLegth;
		arrow.tipWidth = properties.tipWidth;
		arrow.priority = properties.priority;

		return arrow;
	}

	public bool isVisible { get; set; }
	//Durante las animaciones no quiero mostrar la flecha real
	public bool animating { get; set; } = false;

	public Transform target { get; set; }
	
	public float stemLength { get; private set; }
	public float stemWidth { get; set; }
	public float tipLength { get; set; }
	public float tipWidth { get; set; }

	public Color color { get; set; }
	public float priority { get; set; }

	[System.NonSerialized]
	public List<Vector3> verticesList;
	[System.NonSerialized]
	public List<int> trianglesList;

	protected Mesh mesh;
	protected MeshRenderer meshRenderer;
	protected MeshFilter meshFilter;
	protected new Rigidbody2D rigidbody;

	public Vector3 lastFrameVector;

	protected bool forceSmooth;

	protected float threshold = .5f;
	protected float lerpFactor = .1f;
	
	void Start()
    {
        verticesList = new List<Vector3>();
        trianglesList = new List<int>();

        rigidbody = target.GetComponent<Rigidbody2D>();

        transform.position = Vector3.zero;

        mesh = new Mesh();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        lastFrameVector = Vector3.zero;
    }

    void LateUpdate()
    {
		Vector3 vector = GetVector();
		transform.position = target.position + priority * Vector3.back * .1f;

		if (isVisible && !animating && DrawArrow(vector)) {
			meshRenderer.enabled = true;
			vector.Normalize();

			if (forceSmooth)
                transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(Smooth(vector)));
			else
				transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(SmoothIfSimilar(vector, threshold)));
		}
		else
			meshRenderer.enabled = false;
	}

	private Vector3 SmoothIfSimilar(Vector3 vector, float threshold) {
		if ((lastFrameVector - vector).sqrMagnitude < threshold * threshold)
			return Smooth(vector);

		lastFrameVector = vector;
		return vector;
	}

	private Vector3 Smooth(Vector3 vector) {
		Vector3 newVector = Vector3.Lerp(lastFrameVector, vector, lerpFactor);
		lastFrameVector = newVector;
		return newVector;
	}

	bool DrawArrow(Vector3 vector) {
		verticesList.Clear();
		trianglesList.Clear();

		//stem setup
		Vector3 stemOrigin = Vector3.zero;
		float stemHalfWidth = stemWidth * .5f;
		stemLength = vector.magnitude - tipLength;

		if(stemLength < 0) {
			//Flecha demasiado pequeña como para pintarla
			return false;
		}
		
		//Stem points
		verticesList.Add(stemOrigin + stemHalfWidth * Vector3.down);
		verticesList.Add(stemOrigin + stemHalfWidth * Vector3.up);
		verticesList.Add(verticesList[0] + stemLength * Vector3.right);
		verticesList.Add(verticesList[1] + stemLength * Vector3.right);

		//Stem triangles
		trianglesList.Add(0);
		trianglesList.Add(1);
		trianglesList.Add(3);

		trianglesList.Add(0);
		trianglesList.Add(3);
		trianglesList.Add(2);

		//tip setup
		Vector3 tipOrigin = stemOrigin + stemLength * Vector3.right;
		float tipHalfWidth = tipWidth / 2;

		//tip points
		verticesList.Add(tipOrigin + tipHalfWidth * Vector3.up);
		verticesList.Add(tipOrigin + tipHalfWidth * Vector3.down);
		verticesList.Add(tipOrigin + tipLength * Vector3.right);

		//tip triangle
		trianglesList.Add(4);
		trianglesList.Add(6);
		trianglesList.Add(5);

		//assign lists to mesh.
		mesh.vertices = verticesList.ToArray();
		mesh.triangles = trianglesList.ToArray();

		meshFilter.mesh = mesh;
		meshRenderer.material.color = color;
		return true;
	}

	float ArrowRotation(Vector2 vector) => Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);

	protected abstract Vector3 GetVector();

	public void ToggleVisible() {
		isVisible = !isVisible;
	}

	public void SetForceSmooth(bool newValue)
	{
		forceSmooth = newValue;
	}
}
