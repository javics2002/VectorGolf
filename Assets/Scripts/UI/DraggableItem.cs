using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static InteractableObject;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public bool canDrag { get; set; } = true;
	private bool dragging = false;

	[SerializeField, Range(0f, 1f)]
	float scaleWhenDrag;

	private Ball ball;
	private RectTransform rectTransform;
	private Canvas canvas;
	private CanvasGroup canvasGroup;
	private Image image;
	private Color initialColor, undraggableColor;

	private Vector2 initialPositon;

	private ForceKind forceKind;

	private void Awake()
	{
		ball = GameObject.Find("Ball").GetComponentInChildren<Ball>();
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
		image = GetComponentInParent<Image>();

		undraggableColor = initialColor = image.color;
		undraggableColor.a = .3f;

		if (GetComponent<VectorForce>())
			forceKind = ForceKind.Vector;
		else
			forceKind = ForceKind.Scalar;
	}

	private void Update() {
		image.color = !dragging && !CanDrag() ? undraggableColor : initialColor;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// Dont pick invisible items
		if (!CanDrag())
			return;

		dragging = true;

		foreach (SnapArrow snapArrow in FindObjectsOfType<SnapArrow>()) {
			snapArrow.highlight.enabled = snapArrow.forceKind == forceKind;
			snapArrow.highlight.color = initialColor;
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
		if (!dragging)
			return;

		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!dragging)
			return;

		dragging = false;

		foreach (SnapArrow snapArrow in FindObjectsOfType<SnapArrow>()) {
			snapArrow.highlight.enabled = false;
		}

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
		var applyForce = interactable.Type switch
		{
			ObjectType.Ball => ApplyForce(arrow, (Ball)interactable),
			ObjectType.Vehicle => ApplyForce(arrow, (Vehicle)interactable),
			ObjectType.Drone => ApplyForce(arrow, (Drone)interactable),
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

		StartCoroutine(ball.Hit(vectorForce.Force));

		// Delete arrow
		arrow.DeleteArrow();
		return true;
	}

	private bool ApplyForce(SnapArrow arrow, Vehicle vehicle)
	{
		var scalarForce = GetComponent<ScalarForce>();
		if (scalarForce is null) return false;

		vehicle.SetScalarForce(scalarForce.Force);
		arrow.SaveForce();
		return true;
	}
	
	private bool ApplyForce(SnapArrow arrow, Drone drone)
	{
		var vectorForce = GetComponent<VectorForce>();
		if (vectorForce is null) return false;

		drone.SetVectorForce(vectorForce.Force);
		arrow.SaveForce();
		return true;
	}

	private bool CanDrag() {
		return canvasGroup.alpha == 0f || !canDrag || !ball.Animating;
	}
}