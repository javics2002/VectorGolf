using UnityEngine;

public class KinematicViewer : MonoBehaviour
{
	[System.Serializable]
	public class KinematicArrow {
		public bool visible;
		public Color color;
		public float stemLength, stemWidth;
		public float tipLegth, tipWidth;
	}

	[SerializeField]
	KinematicArrow velocityArrow, accelerationArrow;

	private void Start() {
		CreateArrow("Velocity Arrow", velocityArrow);
		CreateArrow("Acceleration Arrow", accelerationArrow);
	}

	private void Update() {
		if (velocityArrow.visible) {

		}
	}

	Transform CreateArrow(string name, KinematicArrow arrow) {
		ArrowGenerator arrowGenerator = new GameObject(name).AddComponent<ArrowGenerator>();

		arrowGenerator.transform.SetParent(transform, false);
		arrowGenerator.GetComponent<MeshRenderer>().material.color = arrow.color;
		arrowGenerator.stemLength = arrow.stemLength;
		arrowGenerator.stemWidth = arrow.stemWidth;
		arrowGenerator.tipLength = arrow.tipLegth;
		arrowGenerator.tipWidth = arrow.tipWidth;

		return transform;
	}
}
