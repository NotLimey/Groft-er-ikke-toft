using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredVariables : MonoBehaviour
{
    public static float Promille;
    public static float TimeOfDay;
    public static bool HasPlayed = false;
    public static bool HasCrashed = false;

    public void Setvalue(float promille, float time)
    {
        Promille = promille;
        TimeOfDay = time;
    }
}