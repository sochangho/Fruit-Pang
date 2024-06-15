using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : Singleton<GameSoundManager>
{
    const string pathBGM = "BGM/";
    const string pathEffect = "Effect/";

   
    SoundManager soundManager = new SoundManager();
    public SoundManager Sound { get { return soundManager; } }


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        soundManager.init();

        var bmgValue = GetSavedSoundData("bgm",GetSoundVolume(global::Sound.Bgm));
        var effectValue = GetSavedSoundData("effect", GetSoundVolume(global::Sound.Effect));

        BgmSoundSet(bmgValue);
        EffectSoundSet(effectValue);

    }

    private void Start()
    {
        OnPlaySound("bgm1", global::Sound.Bgm);
    }


    public float GetSavedSoundData(string key, float value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetFloat(key, value);
        }


        return PlayerPrefs.GetFloat(key);
    }


    public void Clear()
    {
        soundManager.Clear();
    }

    public void OnPlaySound(string sourceName, Sound soundType = global::Sound.Effect)
    {
        if(sourceName.Length == 0)
        {
            return;
        }

        string path = string.Empty;

        if (soundType == global::Sound.Bgm)
        {
            path = $"{pathBGM}{sourceName}";
        }
        else
        {
            path = $"{pathEffect}{sourceName}";
        }


        soundManager.Play(path, soundType);
    }

    public void BgmSoundSet(float value)
    {
        soundManager.SoundSize(global::Sound.Bgm, value);
    }

    public void EffectSoundSet(float value)
    {
        soundManager.SoundSize(global::Sound.Effect, value);
    }

    public void Mute(Sound sound, bool value) => soundManager.SoundMute(sound, value);

    public float GetSoundVolume(Sound sound) => soundManager.GetSoundVolume(sound);

}
