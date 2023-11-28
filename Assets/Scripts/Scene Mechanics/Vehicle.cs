using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class Vehicle : InteractableObject
{
	public WheelJoint2D BackWheel;
	public WheelJoint2D FrontWheel;

	[Tooltip("The speed multiplier for the motor")]
	public float MotorSpeedMultiplier = -1f;

	[Tooltip("The maximum torque that can be applied to the motor")]
	public float MaximumMotorTorque = 10000f;

	[Tooltip("The force that is applied to the vehicle when a scalar force is applied")]
	public float ForceSpeedMultiplier = 100f;

	[Header("Audio Settings")]
	[Range(0f, 1f)]
	[SerializeField]
	private float MaximumVolume = 0.3f;

	[SerializeField]
	[Min(0f)]
	private float TransitionEngineStart = 1f;

	[SerializeField]
	[Min(0f)]
	private float TransitionEnginePitchChange = 1.5f;

	private JointMotor2D _motor;
	private Rigidbody2D _rb;

	private AudioSource _audioSource;

	/// <inheritdoc />
	public override ObjectType Type => ObjectType.Vehicle;

	private void Awake()
	{
		Assert.IsNotNull(BackWheel);
		Assert.IsNotNull(FrontWheel);
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_audioSource = GetComponent<AudioSource>();
	}

	public void SetScalarForce(float force)
	{
		_motor.motorSpeed = force * MotorSpeedMultiplier;
		_motor.maxMotorTorque = MaximumMotorTorque;

		BackWheel.useMotor = true;
		BackWheel.motor = _motor;
		FrontWheel.useMotor = true;
		FrontWheel.motor = _motor;

		_rb.AddForce(Vector2.right * force * ForceSpeedMultiplier);

		if (!_audioSource.isPlaying)
		{
			StartCoroutine(nameof(LerpEngineVolume));
			_audioSource.Play();
		}

		StartCoroutine(LerpEnginePitch(force * 0.2f));
	}

	private IEnumerator LerpEngineVolume()
	{
		foreach (var value in EnumeratorMethods.Lerp(0f, MaximumVolume, TransitionEngineStart))
		{
			_audioSource.volume = value;
			yield return null;
		}
	}

	private IEnumerator LerpEnginePitch(float next)
	{
		foreach (var value in EnumeratorMethods.Lerp(_audioSource.pitch, next, TransitionEnginePitchChange))
		{
			_audioSource.pitch = value;
			yield return null;
		}
	}
}