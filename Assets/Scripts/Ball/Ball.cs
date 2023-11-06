using System.Collections;
using UnityEngine;

public class Ball : InteractableObject {
    [Header("Animation times")]
    [SerializeField, Range(0f, 1f)]
    float secondArrowTime;
	[SerializeField, Range(0f, 1f)]
	float resultArrowTime;
	[SerializeField, Range(0f, 1f)]
	float pauseAfterSecond, pauseAfterResult;
	[SerializeField]
	EasingFunctions.InterpolationType secondEasingFunction, resultEasingFunction;

	[Header("References")]
	[SerializeField]
	Transform secondArrowTarget;

	public VelocityArrow velocityArrow {get;set;}
    InterfaceArrow firstVector, secondVector, resultVector;

	new Rigidbody2D rigidbody;
	

	private void Start()
    {
        objectType = ObjectType.BALL;

        rigidbody = GetComponentInParent<Rigidbody2D>();
    }

	public void Hit(Vector2 force) {
		StartCoroutine(AddVectorsAnimation(force, rigidbody.transform, 
			EasingFunctions.GetEasingFunction(secondEasingFunction), EasingFunctions.GetEasingFunction(resultEasingFunction)));
	}

	public void Hit(Vector2 force, Transform secondVectorOrigin) {
        StartCoroutine(AddVectorsAnimation(force, secondVectorOrigin, 
			EasingFunctions.GetEasingFunction(secondEasingFunction), EasingFunctions.GetEasingFunction(resultEasingFunction)));
	}

	IEnumerator AddVectorsAnimation(Vector3 force, Transform secondVectorOrigin, 
		EasingFunctions.Interpolation secondVectorInterpolation, EasingFunctions.Interpolation resultVectorInterpolation) {
        if (velocityArrow.IsLongEnoughToDraw(velocityArrow.GetVector())) {
			Time.timeScale = 0;

			velocityArrow.isVisible = false;

            firstVector = KinematicArrow.CreateArrow<InterfaceArrow>("First Vector", 
                transform, velocityArrow.GetArrowProperties());
            firstVector.SetInterfaceArrow(velocityArrow.GetVector());
			firstVector.isVisible = true;

			yield return MoveSecondVector(force, secondVectorOrigin, secondVectorInterpolation);
			yield return new WaitForSecondsRealtime(pauseAfterSecond);

			yield return GrowResultVector(force, resultVectorInterpolation);
			yield return new WaitForSecondsRealtime(pauseAfterResult);

            Destroy(firstVector.gameObject);
            Destroy(secondVector.gameObject);
            Destroy(resultVector.gameObject);

            velocityArrow.isVisible = true;

			Time.timeScale = 1;
		}

		rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    IEnumerator MoveSecondVector(Vector3 force, Transform secondVectorOrigin, EasingFunctions.Interpolation interpolation) {
		secondVector = KinematicArrow.CreateArrow<InterfaceArrow>("Second Vector",
				transform, velocityArrow.GetArrowProperties());
		secondVector.SetInterfaceArrow(force);
		secondVector.isVisible = true;
		secondVector.target = secondArrowTarget;

		float t = 0;
        while(t < secondArrowTime) {
			secondArrowTarget.position = Vector3.Lerp(secondVectorOrigin.position, 
                transform.position + velocityArrow.GetVector(), interpolation(t / secondArrowTime));

            yield return null;
            t += Time.unscaledDeltaTime;
        }

		secondArrowTarget.position = transform.position + velocityArrow.GetVector();
	}

	IEnumerator GrowResultVector(Vector3 force, EasingFunctions.Interpolation interpolation) {
		resultVector = KinematicArrow.CreateArrow<InterfaceArrow>("Result Vector",
				transform, velocityArrow.GetArrowProperties());
		resultVector.isVisible = true;

		float t = 0;
        while(t < resultArrowTime) {
			resultVector.SetInterfaceArrow(Vector3.Lerp(Vector3.zero, 
                velocityArrow.GetVector() + force, interpolation(t / resultArrowTime)));

		    yield return null;
			t += Time.unscaledDeltaTime;
		}

		resultVector.SetInterfaceArrow(velocityArrow.GetVector() + force);
	}
}
