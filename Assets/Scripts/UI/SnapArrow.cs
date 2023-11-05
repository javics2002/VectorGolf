using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SnapArrow : MonoBehaviour
{
    [SerializeField]
    private InterfaceArrow _interfaceArrow;

    [SerializeField]
    private float _lastScalarForce;

    [SerializeField]
    KinematicArrow.ArrowProperties arrowProperties;

    // For fan/springs
    [SerializeField]
    private bool _isArrowActive;

    // For fan/springs
    [SerializeField]
    private bool _isArrowVisible;

	private void Start() {
	}

	private void OnMouseEnter()
    {
        if (!GameManager.Instance.isDragging)
            return;

        // Ball
        if (GameManager.Instance.draggedObject.GetComponent<VectorForce>()
            && transform.parent.gameObject.GetComponentInChildren<InteractableObject>().objectType == InteractableObject.ObjectType.BALL)
        {
            // Show arrow
            createVectorArrow();

            // Invisible
            GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
        }

        // Spring/Fan
        else if (GameManager.Instance.draggedObject.GetComponent<ScalarForce>() 
            && transform.parent.gameObject.GetComponentInChildren<InteractableObject>().objectType != InteractableObject.ObjectType.BALL)
        {
            // Show arrow
            createScalarArrow();

            // Invisible
            GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.Instance.isDragging)
            return;

        if (transform.parent.GetComponentInChildren<InteractableObject>().objectType == InteractableObject.ObjectType.BALL &&
            GameManager.Instance.draggedObject.GetComponent<VectorForce>() && _isArrowVisible)
        {
            deleteArrow();
            GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else if (transform.parent.GetComponentInChildren<InteractableObject>().objectType != InteractableObject.ObjectType.BALL &&
            GameManager.Instance.draggedObject.GetComponent<ScalarForce>() && _isArrowVisible)
        {
            deleteArrow();
            GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
        }

    }

    public void createVectorArrow()
    {
        // Reference
        if (_interfaceArrow != null && !_interfaceArrow.enabled)
            _interfaceArrow.enabled = true;
        else
            _interfaceArrow = transform.gameObject.AddComponent<InterfaceArrow>();

        // Properties
        _interfaceArrow.target = transform.parent;
        _interfaceArrow.isVisible = arrowProperties.visible;
        _interfaceArrow.SetForceSmooth(arrowProperties.forceSmooth);

        _interfaceArrow.color = arrowProperties.color;
        _interfaceArrow.stemWidth = arrowProperties.stemWidth;
        _interfaceArrow.tipLength = arrowProperties.tipLength;
        _interfaceArrow.tipWidth = arrowProperties.tipWidth;
        _interfaceArrow.priority = arrowProperties.priority;

        // Behaviour
        _interfaceArrow.SetInterfaceArrow(GameManager.Instance.draggedObject.GetComponent<VectorForce>().getVectorialForce());

        _isArrowVisible = true;
        GameManager.Instance.mouseOverObject = transform.parent.gameObject;
    }

    public void createScalarArrow()
    {
        // Reference
        if (_interfaceArrow != null && !_interfaceArrow.enabled)
            _interfaceArrow.enabled = true;
        else
            _interfaceArrow = transform.gameObject.AddComponent<InterfaceArrow>();

        // Properties
        _interfaceArrow.target = transform.parent;
        _interfaceArrow.isVisible = arrowProperties.visible;
        _interfaceArrow.SetForceSmooth(arrowProperties.forceSmooth);

        _interfaceArrow.color = arrowProperties.color;
        _interfaceArrow.stemWidth = arrowProperties.stemWidth;
        _interfaceArrow.tipLength = arrowProperties.tipLength;
        _interfaceArrow.tipWidth = arrowProperties.tipWidth;
        _interfaceArrow.priority = arrowProperties.priority;

        // Behaviour
        _interfaceArrow.SetInterfaceArrow(transform.parent.up * GameManager.Instance.draggedObject.GetComponent<ScalarForce>().getScalarForce());

        _isArrowVisible = true;
        GameManager.Instance.mouseOverObject = transform.parent.gameObject;
    }

    public void deleteArrow()
    {
        if (_isArrowActive)
        {
            _interfaceArrow.SetInterfaceArrow(transform.parent.up * _lastScalarForce);
        }
        else
        {
            GetComponent<MeshFilter>().mesh = null;
            _interfaceArrow.SetInterfaceArrow(Vector3.zero);
            _interfaceArrow.enabled = false;   
        }
        _isArrowVisible = false;
        GameManager.Instance.mouseOverObject = null;
    }

    public void setArrowActive(bool active)
    {
        _isArrowActive = active;
        _lastScalarForce = GameManager.Instance.draggedObject.GetComponent<ScalarForce>().getScalarForce();
    }

    public void setArrowInvisible()
    {
        _isArrowVisible = false;
    }
}
