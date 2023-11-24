using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallControl : MonoBehaviour
{
	private Rigidbody2D _rb;

	[SerializeField, Range(.1f, 10f)]
	private float speed;

#if UNITY_EDITOR
	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		_rb.velocity = new Vector2(Input.GetAxis("Horizontal"), _rb.velocity.y) * speed;
	}
#endif
}