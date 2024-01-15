using UnityEngine;

public class InterfaceArrow : KinematicArrow
{
	[SerializeField]
	private Vector3 _vector = Vector3.zero;

	public void SetInterfaceArrow(Vector3 vector)
	{
		_vector = vector;
	}

	public override Vector3 GetVector() {
		return _vector;
    }
}
