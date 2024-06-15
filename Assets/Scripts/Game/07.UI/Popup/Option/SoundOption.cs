using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    [SerializeField] private Sound soundType;
    [SerializeField] private Slider slider;



    public void Setting()
    {
        float value = 0;

        if (soundType == Sound.Bgm)
        {
           value = GetSavedSoundData("bgm", GameSoundManager.Instace.GetSoundVolume(soundType));

        }
        else
        {

           value = GetSavedSoundData("effect", GameSoundManager.Instace.GetSoundVolume(soundType));

        }


        slider.value = value;
        slider.onValueChanged.AddListener(SoundOnValueChanged);
    }


    public float GetSavedSoundData(string key , float value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, value);
        }


        return PlayerPrefs.GetFloat(key);
    }

    

    public void SoundOnValueChanged(float value)
    {
        if(soundType == Sound.Bgm)
        {
            GameSoundManager.Instace.BgmSoundSet(value);
            PlayerPrefs.SetFloat("bgm", value);

        }
        else
        {
            GameSoundManager.Instace.EffectSoundSet(value);
            PlayerPrefs.SetFloat("effect", value);
        }

    }


}
