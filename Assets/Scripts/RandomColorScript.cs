using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorScript : MonoBehaviour
{
    public Renderer renderer;

    public void Awake()
    {
        renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 0.9f);
    }
}
