using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField]
    private float _springForce;

    void Start()
    {
        
    }

    void Update()
    {
        // TODO: Play anim
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Check if the ball collided ?
        // If not: as everything jumpes off the spring, we can make puzzles more complex

        // prev force
        // Vector2 prevForce = -collision.gameObject.GetComponent<Rigidbody2D>().totalForce;


		collision.rigidbody.AddForce(transform.up * _springForce  , ForceMode2D.Impulse);
    }

    public void setSpringForce(float springForce) { 
        _springForce = springForce;
    }
}
