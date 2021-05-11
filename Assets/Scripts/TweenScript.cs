using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScript : MonoBehaviour
{
    public float TweenTime;

    void OnMouseOver()
    {
        Debug.Log("Over");
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * 1.2f, TweenTime);
    }

    void OnMouseExit()
    {
        Debug.Log("Exit");
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * 1f, .1f);
    }
}
