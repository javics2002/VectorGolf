using UnityEngine;

public class GravityArrow : KinematicArrow {
	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeForces;

		unit = "N";
	}

	public override Vector3 GetVector() {
		return rigidbody.gravityScale * Vector2.down;
	}
}

