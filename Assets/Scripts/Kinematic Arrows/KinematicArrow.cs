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

		public ArrowProperties(ArrowProperties otherProperties) {
			isVisible = otherProperties.isVisible;
			isLabelVisible = otherProperties.isLabelVisible;
			isValueVisible = otherProperties.isValueVisible;
			priority = otherProperties.priority;
			color = otherProperties.color;
			stemLength = otherProperties.stemLength;
			stemWidth = otherProperties.stemWidth;
			tipLength = otherProperties.tipLength;
			tipWidth = otherProperties.tipWidth;
			forceSmooth = otherProperties.forceSmooth;
			name = otherProperties.name;
			labelSize = otherProperties.labelSize;
			labelSeparation = otherProperties.labelSeparation;
		}
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
	public Transform target { get; set; }

	[System.NonSerialized]
	public List<Vector3> verticesList;
	[System.NonSerialized]
	public List<int> trianglesList;

	protected Mesh mesh;
	protected MeshRenderer meshRenderer;
	protected MeshFilter meshFilter;
	protected new Rigidbody2D rigidbody;
	public TextMeshPro labelText;
	protected GameManager gameManager;

	public Vector3 lastFrameVector;
	protected bool forceSmooth;
	protected float threshold = .5f;
	protected float lerpFactor = .1f;

	protected string unit = "";

	public bool canDecomposite = true;
	InterfaceArrow xComponent, yComponent;
	LineRenderer xLine, yLine;

	protected virtual void Start() {
		gameManager = GameManager.Instance;

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
		labelText.outlineColor = Color.black;
		labelText.outlineWidth = .1f;

		properties.isLabelVisible = gameManager.seeVectorLabels;
		properties.isValueVisible = gameManager.seeVectorValues;

		if(canDecomposite) {
			xComponent = new GameObject(gameObject.name + " X component").AddComponent<InterfaceArrow>();
			xComponent.canDecomposite = false;
			xComponent.properties = new ArrowProperties(properties);
			xComponent.properties.stemWidth = properties.stemWidth / 2;
			xComponent.properties.tipWidth = properties.tipWidth / 2;
			xComponent.properties.tipLength = properties.tipLength / 2;
			xComponent.properties.labelSize = properties.labelSize * .8f;
			xComponent.properties.isVisible = gameManager.vectorDecomposition;
			xComponent.target = target;
			xComponent.gameObject.layer = LayerMask.NameToLayer("UI");
			xComponent.properties.name = properties.name + "<sub>x</sub>";

			yComponent = new GameObject(gameObject.name + " Y component").AddComponent<InterfaceArrow>();
			yComponent.canDecomposite = false;
			yComponent.properties = new ArrowProperties(properties);
			yComponent.properties.stemWidth = properties.stemWidth / 2;
			yComponent.properties.tipWidth = properties.tipWidth / 2;
			yComponent.properties.tipLength = properties.tipLength / 2;
			yComponent.properties.labelSize = properties.labelSize * .8f;
			yComponent.properties.isVisible = gameManager.vectorDecomposition;
			yComponent.target = target;
			yComponent.gameObject.layer = LayerMask.NameToLayer("UI");
			yComponent.properties.name = properties.name + "<sub>y</sub>";

			xLine = Instantiate(gameManager.linePrefab).GetComponent<LineRenderer>();
			xLine.startColor = properties.color;
			xLine.endColor = properties.color;

			yLine = Instantiate(gameManager.linePrefab).GetComponent<LineRenderer>();
			yLine.startColor = properties.color;
			yLine.endColor = properties.color;
		}
	}

	private void OnDestroy() {
		if(labelText)
			Destroy(labelText.gameObject);
	}

	void Update() {
		Vector3 vector = GetVector();
		transform.position = target.position + properties.priority * Vector3.back * .1f;

		if (properties.isVisible && IsLongEnoughToDraw(vector)) {
			CreateArrowMesh(vector);

			if (properties.isLabelVisible || properties.isValueVisible) {
				labelText.enabled = true;

				labelText.rectTransform.position = target.position + vector / 2
					+ Vector3.Cross(vector, Vector3.back).normalized * properties.labelSeparation + properties.priority * Vector3.back * .1f;
				labelText.text = (properties.isLabelVisible ? (!properties.name.Equals("") ? properties.name + ": " : "") : "")
					+ (properties.isValueVisible ? vector.magnitude.ToString("0.#") + " " + unit : "");
			}
			else
				labelText.enabled = false;

			meshRenderer.enabled = true;
			
			if(canDecomposite) {
				xComponent.properties.isVisible = yComponent.properties.isVisible = 
					xLine.enabled = yLine.enabled =
					gameManager.vectorDecomposition && !(Mathf.Abs(vector.x) < 1f || Mathf.Abs(vector.y) < 1f);

				Vector3 x = new Vector3(vector.x, 0, 0);
				Vector3 y = new Vector3(0, vector.y, 0);

				xComponent.SetInterfaceArrow(x);
				yComponent.SetInterfaceArrow(y);

				xLine.SetPosition(0, target.position + y);
				xLine.SetPosition(1, target.position + x + y);

				yLine.SetPosition(0, target.position + x);
				yLine.SetPosition(1, target.position + x + y);
			}

			vector.Normalize();

			if (forceSmooth)
				transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(Smooth(vector)));
			//else
			//	transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(SmoothIfSimilar(vector, threshold)));

			//TEMP: no smooth
			else
				transform.rotation = Quaternion.Euler(0, 0, ArrowRotation(vector));
		}
		else {
			meshRenderer.enabled = false;
			labelText.enabled = false;

			if(canDecomposite) 
				xComponent.properties.isVisible = yComponent.properties.isVisible =
					xLine.enabled = yLine.enabled = false;
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

	private void PositionLabelTextToAvoidOverlap(RectTransform label, RectTransform labelX, RectTransform labelY) {
		MoveLabelToAvoidOverlap(labelX, label);
		MoveLabelToAvoidOverlap(label, labelX);
		MoveLabelToAvoidOverlap(labelY, label);
	}

	private void MoveLabelToAvoidOverlap(RectTransform labelToMove, RectTransform referenceLabel) {
		Vector3 labelToMovePosition = labelToMove.position;
		Vector3 referenceLabelPosition = referenceLabel.position;

		Vector3 moveDirection = labelToMovePosition - referenceLabelPosition;
		Vector3 labelToMoveExtents = GetRectTransformExtents(labelToMove);

		float distanceToMove = CalculateDistanceToAvoidOverlap(labelToMoveExtents, moveDirection);

		labelToMove.position += moveDirection.normalized * distanceToMove;
	}

	private float CalculateDistanceToAvoidOverlap(Vector3 extents, Vector3 moveDirection) {
		float extentAlongMoveDirection = Vector3.Dot(extents, moveDirection.normalized);
		return Mathf.Max(0f, extentAlongMoveDirection);
	}

	private Vector3 GetRectTransformExtents(RectTransform rectTransform) {
		Vector2 size = rectTransform.rect.size * 0.5f;
		return new Vector3(size.x, size.y, 0f);
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

	public void SetDecomposition(bool decomposite) {
		if (canDecomposite) {
			xComponent.SetVisible(decomposite); 
			yComponent.SetVisible(decomposite);
		}
	}

	public void SetForceSmooth(bool newValue) {
		forceSmooth = newValue;
	}
}
