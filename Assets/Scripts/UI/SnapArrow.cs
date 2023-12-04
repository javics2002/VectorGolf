using System;
using System.Collections;
using UnityEngine;
using static InteractableObject;

public class SnapArrow : MonoBehaviour
{
	[SerializeField]
	KinematicArrow.ArrowProperties arrowProperties;

	[SerializeField]
	public VectorDirection VectorDirection = 0f;

	[SerializeField, Range(0f, 1f)]
	private float animationTime;

	[SerializeField]
	EasingFunctions.InterpolationType interpolationType;

	[SerializeField]
	Material arrowMaterial;

	public InterfaceArrow interfaceArrow { get; private set; }
	private Vector2 currentForce, lastForce;

	private void Awake()
	{
		interfaceArrow = new GameObject(gameObject.name + " snap arrow").AddComponent<InterfaceArrow>();
		interfaceArrow.properties = arrowProperties;
		interfaceArrow.target = transform.parent;
		interfaceArrow.gameObject.layer = LayerMask.NameToLayer("UI");
		interfaceArrow.canDecomposite = false;
		interfaceArrow.GetComponent<MeshRenderer>().material = arrowMaterial;

		//TODO poner valor inicial de la flecha
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
			case ObjectType.Spring:
			case ObjectType.Fan:
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
		var type =
			transform.parent.GetComponentInChildren<InteractableObject>().Type;

		var shouldReturn = type switch
		{
			ObjectType.Ball or ObjectType.Drone
				=> draggedObject.GetComponent<VectorForce>() is not null,
			ObjectType.Spring or ObjectType.Fan or ObjectType.Vehicle
				=> draggedObject.GetComponent<ScalarForce>() is not null,
			_ => throw new ArgumentOutOfRangeException()
		};

		if (shouldReturn) ReturnToOldArrow();
	}

	public void DeleteArrow()
	{
		interfaceArrow.properties.isVisible = false;
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
			EasingFunctions.GetEasingFunction(interpolationType)));

		GameManager.Instance.mouseOverObject = transform.parent.gameObject;
	}

	private void CreateScalarArrow(float force)
	{
		interfaceArrow.properties.isVisible = true;

		currentForce = transform.parent.ToDirection(VectorDirection) * force;
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(currentForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType)));

		GameManager.Instance.mouseOverObject = transform.parent.gameObject;
	}

	private void ReturnToOldArrow()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(lastForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType)));

		GameManager.Instance.mouseOverObject = null;
		GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
	}

	private IEnumerator AnimateArrow(Vector2 newVector, float animationTime,
		EasingFunctions.Interpolation interpolation)
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
	}
}