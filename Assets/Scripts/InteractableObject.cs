using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { Ball, Spring, Fan, Vehicle }
    public ObjectType objectType { get; protected set; }
}
