using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	protected override Vector3 GetVector() {
		return rigidbody.gravityScale * Vector2.down + rigidbody.totalForce;
	}
}
