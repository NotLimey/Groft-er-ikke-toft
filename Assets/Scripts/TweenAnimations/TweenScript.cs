using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TweenScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float TweenTime = 0.05f;
    private AudioSource _onHoverSound;

    public void Start()
    {
        _onHoverSound = gameObject.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onHoverSound.Stop();
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;

        _onHoverSound.Play();
        LeanTween.scale(gameObject, Vector3.one * 1.1f, TweenTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * 1f, TweenTime);
    }
}
