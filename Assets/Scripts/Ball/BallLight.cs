using UnityEngine;

public class BallLight : MonoBehaviour
{
	private Transform _target;

	private void Start()
	{
		_target = GameObject.Find("Ball").transform;
	}

	private void Update()
	{
		transform.position = _target.position;
	}
}