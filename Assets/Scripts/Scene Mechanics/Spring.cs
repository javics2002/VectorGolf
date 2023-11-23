using UnityEngine;

public class Spring : InteractableObject
{
    [SerializeField]
    private float _springForce = 1f;

    void Start()
    {
        objectType = ObjectType.Spring;
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

        StartCoroutine(collision.gameObject.GetComponentInChildren<Ball>().Hit(transform.up * _springForce, 
            transform.parent.GetComponentInChildren<SnapArrow>().interfaceArrow));
    }

    public void SetSpringForce(float springForce) { 
        _springForce = springForce;
    }
}
