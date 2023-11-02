using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapArrow : MonoBehaviour
{
    [SerializeField]
    private InterfaceArrow _interfaceArrow;

    private void Start()
    {
    }
    private void OnMouseOver()
    {
        if (!GameManager.Instance.isDragging && GameManager.Instance.isArrowVisible)
            return;

        switch (GetComponentInParent<InteractableObject>().objectType)
        {
            case InteractableObject.ObjectType.BALL:
                if (GameManager.Instance.draggedObject.GetComponent<VectorForce>())
                {
                    // Show arrow
                    _interfaceArrow = gameObject.AddComponent<InterfaceArrow>();

                    _interfaceArrow.target.position = transform.parent.position;
                    _interfaceArrow.setInterfaceArrow(GameManager.Instance.draggedObject.GetComponent<VectorForce>().getVectorialForce());

                    GameManager.Instance.isArrowVisible = true;
                    //_interfaceArrow.ToggleVisible();

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
                    _interfaceArrow.ToggleVisible();

                    // Visible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
                }
                break;
            case InteractableObject.ObjectType.FAN:
            case InteractableObject.ObjectType.SPRING:
                if (GameManager.Instance.draggedObject.GetComponent<ScalarForce>())
                {
                    // NOT Show arrow
                    _interfaceArrow.ToggleVisible();

                    // Visible
                    GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
                }
                break;
        }
    }
}
