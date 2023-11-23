using UnityEngine;
using UnityEngine.UI;

public class ToggleArrows : MonoBehaviour
{
	GameManager gameManager;

	private void Start() {
		gameManager = GameManager.Instance;

		Toggle velocityToggle = transform.GetChild(0).GetComponent<Toggle>();
		velocityToggle.isOn = gameManager.seeVelocity;

		Toggle forcesToggle = transform.GetChild(1).GetComponent<Toggle>();
		forcesToggle.isOn = gameManager.seeForces;

		Toggle labelsToggle = transform.GetChild(2).GetComponent<Toggle>();
		labelsToggle.isOn = gameManager.seeVectorLabels;

		Toggle valuesToggle = transform.GetChild(3).GetComponent<Toggle>();
		valuesToggle.isOn = gameManager.seeVectorValues;

		Toggle animationToggle = transform.GetChild(4).GetComponent<Toggle>();
		animationToggle.isOn = gameManager.seeAnimations;

		Toggle vectorDecompositionToggle = transform.GetChild(5).GetComponent<Toggle>();
		vectorDecompositionToggle.isOn = gameManager.vectorDecomposition;
	}

	public void ToggleAllVelocityArrowsVisible() {
		gameManager.seeVelocity = !gameManager.seeVelocity;

		foreach (VelocityArrow velocityArrow in FindObjectsOfType<VelocityArrow>())
			velocityArrow.SetVisible(gameManager.seeVelocity);
	}

	public void ToggleAllAccelerationArrowsVisible() {
		gameManager.seeForces = !gameManager.seeForces;

		foreach (AccelerationArrow accelerationArrow in FindObjectsOfType<AccelerationArrow>())
			accelerationArrow.SetVisible(gameManager.seeForces);
	}

	public void ToggleLabels() {
		gameManager.seeVectorLabels = !gameManager.seeVectorLabels;

		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.SetLabelVisible(gameManager.seeVectorLabels);
	}

	public void ToggleValues() {
		gameManager.seeVectorValues = !gameManager.seeVectorValues;

		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.SetLabelVisible(gameManager.seeVectorValues);
	}

	public void ToggleAnimations() {
		gameManager.seeAnimations = !gameManager.seeAnimations;
	}

	public void ToggleVectorDecomposition() {
		gameManager.vectorDecomposition = !gameManager.vectorDecomposition;
	}
}
