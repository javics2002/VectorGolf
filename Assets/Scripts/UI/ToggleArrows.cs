using UnityEngine;

public class ToggleArrows : MonoBehaviour
{
	public void ToggleAllVelocityArrowsVisible() {
		foreach (VelocityArrow velocityArrow in FindObjectsOfType<VelocityArrow>())
			velocityArrow.ToggleVisible();
	}

	public void ToggleAllAccelerationArrowsVisible() {
		foreach (AccelerationArrow accelerationArrow in FindObjectsOfType<AccelerationArrow>())
			accelerationArrow.ToggleVisible();
	}

	public void ToggleForceDecomposition() {

	}

	public void ToggleXYComponents() {

	}

	public void ToggleLabels() {
		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.ToggleLabelVisible();
	}
}
