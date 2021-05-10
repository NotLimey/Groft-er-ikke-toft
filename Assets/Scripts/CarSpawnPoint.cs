using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnPoint : MonoBehaviour
{
    public Transform Car;
    public float SpaceBeetweenCars;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, SpaceBeetweenCars + Car.position.z);
    }
}
