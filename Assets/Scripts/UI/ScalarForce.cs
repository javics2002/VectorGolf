using TMPro;
using UnityEngine;

public class ScalarForce : MonoBehaviour
{
    private float _scalarForce;

    public void SetScalarForce(float scalarForce)
    {
        _scalarForce = scalarForce;
        GetComponentInChildren<TextMeshProUGUI>().text = _scalarForce.ToString("0.#");
    }

    public float GetScalarForce()
    {
        return _scalarForce;
    }
}
