using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	[RequireComponent(typeof(RectTransform))]
	public class ArrowUI : Button
	{
		[SerializeField]
		private Transform Child;

		[Range(0f, 360f)]
		[Tooltip("The rotation in degrees")]
		public float Rotation;

		[Range(200f, 1000f)]
		[Tooltip("The amount of force to apply to the ball")]
		public float Force = 200f;

		public new void Start()
		{
			Child.Rotate(Vector3.forward, Rotation);
			onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			Destroy(gameObject);
			GameManager.Instance.ApplyForceToBall(Rotation, Force);
		}
	}
}