using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
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

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // When an object is not dropped inside a valid gameobject
        if (GameManager.Instance.mouseOverObject == null)
        {
            canvasGroup.alpha = 1f;

            rectTransform.localScale = Vector3.one;
            rectTransform.anchoredPosition = initialPositon;

            canvasGroup.blocksRaycasts = true;

            GameManager.Instance.isDragging = false;
            GameManager.Instance.draggedObject = null;
        }
        else
        {
            bool applyForce = false;
            // Apply vectorial force
            if (GetComponent<VectorForce>() != null && GameManager.Instance.mouseOverObject.GetComponentInChildren<InteractableObject>().objectType == InteractableObject.ObjectType.BALL)
            {
                GameManager.Instance.mouseOverObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<VectorForce>().getVectorialForce(), ForceMode2D.Impulse);
                applyForce = true;

                // Delete arrow
                GameManager.Instance.mouseOverObject.GetComponentInChildren<SnapArrow>().deleteArrow();
            }
            // Apply scalar force
            else if (GetComponent<ScalarForce>() && GameManager.Instance.mouseOverObject.GetComponentInChildren<InteractableObject>().objectType != InteractableObject.ObjectType.BALL)
            {
                // Check if fan or spring
                if (GameManager.Instance.mouseOverObject.GetComponentInChildren<Spring>())
                {
                    GameManager.Instance.mouseOverObject.GetComponentInChildren<Spring>().setSpringForce(GetComponent<ScalarForce>().getScalarForce());
                }
                else
                {
                    GameManager.Instance.mouseOverObject.GetComponentInChildren<Fan>().setFanForce(GetComponent<ScalarForce>().getScalarForce());
                }
               
                applyForce = true;

                GameManager.Instance.mouseOverObject.GetComponentInChildren<SnapArrow>().setArrowActive(true);
                GameManager.Instance.mouseOverObject.GetComponentInChildren<SnapArrow>().setArrowInvisible();
            }

            if (applyForce)
            {
                GameManager.Instance.isDragging = false;
                GameManager.Instance.draggedObject = null;
                GameManager.Instance.mouseOverObject = null;

                canvasGroup.alpha = 0f;
            }
        }
    }
}
