using UnityEngine;

public class AccelerationArrow : KinematicArrow {
	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeForces;

		unit = "N";
	}

	public override Vector3 GetVector() {
		//return GameManager.Instance.convertToScale(rigidbody.gravityScale * Vector2.down + rigidbody.totalForce);
		return rigidbody.gravityScale * Vector2.down + rigidbody.totalForce;
	}
}

