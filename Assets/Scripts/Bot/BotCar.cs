using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCar : MonoBehaviour
{
    public int constantspeed;

    public Rigidbody Rb;
    private Transform Car;

    private bool _haveCrashed = false;

    public void Start()
    {
        Rb.useGravity = false;
        Car = GameObject.Find("Car").GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if(!_haveCrashed)
        {
            Vector3 v = Rb.velocity;
            v.z = constantspeed;
            Rb.velocity = -v;
        }

        CheckPositionToCar();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "BotCar")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Car")
        {
            Rb.drag = 10;
            Rb.useGravity = true;
            _haveCrashed = true;
        }
    }

    private void CheckPositionToCar()
    {
        if (transform.rotation.y != 180)
        {
            var y = transform.rotation.y;
            transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, 0);
        }
        if(transform.position.z < (Car.position.z -200) || transform.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
}
