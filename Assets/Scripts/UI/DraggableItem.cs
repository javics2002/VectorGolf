using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
	[SerializeField, Range(0f, 1f)]
	float scaleWhenDrag;

	private RectTransform rectTransform;
	private Canvas canvas;
	private CanvasGroup canvasGroup;

	private Vector2 initialPositon;

	private void Awake() {
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData) {
		// Dont pick invisible items
		if (canvasGroup.alpha == 0f) {
			return;
		}

		GameManager.Instance.isDragging = true;
		GameManager.Instance.draggedObject = gameObject;

		initialPositon = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);

		canvasGroup.alpha = 0.7f;
		canvasGroup.blocksRaycasts = false;

		rectTransform.localScale = new Vector3(scaleWhenDrag, scaleWhenDrag, scaleWhenDrag);
	}

	public void OnDrag(PointerEventData eventData) {
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData) {
		GameObject mouseOverObject = GameManager.Instance.mouseOverObject;

		// When an object is not dropped inside a valid gameobject
		if (!mouseOverObject) {
			canvasGroup.alpha = 1f;

			rectTransform.localScale = Vector3.one;
			rectTransform.anchoredPosition = initialPositon;

			canvasGroup.blocksRaycasts = true;

			GameManager.Instance.isDragging = false;
			GameManager.Instance.draggedObject = null;

			return;
		}

		bool applyForce = false;
		VectorForce vectorForce = GetComponent<VectorForce>();
		ScalarForce scalarForce = GetComponent<ScalarForce>();
		bool isBall = mouseOverObject.GetComponentInChildren<InteractableObject>().objectType == InteractableObject.ObjectType.BALL;

		// Apply vectorial force
		if (vectorForce && isBall) {
			mouseOverObject.GetComponentInChildren<Ball>().Hit(vectorForce.getVectorialForce());
			applyForce = true;

			// Delete arrow
			mouseOverObject.GetComponentInChildren<SnapArrow>().deleteArrow();
		}
		// Apply scalar force
		else if (scalarForce && !isBall) {
			Spring spring = mouseOverObject.GetComponentInChildren<Spring>();
			Fan fan = mouseOverObject.GetComponentInChildren<Fan>();

			// Check if fan or spring
			if (spring) {
				spring.setSpringForce(scalarForce.getScalarForce());
			}
			else if (fan) {
				fan.setFanForce(scalarForce.getScalarForce());
			}

			applyForce = true;

			SnapArrow snapArrow = mouseOverObject.GetComponentInChildren<SnapArrow>();
			snapArrow.setArrowActive(true);
			snapArrow.setArrowInvisible();
		}

		if (applyForce) {
			GameManager.Instance.isDragging = false;
			GameManager.Instance.draggedObject = null;
			GameManager.Instance.mouseOverObject = null;

			canvasGroup.alpha = 0f;
		}
	}
}
