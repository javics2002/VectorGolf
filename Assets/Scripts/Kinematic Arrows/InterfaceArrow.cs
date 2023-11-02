using UnityEngine;

public class InterfaceArrow : KinematicArrow
{
	[SerializeField]
	private Vector3 _vector = Vector3.zero;

	// TODO: arrow color?

	public void setInterfaceArrow(Vector3 vector)
	{
		_vector = vector;
	}

	protected override Vector3 GetVector() {
		return _vector;
	}
}
