using UnityEngine;

namespace UI
{
	public class ArrowListUI : MonoBehaviour
	{
		[SerializeField]
		public GameObject Prefab;

		private void Start()
		{
			foreach (var rotation in GameManager.Instance.BallLaunchers)
			{
				var arrow = Instantiate(Prefab, transform).GetComponent<ArrowUI>();
				arrow.Rotation = rotation.Rotation;
				arrow.Force = rotation.Force;
			}
		}
	}
}