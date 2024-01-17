using System.Collections.Generic;
using UnityEngine;

public static class EnumeratorMethods
{
	public static IEnumerable<float> Lerp(float previous, float next, float time)
	{
		var start = Time.time;
		var end = start + time;
		var now = start;

		while (now < end)
		{
			yield return Mathf.Lerp(previous, next, (now - start) / time);
			now += Time.deltaTime;
		}

		yield return next;
	}
}