using UnityEngine;

public class VelocityArrow : KinematicArrow
{
	protected override Vector3 GetVector() {
		Vector3 vector = GameManager.Instance.convertToScale(rigidbody.velocity);
        return vector;
	}
}
