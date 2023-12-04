using UnityEngine;

public class NormalArrow : KinematicArrow {

	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeForces;

		unit = "N";
	}

	public override Vector3 GetVector() {
        return normal;
	}
}
