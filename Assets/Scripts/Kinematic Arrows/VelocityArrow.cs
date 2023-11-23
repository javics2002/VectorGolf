using UnityEngine;

public class VelocityArrow : KinematicArrow {
	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeVelocity;

		unit = "m/s";
	}

	public override Vector3 GetVector() {
        //return GameManager.Instance.convertToScale(rigidbody.velocity);
        return rigidbody.velocity;
	}
}
