using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Ball, Vehicle, Drone }
    
    /// <summary>
    /// The type of the object, used to identify the object.
    /// </summary>
    public abstract ObjectType Type { get; }
}
