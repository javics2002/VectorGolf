using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Fan : InteractableObject
{
    [SerializeField]
    private float _fanForce;
    
    void Start()
    {
        objectType = ObjectType.FAN;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * _fanForce, ForceMode2D.Force);
    }

    public void setFanForce(float fanForce)
    {
        _fanForce = fanForce;
    }
}
