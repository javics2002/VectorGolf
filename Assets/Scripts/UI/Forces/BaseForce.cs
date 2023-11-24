using UnityEngine;

/// <summary>
/// A structure that represents a force.
/// </summary>
/// <typeparam name="T">The force the object can hold and apply.</typeparam>
public abstract class BaseForce<T> : MonoBehaviour
{
	protected T force;

	/// <summary>
	/// Sets and gets the force for this object.
	/// </summary>
	public abstract T Force { get; set; }

	/// <summary>
	/// Gets the kind of force this object is.
	/// </summary>
	public abstract ForceKind Kind { get; }
}