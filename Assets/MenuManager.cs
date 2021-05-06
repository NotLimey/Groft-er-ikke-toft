using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Slider PromilleSlider;
    public Slider TimeOfDaySlider;

    public void StartGame()
    {
        StoredVariables storedVariables = this.GetComponent<StoredVariables>();
        storedVariables.Setvalue(PromilleSlider.value, TimeOfDaySlider.value);
        SceneManager.LoadSceneAsync(1);
    }
}
