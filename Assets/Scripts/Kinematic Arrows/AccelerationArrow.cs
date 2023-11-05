using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	protected override Vector3 GetVector() {
		return GameManager.Instance.convertToScale(rigidbody.gravityScale * Vector2.down + rigidbody.totalForce);
	}
}

