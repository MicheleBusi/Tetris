using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static ColorSettings Color = default;

    public bool MusicOn = false;

    private void Awake()
    {
        if (FindObjectsOfType<ColorSettings>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
