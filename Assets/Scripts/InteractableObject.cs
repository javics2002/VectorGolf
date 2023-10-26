using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public enum ObjectType { BALL, SPRING, FAN }
    public ObjectType objectType;

    public void addForce(Vector2 force)
    {
        //switch(objectType)
        //{
        //    case ObjectType.BALL:
                
        //    break;
        //    case ObjectType.SPRING:

        //    break;
        //    case ObjectType.FAN:

        //    break;
        //}
    }
}
