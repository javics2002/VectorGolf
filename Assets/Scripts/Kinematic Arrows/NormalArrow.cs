using UnityEngine;

public class NormalArrow : KinematicArrow {

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
			xComponent.labelText.color = gameManager.ForcesColour;
			yComponent.properties.color = gameManager.ForcesColour;
			yComponent.labelText.color = gameManager.ForcesColour;

			xLine.startColor = gameManager.ForcesColour;
			xLine.endColor = gameManager.ForcesColour;
			yLine.startColor = gameManager.ForcesColour;
			yLine.endColor = gameManager.ForcesColour;
		}
	}

	public override Vector3 GetVector() {
        return normal;
	}
}
