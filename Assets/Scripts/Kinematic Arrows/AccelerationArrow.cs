using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	static bool isAccelerationVisible;

	protected override Vector3 GetVector() {
		return rigidbody.gravityScale * Vector2.down + rigidbody.totalForce;
	}
}

