using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScalarForce : MonoBehaviour
{
    private int _scalarForce;

    public void setScalarForce(int scalarForce)
    {
        _scalarForce = scalarForce;
        GetComponentInChildren<TextMeshProUGUI>().text = _scalarForce.ToString();
    }

    public int getScalarForce()
    {
        return _scalarForce;
    }
}
