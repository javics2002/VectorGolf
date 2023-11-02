using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropItem : DropObject, IDropHandler
{
    private Image _background;
    private TextMeshProUGUI _text;

    public void OnDrop(PointerEventData eventData)
    {
        VectorForce vectorialForce = eventData.pointerDrag.GetComponent<VectorForce>();
        if (vectorialForce != null)
            // Not valid, return eventData.pointerDrag object
            return;

        _background = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        // Modify force slot values
        string testText = eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().GetParsedText();
        _text.SetText(testText);

        _background.color = eventData.pointerDrag.GetComponent<Image>().color;

        ScalarForce scalarForce = eventData.pointerDrag.GetComponent<ScalarForce>();
        if (_interactableObject != null)
            _interactableObject.GetComponentInChildren<Spring>().setSpringForce(scalarForce.getScalarForce());

        // On success, make the drag object invisible
        eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 0;
    }
}
