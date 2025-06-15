using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Slider soundLevelSlider;
    [SerializeField] private Dropdown qualityDropDown;
    public static OptionsController Instance;
    public Action voiceChanged;

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

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        soundLevelSlider.value = SettingsSaveManager.Instance.soundLevel;
        QualitySettings.SetQualityLevel(SettingsSaveManager.Instance.qualityLevel);
        qualityDropDown.value = QualitySettings.GetQualityLevel();
    }

    public void ChangeSoundLevel() 
    {
        SettingsSaveManager.Instance.soundLevel = (int)soundLevelSlider.value;
        voiceChanged?.Invoke();
    }

    public void ChangeQualityLevel() 
    {
        SettingsSaveManager.Instance.qualityLevel = (int)qualityDropDown.value;
        QualitySettings.SetQualityLevel(SettingsSaveManager.Instance.qualityLevel);
    }
}
