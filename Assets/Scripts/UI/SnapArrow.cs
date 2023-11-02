using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapArrow : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (!GameManager.Instance.isDragging)
            return;

        switch (GetComponentInParent<InteractableObject>().objectType)
        {
            case InteractableObject.ObjectType.BALL:
                if (GameManager.Instance.draggedObject.GetComponent<VectorForce>())
                {
                    // Show arrow

                    // Invisible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
                }
                break;
            case InteractableObject.ObjectType.FAN:
            case InteractableObject.ObjectType.SPRING:
                if (GameManager.Instance.draggedObject.GetComponent<ScalarForce>())
                {
                    // Show arrow

                    // Invisible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
                }
                break;
        }
    }
    private void OnMouseExit()
    {
        if (!GameManager.Instance.isDragging)
            return;

        switch (GetComponentInParent<InteractableObject>().objectType)
        {
            case InteractableObject.ObjectType.BALL:
                if (GameManager.Instance.draggedObject.GetComponent<VectorForce>())
                {
                    // NOT Show arrow

                    // Visible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
                }
                break;
            case InteractableObject.ObjectType.FAN:
            case InteractableObject.ObjectType.SPRING:
                if (GameManager.Instance.draggedObject.GetComponent<ScalarForce>())
                {
                    // NOT Show arrow

                    // Visible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
                }
                break;
        }
    }
}
