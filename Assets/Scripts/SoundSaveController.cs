using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSaveController : MonoBehaviour
{
    [SerializeField] private Slider soundSlider = null;

    void Start()
    {
        LoadValues();
    }

    public void SaveSoundButton()
    {
        float soundValue = soundSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundValue);
        LoadValues();
    }

    void LoadValues()
    {
        float soundValue = PlayerPrefs.GetFloat("SoundVolume");

        if(soundSlider)
            soundSlider.value = soundValue;

        AudioListener.volume = soundValue;
    }


}
