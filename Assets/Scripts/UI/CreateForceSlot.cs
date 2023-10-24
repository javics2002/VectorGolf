using UnityEngine;

public class ForceSlot : MonoBehaviour
{
    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private bool isBall = false;

    private GameObject instanciatedObject = null;
    private RectTransform instanciatedObjRT = null;

    private void Start()
    {
        Transform objectPosition = GetComponentInParent<Transform>();

        instanciatedObject = Instantiate(prefab);
        instanciatedObject.transform.SetParent(canvas);

        // TODO: ID?
        instanciatedObject.name = transform.parent.name + "_ForceSlot";

        instanciatedObjRT = instanciatedObject.GetComponent<RectTransform>();
        instanciatedObjRT.anchoredPosition = objectPosition.position * 50;
        instanciatedObjRT.localScale = Vector2.one;

        // Get sprite width and height
        SpriteRenderer springSprite = transform.parent.GetComponentInChildren<SpriteRenderer>();

        if (isBall)
			instanciatedObjRT.position = GameObject.Find("Ball").transform.position;
		else
            instanciatedObjRT.anchoredPosition = new Vector2(instanciatedObjRT.anchoredPosition.x + (springSprite.bounds.size.x * 30), instanciatedObjRT.anchoredPosition.y + (springSprite.bounds.size.y * 90));
    }
}
