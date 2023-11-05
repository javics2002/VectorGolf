using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	public override Vector3 GetVector() {
		//return GameManager.Instance.convertToScale(rigidbody.gravityScale * Vector2.down + rigidbody.totalForce);
		return rigidbody.gravityScale * Vector2.down + rigidbody.totalForce;
	}
}

