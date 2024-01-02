using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider ALLSlider, BGMSlider, SFXSlider;

    private void Start()
    {
        SetALLVolume();
        SetBGMVolume();
        SetSFXVolume();
    }

    public void SetALLVolume()
    {
        float volume = ALLSlider.value;
        myMixer.SetFloat("all", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
}
