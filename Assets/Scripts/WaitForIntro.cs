using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForIntro : MonoBehaviour
{
    public float Time;

    public AudioSource Audio;

    void Start()
    {
        StartCoroutine(Wait_For_Intro());
        Audio.Play();
    }

    IEnumerator Wait_For_Intro()
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadSceneAsync(1);

    }
}
