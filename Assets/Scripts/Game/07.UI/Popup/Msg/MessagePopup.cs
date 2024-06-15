using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MessagePopup : Popup
{
    [SerializeField] private TextMeshProUGUI tmp_msg;
    [SerializeField] private Button button;

    public void Awake()
    {
        button.onClick.AddListener(CloseButton);
    }

    public override void Open(UnityAction action = null)
    {
        base.Open(action);
    }

    public override void Close(UnityAction action = null)
    {
        base.Close(action);
    }

    public void CloseButton()
    {
        UIManager.Instace.ClosePopup(null, type);
    }

    public void MsgSet(string txt)
    {
        tmp_msg.text = txt;
        
    }



}
