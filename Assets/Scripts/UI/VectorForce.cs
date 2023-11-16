using TMPro;
using UnityEngine;

public class VectorForce : MonoBehaviour
{
    private Vector2 _vectorialForce;

    public void SetVectorialForce(Vector2 vectorialForce)
    {
        _vectorialForce = vectorialForce;

        // Parse
        string forceText = "(" + _vectorialForce.x.ToString("0.#") + ", " + _vectorialForce.y.ToString("0.#") + ")";
        GetComponentInChildren<TextMeshProUGUI>().text = forceText;
    }

    public Vector2 GetVectorialForce()
    {
        return _vectorialForce;
    }
}
