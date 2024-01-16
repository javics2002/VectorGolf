using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableVehicleCollider : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D boxRb;

    private void Update()
    {
        if (boxRb.velocity.y < -0.8)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}
