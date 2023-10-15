using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DropForceBall : MonoBehaviour, IDropHandler
{
    private Image background;

    public void OnDrop(PointerEventData eventData)
    {
        background = GetComponentInChildren<Image>();

        // Modify our values
        background.color = eventData.pointerPress.GetComponent<Image>().color;
        string testText = eventData.pointerPress.GetComponentInChildren<TextMeshProUGUI>().GetParsedText();

        // Parse String to Vector2
        testText = testText.Substring(1, testText.Length - 2);
        string[] forces = testText.Split(',');

        float valueX, valueY;
        float.TryParse(forces[0], out valueX);
        float.TryParse(forces[1], out valueY);

        Vector2 force = new Vector2(valueX, valueY);

        // TODO: Add object ID
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}

