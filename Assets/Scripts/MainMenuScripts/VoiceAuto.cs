using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceAuto : MonoBehaviour
{
    AudioSource voiceClip;

    private void Start()
    {
        voiceClip = GetComponent<AudioSource>();
        ChangeClipVolume();
        OptionsController.Instance.voiceChanged += ChangeClipVolume;
    }

    private void ChangeClipVolume() 
    {
        voiceClip.volume = SettingsSaveManager.Instance.soundLevel * 0.1f;
    }
}
