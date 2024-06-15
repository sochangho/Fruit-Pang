using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class Popup : MonoBehaviour
{
    public Type type;

    public string openSound = "popup_open";
    public string closeSound = "popup_close";

    virtual public void Open(UnityAction action = null) 
    {
        //action?.Invoke();
        this.gameObject.SetActive(true);
        GameSoundManager.Instace.OnPlaySound(openSound, Sound.Effect);
        gameObject.transform.SetAsLastSibling();
    }

    virtual public void Close(UnityAction action = null)
    {
        //action?.Invoke();
        GameSoundManager.Instace.OnPlaySound(closeSound, Sound.Effect);
        this.gameObject.SetActive(false);
       
    }

}
