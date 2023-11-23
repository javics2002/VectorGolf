using UnityEngine;
using UnityEngine.Assertions;

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

	private JointMotor2D _motor;
	private Rigidbody2D _rb;

	private void Awake()
	{
		Assert.IsNotNull(BackWheel);
		Assert.IsNotNull(FrontWheel);

		objectType = ObjectType.Vehicle;
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
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
	}
}