using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    protected GameObject _interactableObject = null;

    static int _id = 0;

    public void setInteractableItem(GameObject interactableObject)
    {
        _interactableObject = interactableObject;
        _id++;
    }

    public int getNextID()
    {
        return _id;
    }
}
