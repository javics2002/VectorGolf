using System.Collections.Generic;

using TMPro;

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public abstract class KinematicArrow : MonoBehaviour {
	[System.Serializable]
	public class ArrowProperties {
		public bool isVisible;
		public bool isLabelVisible;
		public bool isValueVisible;
		public int priority;
		public Color color;
		public float stemLength, stemWidth;
		public float tipLength, tipWidth;
		public bool forceSmooth;
		public string name;
		public float labelSize = 6;
		public float labelSeparation = 1;
	}

	public static ArrowType CreateArrow<ArrowType>(string name, Transform target, ArrowProperties properties)
		where ArrowType : KinematicArrow {
		GameObject arrowGameObject = new GameObject(name);
		arrowGameObject.layer = LayerMask.NameToLayer("UI");
		ArrowType arrow = arrowGameObject.AddComponent<ArrowType>();

		arrow.target = target;
		arrow.properties = properties;

		return arrow;
	}

	public ArrowProperties properties { get; set; }
	//Durante las animaciones no quiero mostrar la flecha real
	public bool animating { get; set; } = false;
	public Transform target { get; set; }

	[System.NonSerialized]
	public List<Vector3> verticesList;
	[System.NonSerialized]
	public List<int> trianglesList;

	protected Mesh mesh;
	protected MeshRenderer meshRenderer;
	protected MeshFilter meshFilter;
	protected new Rigidbody2D rigidbody;
	protected TextMeshPro labelText;

	public Vector3 lastFrameVector;

	protected bool forceSmooth;

	protected float threshold = .5f;
	protected float lerpFactor = .1f;

	void Start() {
		verticesList = new List<Vector3>();
		trianglesList = new List<int>();

		rigidbody = target.GetComponent<Rigidbody2D>();

		transform.position = Vector3.zero;

		mesh = new Mesh();
		meshRenderer = GetComponent<MeshRenderer>();
		meshFilter = GetComponent<MeshFilter>();

		lastFrameVector = Vector3.zero;

		labelText = new GameObject(name + " Label").AddComponent<TextMeshPro>();
		labelText.fontSize = 6;
		labelText.rectTransform.sizeDelta = new Vector2(5, 2);
		labelText.alignment = TextAlignmentOptions.Center;
		labelText.color = properties.color;
	}

	private void OnDestroy() {
		if(labelText)
			Destroy(labelText.gameObject);
	}

	void LateUpdate() {
		Vector3 vector = GetVector();
		transform.position = target.position + properties.priority * Vector3.back * .1f;

		if (properties.isVisible && !animating && IsLongEnoughToDraw(vector)) {
			CreateArrowMesh(vector);

			if (properties.isLabelVisible || properties.isValueVisible) {
				labelText.enabled = true;

				labelText.rectTransform.position = target.position + vector / 2 
					+ Vector3.Cross(vector, Vector3.back).normalized * properties.labelSeparation + properties.priority * Vector3.back * .1f;
				labelText.text = (properties.isLabelVisible ? (!properties.name.Equals("") ? properties.name + ": " : "") : "")
					+ (properties.isValueVisible ? vector.magnitude.ToString("0.0") : "");
			}
			else
				labelText.enabled = false;

			meshRenderer.enabled = true;
			vector.Normalize();

			if (forceSmooth)
				transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(Smooth(vector)));
			else
				transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(SmoothIfSimilar(vector, threshold)));
		}
		else {
			meshRenderer.enabled = false;
			labelText.enabled = false;
		}
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

	void CreateArrowMesh(Vector3 vector) {
		verticesList.Clear();
		trianglesList.Clear();

		Vector3 stemOrigin = Vector3.zero;
		float stemHalfWidth = properties.stemWidth * .5f;
		properties.stemLength = vector.magnitude - properties.tipLength;

		//Stem points
		verticesList.Add(stemOrigin + stemHalfWidth * Vector3.down);
		verticesList.Add(stemOrigin + stemHalfWidth * Vector3.up);
		verticesList.Add(verticesList[0] + properties.stemLength * Vector3.right);
		verticesList.Add(verticesList[1] + properties.stemLength * Vector3.right);

		//Stem triangles
		trianglesList.Add(0);
		trianglesList.Add(1);
		trianglesList.Add(3);

		trianglesList.Add(0);
		trianglesList.Add(3);
		trianglesList.Add(2);

		//tip setup
		Vector3 tipOrigin = stemOrigin + properties.stemLength * Vector3.right;
		float tipHalfWidth = properties.tipWidth / 2;

		//tip points
		verticesList.Add(tipOrigin + tipHalfWidth * Vector3.up);
		verticesList.Add(tipOrigin + tipHalfWidth * Vector3.down);
		verticesList.Add(tipOrigin + properties.tipLength * Vector3.right);

		//tip triangle
		trianglesList.Add(4);
		trianglesList.Add(6);
		trianglesList.Add(5);

		//assign lists to mesh.
		mesh.vertices = verticesList.ToArray();
		mesh.triangles = trianglesList.ToArray();

		meshFilter.mesh = mesh;
		meshRenderer.material.color = properties.color;
	}

	public bool IsLongEnoughToDraw(Vector3 vector) {
		return vector.magnitude - properties.tipLength >= 0;
	}

	float ArrowRotation(Vector2 vector) => Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);

	public abstract Vector3 GetVector();

	public void SetVisible(bool visible) {
		properties.isVisible = visible;
	}

	public void SetLabelVisible(bool visible) {
		properties.isLabelVisible = visible;
	}

	public void SetValueVisible(bool visible) {
		properties.isValueVisible = visible;
	}

	public void SetForceSmooth(bool newValue) {
		forceSmooth = newValue;
	}
}
