using UnityEngine;

public class DumpingArrow : KinematicArrow {


	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeForces;

		unit = "N";
	}

	public override Vector3 GetVector() {
		if(rigidbody.velocity.sqrMagnitude < 0.1f)
			return Vector3.zero;

        return Vector3.RotateTowards(rigidbody.drag * normal, - rigidbody.velocity, 3f, 0);
	}
}
