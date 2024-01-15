using UnityEngine;

public class KinematicViewer : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties velocityArrowProperties, accelerationArrowProperties,
		normalArrowProperties, dumpingArrowProperties;

	[SerializeField]
	Material arrowMaterial;

	Ball ball;

	private void Awake() {
		ball = GetComponentInChildren<Ball>();
		if (ball) {
			ball.velocityArrow = KinematicArrow.CreateArrow<VelocityArrow>(gameObject.name 
				+ " Velocity Arrow", transform, velocityArrowProperties, arrowMaterial);
		}
		else{
			KinematicArrow.CreateArrow<VelocityArrow>(gameObject.name + " Velocity Arrow",
			transform, velocityArrowProperties, arrowMaterial);
		}
		KinematicArrow.CreateArrow<GravityArrow>(gameObject.name + " Gravity Arrow", 
			transform, accelerationArrowProperties, arrowMaterial);
		KinematicArrow.CreateArrow<NormalArrow>(gameObject.name + " Normal Arrow",
			transform, normalArrowProperties, arrowMaterial);
		KinematicArrow.CreateArrow<DumpingArrow>(gameObject.name + " Dumping Arrow",
			transform, dumpingArrowProperties, arrowMaterial);
	}
}
