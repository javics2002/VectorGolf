using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { BALL, SPRING, FAN }
    public ObjectType objectType { get; protected set; }
}
