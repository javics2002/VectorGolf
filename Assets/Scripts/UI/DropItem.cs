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
        _background = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        // Modify our values
        _background.color = eventData.pointerDrag.GetComponent<Image>().color;
        string testText = eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().GetParsedText();
        _text.SetText(testText);

        // Change Spring Force value
        float value;
        float.TryParse(testText, out value);
        
        if (_interactableObject != null)
            _interactableObject.GetComponentInChildren<Spring>().setSpringForce(value);
    }
}
