using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Drone : InteractableObject
{
	[SerializeField]
	private float Speed = 2f;

	[SerializeField, Min(0.001f)]
	private float CableWidth = 0.03f;

	[SerializeField]
	private LineRenderer CableLeft;

	[SerializeField]
	private LineRenderer CableRight;

	[FormerlySerializedAs("CableOffset")]
	[SerializeField]
	private Vector3 LeftCableOffset;

	[SerializeField]
	private Vector3 RightCableOffset;

	private static readonly Vector3 OriginCableLeft = new(-1.5f, -0.5f, 0f);
	private static readonly Vector3 OriginCableRight = new(1.5f, -0.5f, 0f);

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

		Assert.IsNotNull(CableLeft);
		Assert.IsNotNull(CableRight);

		CableLeft.startWidth = CableWidth;
		CableLeft.endWidth = CableWidth;
		CableLeft.positionCount = 2;
		CableLeft.SetPosition(0, OriginCableLeft);
		CableLeft.SetPosition(1, LeftCableOffset);

		CableRight.startWidth = CableWidth;
		CableRight.endWidth = CableWidth;
		CableRight.positionCount = 2;
		CableRight.SetPosition(0, OriginCableRight);
		CableRight.SetPosition(1, RightCableOffset);
	}

	private void OnDrawGizmos()
	{
		var tf = transform;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(tf.TransformPoint(OriginCableLeft), tf.TransformPoint(LeftCableOffset));
		Gizmos.DrawLine(tf.TransformPoint(OriginCableRight), tf.TransformPoint(RightCableOffset));
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
		_arrow.DeleteArrow();
	}
}