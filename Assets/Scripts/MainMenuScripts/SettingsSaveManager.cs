using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaveManager : MonoBehaviour
{
    public static SettingsSaveManager Instance;

    public int soundLevel { get => PlayerPrefs.GetInt("soundLevel"); set => PlayerPrefs.SetInt("soundLevel", value); }
    public int qualityLevel { get => PlayerPrefs.GetInt("qualityLevel"); set => PlayerPrefs.SetInt("qualityLevel", value); }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
