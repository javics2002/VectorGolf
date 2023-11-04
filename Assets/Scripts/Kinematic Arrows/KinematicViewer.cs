using UnityEngine;
using UnityEngine.UI;

public class KinematicViewer : MonoBehaviour
{
	[System.Serializable]
	public class ArrowProperties {
		public bool visible;
		public int priority;
		public Color color;
		public float stemLength, stemWidth;
		public float tipLegth, tipWidth;
	}

	[SerializeField]
	ArrowProperties velocityArrow, accelerationArrow;

	private void Start() {

		CreateArrow<VelocityArrow>("Velocity Arrow", velocityArrow);
		CreateArrow<AccelerationArrow>("Acceleration Arrow", accelerationArrow);
	}

	Transform CreateArrow<ArrowType>(string name, ArrowProperties properties) where ArrowType : KinematicArrow {
		KinematicArrow arrow = new GameObject(name).AddComponent<ArrowType>();

		arrow.target = transform;
        arrow.setVisible(properties.visible);
        arrow.color = properties.color;
		arrow.stemWidth = properties.stemWidth;
		arrow.tipLength = properties.tipLegth;
		arrow.tipWidth = properties.tipWidth;
		arrow.priority = properties.priority;

		return transform;
	}
}
