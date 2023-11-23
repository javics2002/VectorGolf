using System.Collections;
using UnityEngine;

public class SnapArrow : MonoBehaviour
{
    [SerializeField]
    KinematicArrow.ArrowProperties arrowProperties;

    [SerializeField, Range(0, 360)]
    private float VectorDirection = 0f;

    [SerializeField, Range(0f, 1f)]
    float animationTime;
    [SerializeField]
    EasingFunctions.InterpolationType interpolationType;

    public InterfaceArrow interfaceArrow { get; private set; }
    private Vector2 currentForce, lastForce;

	private void Awake() {
		interfaceArrow = new GameObject(gameObject.name + " snap arrow").AddComponent<InterfaceArrow>();
		interfaceArrow.properties = arrowProperties;
		interfaceArrow.target = transform.parent;
		interfaceArrow.gameObject.layer = LayerMask.NameToLayer("UI");
        interfaceArrow.canDecomposite = false;

        //TODO poner valor inicial de la flecha
        currentForce = Vector2.zero;
        lastForce = Vector2.zero;
	}

	private void OnMouseEnter()
    {
        if (!GameManager.Instance.isDragging)
            return;

		GameObject draggedObject = GameManager.Instance.draggedObject;
		InteractableObject.ObjectType interactableObjectType = 
            transform.parent.gameObject.GetComponentInChildren<InteractableObject>().objectType;

		VectorForce vectorForce = draggedObject.GetComponent<VectorForce>();
		ScalarForce scalarForce = draggedObject.GetComponent<ScalarForce>();
		// Ball
		if (vectorForce && interactableObjectType == InteractableObject.ObjectType.Ball)
        {
            // Show arrow
            CreateVectorArrow(vectorForce.GetVectorialForce());

			// Invisible
			draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
        }
        // Spring/Fan
        else if (scalarForce && interactableObjectType != InteractableObject.ObjectType.Ball)
        {
            // Show arrow
            CreateScalarArrow(scalarForce.GetScalarForce());

			// Invisible
			draggedObject.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    private void OnMouseExit()
    {
        if (!GameManager.Instance.isDragging)
            return;

		GameObject draggedObject = GameManager.Instance.draggedObject;
		InteractableObject.ObjectType interactableObjectType = 
            transform.parent.GetComponentInChildren<InteractableObject>().objectType;
		if (interactableObjectType == InteractableObject.ObjectType.Ball &&
			draggedObject.GetComponent<VectorForce>() && interfaceArrow.properties.isVisible
            || interactableObjectType != InteractableObject.ObjectType.Ball &&
			draggedObject.GetComponent<ScalarForce>() && interfaceArrow.properties.isVisible)
        {
			ReturnToOldArrow();
		}
    }

    public void CreateVectorArrow(Vector2 force)
    {
        interfaceArrow.properties.isVisible = true;

        currentForce = force;
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(currentForce, animationTime, 
            EasingFunctions.GetEasingFunction(interpolationType)));

        GameManager.Instance.mouseOverObject = transform.parent.gameObject;
    }

    public void CreateScalarArrow(float force)
    {
		interfaceArrow.properties.isVisible = true;

        currentForce = GetTransformedVector() * force;
        StopAllCoroutines();
		StartCoroutine(AnimateArrow(currentForce, animationTime, 
            EasingFunctions.GetEasingFunction(interpolationType)));

        GameManager.Instance.mouseOverObject = transform.parent.gameObject;
    }

    public void ReturnToOldArrow()
    {
		StopAllCoroutines();
		StartCoroutine(AnimateArrow(lastForce, animationTime,
			EasingFunctions.GetEasingFunction(interpolationType)));

		GameManager.Instance.mouseOverObject = null;
		GameManager.Instance.draggedObject.GetComponent<CanvasGroup>().alpha = 1f;
	}

	public void DeleteArrow() {
        interfaceArrow.properties.isVisible = false;
		GameManager.Instance.mouseOverObject = null;
	}

	public void SaveForce() {
        lastForce = currentForce;
	}

	IEnumerator AnimateArrow(Vector2 newVector, float animationTime, EasingFunctions.Interpolation interpolation) {
        Vector2 initialVector = interfaceArrow.GetVector();

		float t = 0;
		while (t < animationTime) {
            interfaceArrow.SetInterfaceArrow(Vector3.Lerp(initialVector,
                newVector, interpolation(t / animationTime)));

			yield return null;
			t += Time.unscaledDeltaTime;
		}

		interfaceArrow.SetInterfaceArrow(newVector);
	}
	
	private Vector2 GetTransformedVector() {
		// Rotate the vector by the parent's rotation:
		//      0º
		// 90º  x   270º
		//     180º
		//
		// We use Quaternion.AngleAxis to rotate the vector and then we
		// multiply it by the parent's up vector to get the final vector:
		return Quaternion.AngleAxis(VectorDirection, Vector3.forward) * transform.parent.up;
	}
}
