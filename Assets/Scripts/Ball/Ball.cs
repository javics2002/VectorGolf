using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : InteractableObject
{
    Vector3 lastPosition;
    Vector2 deltaPositon;

    void Awake()
    {
        lastPosition = transform.position;
        deltaPositon = Vector2.zero;
    }

    private void Start()
    {
        objectType = ObjectType.BALL;
    }

    void FixedUpdate()
    {
        deltaPositon = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
        lastPosition = transform.position;
    }

    public Vector3 getDeltaPosition()
    {
        return new Vector3(deltaPositon.x, deltaPositon.y, 0);
    }
}
