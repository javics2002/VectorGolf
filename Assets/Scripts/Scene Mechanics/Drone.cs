using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
public class Drone : InteractableObject
{
	[SerializeField]
	private float Speed = 2f;

	private SnapArrow _arrow;
	private Rigidbody2D _rb;
	private float _movementDuration;

	/// <inheritdoc />
	public override ObjectType Type => ObjectType.Drone;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		Assert.IsNotNull(_rb);

		_arrow = GetComponentInChildren<SnapArrow>();
		Assert.IsNotNull(_arrow);
	}

	public void SetVectorForce(Vector2 vector)
	{
		_rb.velocity = vector.normalized * Speed;
		_movementDuration = vector.magnitude / Speed;
		StartCoroutine(nameof(RemoveForce));
	}

	private IEnumerator RemoveForce()
	{
		yield return new WaitForSeconds(_movementDuration);
		_rb.velocity = Vector2.zero;
		
		// TODO: Shrink the vector arrow
		// TODO: Figure out why this doesn't remove the arrow
		_arrow.DeleteArrow();
	}
}