using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	static bool isAccelerationVisible;

	protected override Vector3 GetVector() {
		Vector3 vector = GameManager.Instance.convertToScale(rigidbody.gravityScale * Vector2.down + rigidbody.totalForce);
		return vector;
	}
}

