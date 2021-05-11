using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public Slider PromilleSlider;
    public Slider TimeOfDaySlider;

    public Canvas MainMenu;
    public Canvas StartCanvas;
    public Canvas Settings;

    public VideoPlayer MyVideoPlayer;

    public void Start()
    {

        if(StoredVariables.HasPlayed) 
        {
            MainMenu.gameObject.SetActive(true);
            StartCanvas.gameObject.SetActive(false);
            Settings.gameObject.SetActive(false);
        }else
        {
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        MyVideoPlayer.Play();
        StartCoroutine(StartFirstScene());
    }

    public void StartGame()
    {
        StoredVariables storedVariables = this.GetComponent<StoredVariables>();
        storedVariables.Setvalue(PromilleSlider.value, TimeOfDaySlider.value);
        SceneManager.LoadSceneAsync(1);
    }

    public void GoToStart()
    {
        MainMenu.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(true);
        Settings.gameObject.SetActive(false);
    }

    public void GoToMainMenu()
    {
        MainMenu.gameObject.SetActive(true);
        StartCanvas.gameObject.SetActive(false);
        Settings.gameObject.SetActive(false);
    }

    public void GoToSettings()
    {
        MainMenu.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        Settings.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator StartFirstScene()
    {
        yield return new WaitForSeconds(10);
        StoredVariables.Promille = 2;
        SceneManager.LoadScene(1);
    }
}
