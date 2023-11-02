using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropForceBall : DropObject, IDropHandler
{
    private RectTransform rectTransform;
    private Ball ballData;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ballData = _interactableObject.GetComponent<Ball>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        VectorForce vectorialForce = eventData.pointerDrag.GetComponent<VectorForce>();
        if (vectorialForce == null)
            return;

		ballData.gameObject.GetComponent<Rigidbody2D>().AddForce(vectorialForce.getVectorialForce(), ForceMode2D.Impulse);

        // On success, make the drag object invisible
        eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 0;
    }

	private void Update() {
		rectTransform.position = ballData.gameObject.transform.position;
	}
}

