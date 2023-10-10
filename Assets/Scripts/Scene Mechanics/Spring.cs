using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))] - ?
public class Spring : MonoBehaviour
{
    [SerializeField]
    private float springForce;

    [SerializeField]
    private float springRotation;


    void Start()
    {
        
    }

    void Update()
    {
        // TODO: Play anim
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Check if the ball collided
        float radians = springRotation * Mathf.Deg2Rad;
        collision.rigidbody.AddForce(new Vector2(Mathf.Cos(radians) * springForce, Mathf.Sin(radians) * springForce), ForceMode2D.Impulse);
    }
}
