using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text SpeedLimiter;
    public CarController _carController;

    public void FixedUpdate()
    {
        SpeedLimiter.text = _carController.CarSpeed + " Km/h";
    }
}
