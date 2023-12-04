using System;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

/// <summary>
/// A static class that provides extension methods for the Transform class.
/// </summary>
public static class TransformExtensions
{
	/// <summary>
	/// Converts a Transform's direction to a Vector2 based on the provided VectorDirection.
	/// </summary>
	/// <param name="transform">The Transform instance on which the extension method is invoked.</param>
	/// <param name="direction">The direction to which the Transform's direction should be converted.</param>
	/// <returns>A Vector2 representing the direction of the Transform.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Thrown when an unsupported VectorDirection is provided.</exception>
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