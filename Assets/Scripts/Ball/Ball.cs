using System.Collections;

using UnityEngine;

public class Ball : InteractableObject {
    new Rigidbody2D rigidbody;

    public VelocityArrow velocityArrow {get;set;}

    private void Start()
    {
        objectType = ObjectType.BALL;

        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

    public void Hit(Vector2 force) {
        StartCoroutine(AddVectorsAnimation(force));
	}

    IEnumerator AddVectorsAnimation(Vector2 force) {
        Time.timeScale = 0;

        if (velocityArrow.isVisible) {
            velocityArrow.isVisible = false;



			yield return ShowSecondVector();
			yield return ShowResultVector();

            velocityArrow.isVisible = true;
		}
        
		rigidbody.AddForce(force, ForceMode2D.Impulse);

		Time.timeScale = 1;
    }

    IEnumerator ShowSecondVector() {
        yield return null;
    }

	IEnumerator ShowResultVector() {
		yield return null;
	}
}
