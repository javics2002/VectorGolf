using UnityEngine;

public class KinematicViewer : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties velocityArrowProperties, accelerationArrowProperties;

	Ball ball;

	private void Awake() {
		ball = GetComponentInChildren<Ball>();

		ball.velocityArrow = 
			KinematicArrow.CreateArrow<VelocityArrow>("Velocity Arrow", transform, velocityArrowProperties);
		KinematicArrow.CreateArrow<AccelerationArrow>("Acceleration Arrow", transform, accelerationArrowProperties);
	}
}
