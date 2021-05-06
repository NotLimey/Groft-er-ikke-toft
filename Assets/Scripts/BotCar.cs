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
            v.x = constantspeed;
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
        if(transform.position.x < (Car.position.x -200) || transform.position.y < 0 || transform.position.z > 5)
        {
            Destroy(gameObject);
        }
    }
}
