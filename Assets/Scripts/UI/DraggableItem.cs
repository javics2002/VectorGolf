using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	[SerializeField, Range(0f, 1f)]
	float scaleWhenDrag;

	private RectTransform rectTransform;
	private Canvas canvas;
	private CanvasGroup canvasGroup;

	private Vector2 initialPositon;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// Dont pick invisible items
		if (canvasGroup.alpha == 0f)
		{
			return;
		}

		GameManager.Instance.isDragging = true;
		GameManager.Instance.draggedObject = gameObject;

		initialPositon = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);

		canvasGroup.alpha = 0.7f;
		canvasGroup.blocksRaycasts = false;

		rectTransform.localScale = new Vector3(scaleWhenDrag, scaleWhenDrag, scaleWhenDrag);
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var mouseOverObject = GameManager.Instance.mouseOverObject;

		// When an object is not dropped inside a valid gameobject
		if (!mouseOverObject)
		{
			canvasGroup.alpha = 1f;

			rectTransform.localScale = Vector3.one;
			rectTransform.anchoredPosition = initialPositon;

			canvasGroup.blocksRaycasts = true;

			GameManager.Instance.isDragging = false;
			GameManager.Instance.draggedObject = null;

			return;
		}

		var interactable = mouseOverObject.GetComponentInChildren<InteractableObject>();
		if (interactable is null) return;

		var arrow = mouseOverObject.GetComponentInChildren<SnapArrow>();
		var applyForce = interactable.objectType switch
		{
			InteractableObject.ObjectType.Ball => ApplyForce(arrow, (Ball)interactable),
			InteractableObject.ObjectType.Spring => ApplyForce(arrow, (Spring)interactable),
			InteractableObject.ObjectType.Fan => ApplyForce(arrow, (Fan)interactable),
			InteractableObject.ObjectType.Vehicle => ApplyForce(arrow, (Vehicle)interactable),
			_ => throw new ArgumentOutOfRangeException()
		};

		if (!applyForce) return;

		GameManager.Instance.isDragging = false;
		GameManager.Instance.draggedObject = null;
		GameManager.Instance.mouseOverObject = null;
		canvasGroup.alpha = 0f;
	}

	private bool ApplyForce(SnapArrow arrow, Ball ball)
	{
		var vectorForce = GetComponent<VectorForce>();
		if (vectorForce is null) return false;

		StartCoroutine(ball.Hit(vectorForce.GetVectorialForce()));

		// Delete arrow
		arrow.DeleteArrow();
		return true;
	}

	private bool ApplyForce(SnapArrow arrow, Spring spring)
	{
		var scalarForce = GetComponent<ScalarForce>();
		if (scalarForce is null) return false;

		spring.setSpringForce(scalarForce.GetScalarForce());
		arrow.SaveForce();
		return true;
	}

	private bool ApplyForce(SnapArrow arrow, Fan fan)
	{
		var scalarForce = GetComponent<ScalarForce>();
		if (scalarForce is null) return false;

		fan.SetFanForce(scalarForce.GetScalarForce());
		arrow.SaveForce();
		return true;
	}

	private bool ApplyForce(SnapArrow arrow, Vehicle vehicle)
	{
		var scalarForce = GetComponent<ScalarForce>();
		if (scalarForce is null) return false;

		vehicle.SetScalarForce(scalarForce.GetScalarForce());
		arrow.SaveForce();
		return true;
	}
}