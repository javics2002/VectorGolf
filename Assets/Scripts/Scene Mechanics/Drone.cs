using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
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
	
	[Header("Audio Settings")]
	[SerializeField]
	[Range(0f, 3f)]
	[Tooltip("The maximum pitch for the drone")]
	private float MaximumPitch = 2f;

	[SerializeField]
	[Range(0f, 3f)]
	[Tooltip("The minimum pitch for the drone")]
	private float MinimumPitch = 0.5f;

	[SerializeField]
	[Range(0f, 3f)]
	[Tooltip("The multiplier for the pitch based on the speed of the drone")]
	private float PitchStepMultiplier = 0.1f;

	private static readonly Vector3 OriginCableLeft = new(-1.5f, -0.5f, 0f);
	private static readonly Vector3 OriginCableRight = new(1.5f, -0.5f, 0f);

	private SnapArrow _arrow;
	private Rigidbody2D _rb;
	private AudioSource _audioSource;

	/// <inheritdoc />
	public override ObjectType Type => ObjectType.Drone;

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();

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
		_rb.velocity = vector * Speed;
		_audioSource.pitch = Mathf.Min(MaximumPitch, MinimumPitch + vector.magnitude * PitchStepMultiplier);
		Debug.Log(_audioSource.pitch, this);
	}
}