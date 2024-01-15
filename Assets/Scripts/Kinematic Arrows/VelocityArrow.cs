using UnityEngine;

public class VelocityArrow : KinematicArrow {
	protected override void Start() {
		base.Start();

		properties.isVisible = gameManager.seeVelocity;

		unit = "m/s";
	}

	private void Update() {
		properties.color = gameManager.SpeedColour;
		labelText.color = gameManager.SpeedColour;

		if(canDecomposite) {
			xComponent.properties.color = gameManager.SpeedColour;
			yComponent.properties.color = gameManager.SpeedColour;

			xLine.startColor = gameManager.SpeedColour;
			xLine.endColor = gameManager.SpeedColour;
			yLine.startColor = gameManager.SpeedColour;
			yLine.endColor = gameManager.SpeedColour;
		}
	}

	public override Vector3 GetVector() {
        return rigidbody.velocity;
	}
}
