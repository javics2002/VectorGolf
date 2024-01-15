using UnityEngine;

public class DumpingArrow : KinematicArrow {
	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeForces;

		unit = "N";
	}

	private void Update() {
		properties.color = gameManager.ForcesColour;
		labelText.color = gameManager.ForcesColour;

		if (canDecomposite) {
			xComponent.properties.color = gameManager.ForcesColour;
			yComponent.properties.color = gameManager.ForcesColour;

			xLine.startColor = gameManager.ForcesColour;
			xLine.endColor = gameManager.ForcesColour;
			yLine.startColor = gameManager.ForcesColour;
			yLine.endColor = gameManager.ForcesColour;
		}
	}

	public override Vector3 GetVector() {
		if(rigidbody.velocity.sqrMagnitude < 0.1f)
			return Vector3.zero;

        return Vector3.RotateTowards(friction * normal, - rigidbody.velocity, 3f, 0);
	}
}
