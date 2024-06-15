using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityEngine.UI;

using TMPro;

public class CreateAccountPopup : Popup
{
    [SerializeField] private TMP_InputField input_email;
    [SerializeField] private TMP_InputField input_password;

    [SerializeField] private Button button_close;
    [SerializeField] private Button button_create;

    public void Awake()
    {
        FirebaseAuthManager.Instace.failCreate += CreateAccountFail;
        FirebaseAuthManager.Instace.successCreate += CreateAccountSuccess;
        FirebaseAuthManager.Instace.createResult += UIManager.Instace.Dimmed;

        button_create.onClick.AddListener(CreateButton);
        button_close.onClick.AddListener(CloseButton);
    }

    public override void Open(UnityAction action = null)
    {
        base.Open(action);
    }
    public override void Close(UnityAction action = null)
    {
        base.Close(action);
    }

    public void CreateButton()
    {
        UIManager.Instace.Dimmdlast();
        FirebaseAuthManager.Instace.Create(input_email.text, input_password.text);
    }

    public void CloseButton()
    {
        UIManager.Instace.ClosePopup(null, type);
    }


    public void CreateAccountSuccess(string email, string password, string msg)
    {
        UIManager.Instace.ClosePopup(null,type);

    }

    public void CreateAccountFail(string email, string password, string msg)
    {
        UIManager.Instace.OpenPopup<MessagePopup>(null).MsgSet(msg);

    }




}
