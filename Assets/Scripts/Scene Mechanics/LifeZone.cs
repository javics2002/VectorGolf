using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class LifeZone : MonoBehaviour
{
	private static readonly Vector3 ViewpointPointA = new(0f, 0f, 0f);
	private static readonly Vector3 ViewpointPointB = new(0.825f, 1f, 0f);

	[SerializeField]
	private Camera Camera;

	private BoxCollider2D _collider;
	private Vector3 Size { get; set; }
	private Vector3 Position { get; set; }

	public bool _enabled = true;

	private void Start()
	{
		_collider = GetComponent<BoxCollider2D>();

		Assert.IsNotNull(Camera);
		Assert.IsNotNull(_collider);

		// Otherwise, enable the collider and set its size and position:
		var pointA = Camera.ViewportToWorldPoint(ViewpointPointA) - Vector3.one;
		var pointB = Camera.ViewportToWorldPoint(ViewpointPointB) + Vector3.one;

		Size = pointB - pointA;
		Position = pointA + Size / 2f;

		_collider.size = Size;
		_collider.offset = Position;
		_collider.enabled = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		var scene = SceneManager.GetActiveScene();
		if (!scene.isLoaded) return;

		if (!_enabled) return;

		Debug.Log("Player left the visible area! Restarting level...", this);
		GameManager.Instance.ChangeScene((GameScene.Id)scene.buildIndex);
	}
}