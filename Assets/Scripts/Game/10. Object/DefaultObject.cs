using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultObject : MonoBehaviour
{
    [SerializeField]
    protected string soundName;

    [SerializeField]
    protected Sound soundType;
    
    protected void PlaySound()
    {
        GameSoundManager.Instace.OnPlaySound(soundName, soundType);
    }
    
}
