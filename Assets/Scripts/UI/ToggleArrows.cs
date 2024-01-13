using UnityEngine;
using UnityEngine.UI;

public class ToggleArrows : MonoBehaviour
{
	GameManager gameManager;

	private void Start() {
		gameManager = GameManager.Instance;

		Toggle velocityToggle = transform.GetChild(0).GetComponent<Toggle>();
		velocityToggle.isOn = gameManager.seeVelocity;

		if (!velocityToggle.isOn) { 
			ToggleAllVelocityArrowsVisible();
		}

		Toggle forcesToggle = transform.GetChild(1).GetComponent<Toggle>();
		forcesToggle.isOn = gameManager.seeForces;

		if (!forcesToggle.isOn)
		{
			ToggleAllAccelerationArrowsVisible();
		}

		Toggle labelsToggle = transform.GetChild(2).GetComponent<Toggle>();
		labelsToggle.isOn = gameManager.seeVectorLabels;

		if (!labelsToggle.isOn)
		{
			ToggleLabels();
		}


		Toggle valuesToggle = transform.GetChild(3).GetComponent<Toggle>();
		valuesToggle.isOn = gameManager.seeVectorValues;

		if (!valuesToggle.isOn)
		{
			ToggleValues();
		}

		Toggle animationToggle = transform.GetChild(4).GetComponent<Toggle>();
		animationToggle.isOn = gameManager.seeAnimations;

		if (!animationToggle.isOn)
		{
			ToggleAnimations();
		}

		Toggle vectorDecompositionToggle = transform.GetChild(5).GetComponent<Toggle>();
		vectorDecompositionToggle.isOn = gameManager.vectorDecomposition;

		if (!vectorDecompositionToggle.isOn)
		{
			ToggleVectorDecomposition();
		}
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

		foreach (GravityArrow gravityArrow in FindObjectsOfType<GravityArrow>())
			gravityArrow.SetVisible(gameManager.seeForces);

		foreach (NormalArrow normalArrow in FindObjectsOfType<NormalArrow>())
			normalArrow.SetVisible(gameManager.seeForces);

		foreach (DumpingArrow dumpingArrow in FindObjectsOfType<DumpingArrow>())
			dumpingArrow.SetVisible(gameManager.seeForces);
	}

	public void ToggleLabels() {
		gameManager.seeVectorLabels = !gameManager.seeVectorLabels;

		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.SetLabelVisible(gameManager.seeVectorLabels);
	}

	public void ToggleValues() {
		gameManager.seeVectorValues = !gameManager.seeVectorValues;

		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.SetValueVisible(gameManager.seeVectorValues);
	}

	public void ToggleAnimations() {
		gameManager.seeAnimations = !gameManager.seeAnimations;
	}

	public void ToggleVectorDecomposition() {
		gameManager.vectorDecomposition = !gameManager.vectorDecomposition;

		foreach (KinematicArrow kinematicArrow in FindObjectsOfType<KinematicArrow>())
			kinematicArrow.SetDecomposition(gameManager.vectorDecomposition);
	}
}
