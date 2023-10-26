using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropItem : MonoBehaviour, IDropHandler
{
    private Image background;
    private TextMeshProUGUI text;

    public void OnDrop(PointerEventData eventData)
    {
        background = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        // Modify our values
        background.color = eventData.pointerDrag.GetComponent<Image>().color;
        string testText = eventData.pointerDrag.GetComponentInChildren<TextMeshProUGUI>().GetParsedText();
        text.SetText(testText);

        // Change Spring Force value
        float value;
        float.TryParse(testText, out value);

        // TODO: Add object ID
        GameObject.Find("Spring").GetComponentInChildren<Spring>().setSpringForce(value);
    }
}
