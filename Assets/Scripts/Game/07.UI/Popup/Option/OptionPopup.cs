using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionPopup : Popup
{
    [SerializeField] SoundOption soundOption_BGM;
    [SerializeField] SoundOption soundOption_Effect;

    public void Awake()
    {
        soundOption_BGM.Setting();
        soundOption_Effect.Setting();
    }

    public override void Open(UnityAction action = null)
    {
        base.Open(action);
    }

    public override void Close(UnityAction action = null)
    {
        base.Close(action);
    }

    public void OnClosedButton()
    {
        UIManager.Instace.ClosePopup(null, type);
    }


}
