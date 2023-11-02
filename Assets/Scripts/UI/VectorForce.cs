using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VectorForce : MonoBehaviour
{
    private Vector2 _vectorialForce;

    public void setVectorialForce(Vector2 vectorialForce)
    {
        _vectorialForce = vectorialForce;

        // Parse
        string forceText = "(" + _vectorialForce.x + ", " + _vectorialForce.y + ")";
        GetComponentInChildren<TextMeshProUGUI>().text = forceText;
    }

    public Vector2 getVectorialForce()
    {
        return _vectorialForce;
    }
}
