using System.Collections;

using UnityEngine;

public class Ball : InteractableObject
{
	public int hits { get; private set; } = 0;
	public bool animating { get; private set; } = false;

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

	public VelocityArrow velocityArrow { get; set; }
	InterfaceArrow firstVector, secondVector, resultVector;

	new Rigidbody2D rigidbody;
	GameManager gameManager;

	private void Start() {
		objectType = ObjectType.BALL;
		rigidbody = GetComponentInParent<Rigidbody2D>();

		gameManager = GameManager.Instance;
	}

	public IEnumerator Hit(Vector2 force)
	{
		hits++;

		if(gameManager.seeAnimations)
			yield return AddVectorsAnimation(force, rigidbody.transform,
				EasingFunctions.GetEasingFunction(secondEasingFunction), EasingFunctions.GetEasingFunction(resultEasingFunction));

		rigidbody.AddForce(force, ForceMode2D.Impulse);
	}

	public IEnumerator Hit(Vector2 force, InterfaceArrow secondArrow) {
		hits++;

		if (!gameManager.seeAnimations) {
			rigidbody.AddForce(force, ForceMode2D.Impulse);
			yield break;
		}

		secondArrow.properties.isVisible = false;

		yield return AddVectorsAnimation(force, secondArrow.transform,
			EasingFunctions.GetEasingFunction(secondEasingFunction), EasingFunctions.GetEasingFunction(resultEasingFunction));

		rigidbody.AddForce(force, ForceMode2D.Impulse);

		//TODO usar alpha para volver a verlo
		secondArrow.properties.isVisible = true;
	}

	IEnumerator AddVectorsAnimation(Vector3 force, Transform secondVectorOrigin,
		EasingFunctions.Interpolation secondVectorInterpolation, EasingFunctions.Interpolation resultVectorInterpolation) {
		if (velocityArrow.IsLongEnoughToDraw(velocityArrow.GetVector())) {
			Time.timeScale = 0;
			animating = true;

			velocityArrow.properties.isVisible = false;

			firstVector = KinematicArrow.CreateArrow<InterfaceArrow>("First Vector",
				transform, velocityArrow.properties);
			firstVector.SetInterfaceArrow(velocityArrow.GetVector());
			firstVector.properties.isVisible = true;

			yield return MoveSecondVector(force, secondVectorOrigin, secondVectorInterpolation);
			yield return new WaitForSecondsRealtime(pauseAfterSecond);

			yield return GrowResultVector(force, resultVectorInterpolation);
			yield return new WaitForSecondsRealtime(pauseAfterResult);

			Destroy(firstVector.gameObject);
			Destroy(secondVector.gameObject);
			Destroy(resultVector.gameObject);

			velocityArrow.properties.isVisible = true;

			animating = false;
			Time.timeScale = 1;
		}
	}

	IEnumerator MoveSecondVector(Vector3 force, Transform secondVectorOrigin, EasingFunctions.Interpolation interpolation) {
		secondVector = KinematicArrow.CreateArrow<InterfaceArrow>("Second Vector",
				transform, velocityArrow.properties);
		secondVector.SetInterfaceArrow(force);
		secondVector.properties.isVisible = true;
		secondVector.target = secondArrowTarget;

		float t = 0;
		while (t < secondArrowTime) {
			secondArrowTarget.position = Vector3.Lerp(secondVectorOrigin.position,
				transform.position + velocityArrow.GetVector(), interpolation(t / secondArrowTime));

			yield return null;
			t += Time.unscaledDeltaTime;
		}

		secondArrowTarget.position = transform.position + velocityArrow.GetVector();
	}

	IEnumerator GrowResultVector(Vector3 force, EasingFunctions.Interpolation interpolation) {
		resultVector = KinematicArrow.CreateArrow<InterfaceArrow>("Result Vector",
				transform, velocityArrow.properties);
		resultVector.properties.isVisible = true;

		float t = 0;
		while (t < resultArrowTime) {
			resultVector.SetInterfaceArrow(Vector3.Lerp(Vector3.zero,
				velocityArrow.GetVector() + force, interpolation(t / resultArrowTime)));

			yield return null;
			t += Time.unscaledDeltaTime;
		}

		resultVector.SetInterfaceArrow(velocityArrow.GetVector() + force);
	}
}
