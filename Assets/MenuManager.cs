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
    public Canvas Tutorial;

    public VideoPlayer MyVideoPlayer;

    public void Start()
    {

        if(StoredVariables.HasPlayed) 
        {
            MainMenu.gameObject.SetActive(true);
            StartCanvas.gameObject.SetActive(false);
            Settings.gameObject.SetActive(false);
            Tutorial.gameObject.SetActive(false);
        }
        else
        {
            MainMenu.gameObject.SetActive(false);
            StartCanvas.gameObject.SetActive(false);
            Settings.gameObject.SetActive(false);
            Tutorial.gameObject.SetActive(false);
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
        SceneManager.LoadSceneAsync(2);
    }

    public void StartFirstGame()
    {
        StoredVariables storedVariables = this.GetComponent<StoredVariables>();
        storedVariables.Setvalue(2, 12);
        SceneManager.LoadSceneAsync(2);
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
        MyVideoPlayer.Stop();
        StoredVariables.Promille = 2;
        StoredVariables.HasPlayed = true;
        LoadTutorial();
    }

    private void LoadTutorial()
    {
        MainMenu.gameObject.SetActive(false);
        StartCanvas.gameObject.SetActive(false);
        Settings.gameObject.SetActive(false);
        Tutorial.gameObject.SetActive(true);
    }
}
