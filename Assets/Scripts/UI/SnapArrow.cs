using System;
using System.Collections;
using UnityEngine;
using static InteractableObject;

public class SnapArrow : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties arrowProperties, additionArrowProperties;

	[SerializeField]
	public VectorDirection VectorDirection = 0f;

	[SerializeField, Range(0f, 1f)]
	private float animationTime, additionAnimationTime;
	[SerializeField, Range(0f, .5f)]
	private float additionWaitTime;

	[SerializeField]
	EasingFunctions.InterpolationType interpolationType;

	[SerializeField]
	Material arrowMaterial;

	public InterfaceArrow interfaceArrow { get; private set; }
	public InterfaceArrow interfaceAdditionArrow { get; private set; }
	private Vector2 currentForce, lastForce;
	private VelocityArrow ballVelocityArrow;

	private void Awake()
	{
		interfaceArrow = new GameObject(gameObject.name + " snap arrow").AddComponent<InterfaceArrow>();
		interfaceArrow.properties = arrowProperties;
		interfaceArrow.target = transform.parent;
		interfaceArrow.gameObject.layer = LayerMask.NameToLayer("UI");
		interfaceArrow.canDecomposite = false;
		interfaceArrow.GetComponent<MeshRenderer>().material = arrowMaterial;

		interfaceAdditionArrow = new GameObject(gameObject.name + " addition arrow").AddComponent<InterfaceArrow>();
		interfaceAdditionArrow.properties = additionArrowProperties;
		interfaceAdditionArrow.target = transform.parent;
		interfaceAdditionArrow.gameObject.layer = LayerMask.NameToLayer("UI");
		interfaceAdditionArrow.canDecomposite = false;
		interfaceAdditionArrow.GetComponent<MeshRenderer>().material = arrowMaterial;
	}

	private void Start() {
		ballVelocityArrow = GameObject.Find("Ball Velocity Arrow").GetComponent<VelocityArrow>();

		currentForce = Vector2.zero;
		lastForce = Vector2.zero;
	}

	private void OnMouseEnter()
	{
		if (!GameManager.Instance.isDragging)
			return;

		var draggedObject = GameManager.Instance.draggedObject;
		var interactableObjectType =
			transform.parent.gameObject.GetComponentInChildren<InteractableObject>().Type;

		switch (interactableObjectType)
		{
			case ObjectType.Ball:
			case ObjectType.Drone:
				var vectorForce = draggedObject.GetComponent<VectorForce>();
				if (vectorForce is null) return;

				// Show arrow and make the group invisible:
				CreateVectorArrow(vectorForce.Force);
				draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
				break;
			case ObjectType.Vehicle:
				var scalarForce = draggedObject.GetComponent<ScalarForce>();
				if (scalarForce is null) return;

				// Show arrow and make the group invisible:
				CreateScalarArrow(scalarForce.Force);
				draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void OnMouseExit()
	{
		if (!GameManager.Instance.isDragging || !interfaceArrow.properties.isVisible)
			return;

		var draggedObject = GameManager.Instance.draggedObject;
		var type = transform.parent.GetComponentInChildren<InteractableObject>().Type;

		var shouldReturn = type switch
		{
			ObjectType.Ball or ObjectType.Drone
				=> draggedObject.GetComponent<VectorForce>() is not null,
			ObjectType.Vehicle
				=> draggedObject.GetComponent<ScalarForce>() is not null,
			_ => throw new ArgumentOutOfRangeException()
		};

		if (shouldReturn) ReturnToOldArrow();
	}

	public void DeleteArrow()
	{
		interfaceArrow.properties.isVisible = false;
		interfaceArrow.SetInterfaceArrow(Vector3.zero);

		interfaceAdditionArrow.properties.isVisible = false;
		interfaceAdditionArrow.SetInterfaceArrow(Vector3.zero);

		GameManager.Instance.mouseOverObject = null;
	}

	public void SaveForce()
	{
		lastForce = currentForce;
	}

	private void CreateVectorArrow(Vector2 force)
	{
		interfaceArrow.properties.isVisible = true;

		currentForce = force;
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(currentForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType),
			GameManager.Instance.isTimeStopped));

		GameManager.Instance.mouseOverObject = transform.parent.gameObject;
	}

	private void CreateScalarArrow(float force)
	{
		interfaceArrow.properties.isVisible = true;

		currentForce = transform.parent.ToDirection(VectorDirection) * force;
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(currentForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType), 
			GameManager.Instance.isTimeStopped));

		GameManager.Instance.mouseOverObject = transform.parent.gameObject;
	}

	private void ReturnToOldArrow()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(lastForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType), false));

		interfaceAdditionArrow.properties.isVisible = false;
		interfaceAdditionArrow.SetInterfaceArrow(Vector3.zero);

		GameManager.Instance.mouseOverObject = null;
		GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
	}

	private IEnumerator AnimateArrow(Vector2 newVector, float animationTime,
		EasingFunctions.Interpolation interpolation, bool seeAddition)
	{
		Vector2 initialVector = interfaceArrow.GetVector();

		float t = 0;
		while (t < animationTime)
		{
			interfaceArrow.SetInterfaceArrow(Vector3.Lerp(initialVector,
				newVector, interpolation(t / animationTime)));

			yield return null;
			t += Time.unscaledDeltaTime;
		}

		interfaceArrow.SetInterfaceArrow(newVector);

		if (seeAddition) {
			yield return new WaitForSecondsRealtime(additionWaitTime);

			interfaceAdditionArrow.properties.isVisible = true;

			Vector2 currentVelocity = ballVelocityArrow.GetVector();
			Vector2 additionVector = currentVelocity + newVector;
			t = 0;
			while (t < animationTime) {
				interfaceAdditionArrow.SetInterfaceArrow(Vector3.Lerp(currentVelocity,
					additionVector, interpolation(t / animationTime)));

				yield return null;
				t += Time.unscaledDeltaTime;
			}

			interfaceAdditionArrow.SetInterfaceArrow(additionVector);
		}
	}
}