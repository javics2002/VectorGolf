using UnityEngine;

public class InterfaceArrow : KinematicArrow
{
	[SerializeField]
	private Vector3 _vector = Vector3.zero;

	public void setInterfaceArrow(Vector3 vector)
	{
		_vector = GameManager.Instance.convertToScale(vector);
		lastFrameVector = Vector3.zero;
	}
	protected override Vector3 GetVector() {
		return _vector;
    }
}
