using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Fan : InteractableObject
{
    [SerializeField]
    private float _fanForce;
    
    private bool _addForceFieldEffect = false;
    private Rigidbody2D _objectRb = null;

    void Start()
    {
        objectType = ObjectType.FAN;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _addForceFieldEffect = true;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
		
		// TODO: every object that enters in the trigger and is detected by ontriggerstay has rigidbody
		// so this is useless?
		
        Assert.IsNotNull(rb, "The object in the fan zone does not have rigidbody");

        _objectRb = rb;
    }

    void FixedUpdate()
    {
        if (_addForceFieldEffect)
        {
            _objectRb.AddForce(transform.up * _fanForce, ForceMode2D.Force);
            _addForceFieldEffect = false;
            _objectRb = null;
        }
    }
}
