using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallData : MonoBehaviour
{
    Vector3 lastPosition;
    Vector2 deltaPositon;

    void Awake()
    {
        lastPosition = transform.position;
        deltaPositon = Vector2.zero;
    }

    void Update()
    {
        deltaPositon = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
        lastPosition = transform.position;
    }

    public Vector2 getDeltaPosition()
    {
        return deltaPositon;
    }
}
