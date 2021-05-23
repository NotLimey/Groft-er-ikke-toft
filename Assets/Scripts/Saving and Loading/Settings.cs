using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool HasCrashed;
    public bool HasPlayed;

    public void SaveSettings()
    {
        SavingSystem.SaveSettings(this);
    }

    public void LoadSettings()
    {
        SettingsData data = SavingSystem.LoadSettings();

        HasCrashed = data.HasCrashed;
        HasPlayed = data.HasPlayed;
    }
}
