using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool HasPlayed;
    public bool HasCrashed;

    public SettingsData (Settings settings)
    {
        HasPlayed = settings.HasPlayed;
        HasCrashed = settings.HasCrashed;
    }
}
