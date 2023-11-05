using UnityEngine;

public class VelocityArrow : KinematicArrow
{
	protected override Vector3 GetVector() {
        return GameManager.Instance.convertToScale(rigidbody.velocity);
	}
}
