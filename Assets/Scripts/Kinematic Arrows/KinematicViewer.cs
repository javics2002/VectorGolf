using UnityEngine;

public class KinematicViewer : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties velocityArrowProperties, accelerationArrowProperties;

	Ball ball;

	private void Awake() {
		ball = GetComponentInChildren<Ball>();

		ball.velocityArrow = 
			KinematicArrow.CreateArrow<VelocityArrow>(gameObject.name + " Velocity Arrow", transform, velocityArrowProperties);
		KinematicArrow.CreateArrow<AccelerationArrow>(gameObject.name + " Acceleration Arrow", transform, accelerationArrowProperties);
	}
}
