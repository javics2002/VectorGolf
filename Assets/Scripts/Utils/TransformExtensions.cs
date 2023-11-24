using System;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess
public static class TransformExtensions
{
	public static Vector2 ToDirection(this Transform transform, VectorDirection direction)
	{
		return direction switch
		{
			VectorDirection.Up => transform.up,
			VectorDirection.Down => -transform.up,
			VectorDirection.Left => -transform.right,
			VectorDirection.Right => transform.right,
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}
}