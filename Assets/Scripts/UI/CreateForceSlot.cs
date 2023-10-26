using UnityEngine;

public class CreateForceSlot : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private bool isBall = false;

    private GameObject instanciatedObject = null;

    private void Start()
    {
        instanciatedObject = Instantiate(prefab);
        instanciatedObject.transform.SetParent(canvas);

        instanciatedObject.GetComponent<RectTransform>().localScale = Vector3.one;

        DropObject dropObject = instanciatedObject.GetComponent<DropObject>();
        dropObject.setInteractableItem(transform.parent.gameObject);
       
        instanciatedObject.name = transform.parent.name + "_ForceSlot_" + dropObject.getNextID();

        instanciatedObject.transform.position = transform.parent.transform.position;

        if (!isBall)
            instanciatedObject.transform.Translate(new Vector3(1, 1, 0));
    }
}
