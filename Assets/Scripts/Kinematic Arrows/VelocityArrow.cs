using UnityEngine;

public class VelocityArrow : KinematicArrow
{
	protected override Vector3 GetVector() {
		return rigidbody.velocity;
	}
}
