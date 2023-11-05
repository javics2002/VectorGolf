using UnityEngine;

public class VelocityArrow : KinematicArrow
{
	public override Vector3 GetVector() {
        //return GameManager.Instance.convertToScale(rigidbody.velocity);
        return rigidbody.velocity;
	}
}
