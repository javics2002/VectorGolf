using System.Collections;
using System.Collections.Generic;
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
}
