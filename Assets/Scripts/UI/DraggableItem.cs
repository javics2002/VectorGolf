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
        GameManager.Instance.isDragging = false;
        GameManager.Instance.draggedObject = null;

        // Dont return to the canvas invisible items
        if (canvasGroup.alpha == 0f)
            return;

        canvasGroup.alpha = 1f;

        rectTransform.localScale = Vector3.one;
        rectTransform.anchoredPosition = initialPositon;

        canvasGroup.blocksRaycasts = true;
    }
}
