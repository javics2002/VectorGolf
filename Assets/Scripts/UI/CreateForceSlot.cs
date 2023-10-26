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

    private void Start()
    {
        Transform objectPosition = GetComponentInParent<Transform>();

        instanciatedObject = Instantiate(prefab);
        instanciatedObject.transform.SetParent(canvas);

        instanciatedObject.GetComponent<RectTransform>().localScale = Vector3.one;

        // TODO: ID?
        instanciatedObject.name = transform.parent.name + "_ForceSlot";

        if (isBall)
            instanciatedObject.transform.position = GameObject.Find("Ball").transform.position;
        else
        {
            instanciatedObject.transform.position = GameObject.Find("Spring").transform.position;
            instanciatedObject.transform.Translate(new Vector3(1, 1, 0));
        }
    }
}
