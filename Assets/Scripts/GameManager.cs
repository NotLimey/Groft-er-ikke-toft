using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text SpeedLimiter;
    public CarController _carController;
    public StoredVariables StoredV;
    public RawImage Vignett;
    public Image Blur;

    public bool SliderActive = false;

    public GameObject infoBackground;
    public GameObject infoStartText;
    public GameObject SpeedometerInfo;
    public GameObject Text1;
    public GameObject Text2;

    public GameObject NextImgButton;

    public float TimeBeetweenVelocityDisplay = 0;

    float alpha = 1.0f;

    private int infoOn = 1;

    public void Start()
    {
        CheckPromille();
        if(!StoredVariables.HasPlayed)
        {
            Time.timeScale = 0;
            infoBackground.SetActive(true);
            infoStartText.SetActive(true);
            NextImgButton.SetActive(true);
            SpeedometerInfo.SetActive(false);
            Text1.SetActive(false);
            Text2.SetActive(false);
            SliderActive = true;
        }else
        {
            Time.timeScale = 1;
            infoBackground.SetActive(false);
            SpeedometerInfo.SetActive(false);
            Text1.SetActive(false);
            Text2.SetActive(false);
            StartCoroutine(DisplayVelocity());
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && SliderActive)
        {
            FadeOut();
            Debug.Log("Yes");
        }
    }

    private void CheckPromille()
    {
        alpha = 0;
        TimeBeetweenVelocityDisplay = 0;
        if (StoredVariables.Promille > .3)
        {
            alpha = 0f;
            TimeBeetweenVelocityDisplay = .5f;
        }

        if (StoredVariables.Promille > .6)
        {
            alpha = 0.1f;
            TimeBeetweenVelocityDisplay = .7f;
        }

        if (StoredVariables.Promille > .9)
        {
            alpha = 0.18f;
            TimeBeetweenVelocityDisplay = 1f;
        }

        if (StoredVariables.Promille > 1.3)
        {
            alpha = 0.23f;
            TimeBeetweenVelocityDisplay = 1.7f;
        }

        if (StoredVariables.Promille > 1.6)
        {
            alpha = 0.45f;
            TimeBeetweenVelocityDisplay = 1.9f;
        }

        if (StoredVariables.Promille > 2.1)
        {
            alpha = 0.66f;
            TimeBeetweenVelocityDisplay = 2.7f;
        }

        if (StoredVariables.Promille > 3)
        {
            alpha = 0.90f;
            TimeBeetweenVelocityDisplay = 3.7f;
        }
            
        if (StoredVariables.Promille > .2)
        {
            Color currentColor = Vignett.color;
            currentColor.a = alpha;
            Vignett.color = currentColor;

            Color currentColor2 = Blur.color;
            currentColor2.a = alpha;
            Blur.color = currentColor2;
        }
    }

    public void FadeOut()
    {
        
    }

    private void StartGame()
    {
        throw new NotImplementedException();
    }

    public void FadeIn()
    {
        
    }

    IEnumerator DisplayVelocity()
    {
        yield return new WaitForSeconds(TimeBeetweenVelocityDisplay);
        SpeedLimiter.text = _carController.CarSpeed;
        StartCoroutine(DisplayVelocity());
    }
}
