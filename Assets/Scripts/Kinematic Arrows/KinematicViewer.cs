using UnityEngine;

public class KinematicViewer : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties velocityArrowProperties, accelerationArrowProperties;

	[SerializeField]
	Material arrowMaterial;

	Ball ball;

	private void Awake() {
		ball = GetComponentInChildren<Ball>();

		ball.velocityArrow = 
			KinematicArrow.CreateArrow<VelocityArrow>(gameObject.name + " Velocity Arrow", 
			transform, velocityArrowProperties, arrowMaterial);
		KinematicArrow.CreateArrow<GravityArrow>(gameObject.name + " Acceleration Arrow", 
			transform, accelerationArrowProperties, arrowMaterial);
		KinematicArrow.CreateArrow<NormalArrow>(gameObject.name + " Acceleration Arrow",
			transform, accelerationArrowProperties, arrowMaterial);
	}
}
