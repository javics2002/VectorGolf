using System;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
	[Tooltip("The ball launchers for this level")]
	[SerializeField]
	public BallLaunch[] BallLaunchers;

	private GameObject _player;
	private Rigidbody2D _playerRigidBody;

    /// <summary>
    ///     The GameManager instance for the game
    /// </summary>
    public static GameManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		Instance.Init();
	}

    /// <summary>
    ///     A function called by <see cref="Awake" /> on the current <see cref="GameManager" /> instance, for setting up the
    ///     data on scene load.
    /// </summary>
    private void Init()
	{
		_player = GameObject.FindWithTag("Player");
		Assert.IsNotNull(_player, "Could not find a Player");

		_playerRigidBody = _player.GetComponent<Rigidbody2D>();
		Assert.IsNotNull(_playerRigidBody, "Could not find RigidBody2D in Player");
	}

    /// <summary>
    ///     Applies a force to the ball given a <paramref name="rotation" /> in degrees.
    /// </summary>
    /// <param name="rotation">The amount of degrees</param>
    /// <param name="force">The amount of force that should be applied</param>
    public void ApplyForceToBall(float rotation, float force)
	{
		var radians = rotation * Mathf.Deg2Rad;
		_playerRigidBody.AddForce(new Vector2(Mathf.Cos(radians) * force, Mathf.Sin(radians) * force));
	}

	[Serializable]
	public struct BallLaunch
	{
		[Range(0f, 360f)]
		[Tooltip("The rotation in degrees")]
		public float Rotation;

		[Range(200f, 1000f)]
		[Tooltip("The amount of force to apply to the ball")]
		public float Force;
	}
}