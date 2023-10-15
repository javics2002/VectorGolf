using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    new Rigidbody2D rigidbody;

	[SerializeField, Range(.1f, 10f)]
	float speed;

#if UNITY_EDITOR
	private void Start() {
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), rigidbody.velocity.y, 0) * speed;
	}
#endif
}
