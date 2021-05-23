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
    public RawImage Blur;

    public bool SliderActive = false;

    public GameObject infoBackground;
    public GameObject infoStartText;
    public GameObject SpeedometerInfo;
    public GameObject Text1;
    public GameObject Text2;

    public GameObject NextImgButton;
    public GameObject PressSpaceToStart;

    private GameObject currentGameObject;
    private GameObject nextGameObject;

    public float TimeBeetweenVelocityDisplay = 0;

    float alpha = 1.0f;

    private int infoOn = 1;

    public void Start()
    {
        CheckPromille();
        PressSpaceToStart.SetActive(false);
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
        }
    }

    private void CheckPromille()
    {
        alpha = 0;
        TimeBeetweenVelocityDisplay = 0;
        if (StoredVariables.Promille > .3)
            SetPromilleValues(0f, .5f);

        if (StoredVariables.Promille > .6)
            SetPromilleValues(0.1f, .7f);

        if (StoredVariables.Promille > .9)
            SetPromilleValues(0.18f, 1f);

        if (StoredVariables.Promille > 1.3)
            SetPromilleValues(0.23f, 1.7f);

        if (StoredVariables.Promille > 1.6)
            SetPromilleValues(0.45f, 1.9f);

        if (StoredVariables.Promille > 2.1)
            SetPromilleValues(0.66f, 2.7f);

        if (StoredVariables.Promille > 3)
            SetPromilleValues(.9f, 3.7f);
            
        if (StoredVariables.Promille > .2)
        {
            SetAlpha(Blur);
            SetAlpha(Vignett);
        }
    }

    private void SetAlpha(RawImage img)
    {
        Color currentColor = img.color;
        currentColor.a = alpha;
        img.color = currentColor;
    }

    public void SetPromilleValues(float a, float time)
    {
        alpha = a;
        TimeBeetweenVelocityDisplay = time;
    }

    public void FadeOut()
    {
        if(infoOn == 1)
        {
            FadeOutAction(infoStartText);
            nextGameObject = SpeedometerInfo;
        }
        if(infoOn == 2)
        {
            FadeOutAction(SpeedometerInfo);
            nextGameObject = Text1;
        }
        if (infoOn == 3)
        {
            FadeOutAction(Text1);
            nextGameObject = Text2;
        }
        if (infoOn == 4)
            FadeOutAction(Text2);
    }

    public void FadeOutAction(GameObject gameObject)
    {
        NextImgButton.SetActive(false);
        currentGameObject = gameObject;
        LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(-4000f, 0f, 0f), 1f).setEase(LeanTweenType.easeOutCubic).setOnComplete(SetFadedOutObjectDisabled).setIgnoreTimeScale(true);
        if(infoOn == 4)
            StartGame();
    }

    public void SetFadedOutObjectDisabled()
    {
        currentGameObject.SetActive(false);
        if (infoOn == 4)
        {
            StartGame();
        }else
        {
            FadeIn(nextGameObject);
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        infoBackground.SetActive(false);
        SpeedometerInfo.SetActive(false);
        Text1.SetActive(false);
        Text2.SetActive(false);
        StartCoroutine(DisplayVelocity());
    }

    public void FadeIn(GameObject gameOb)
    {
        gameOb.SetActive(true);
        LeanTween.alpha(gameOb, 1f, 1f).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
        infoOn++;
        if(infoOn == 4)
        {
            PressSpaceToStart.SetActive(true);
            NextImgButton.SetActive(false);
        }
        else
            NextImgButton.SetActive(true);
    }

    IEnumerator DisplayVelocity()
    {
        yield return new WaitForSeconds(TimeBeetweenVelocityDisplay);
        SpeedLimiter.text = _carController.CarSpeed;
        StartCoroutine(DisplayVelocity());
    }
}
