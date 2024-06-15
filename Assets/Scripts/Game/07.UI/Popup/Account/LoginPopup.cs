using System.Collections;
using System.Collections.Generic;

using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using TMPro;



public class LoginPopup : Popup
{
    [SerializeField] private TMP_InputField input_email;
    [SerializeField] private TMP_InputField input_password;

    [SerializeField] private Button button_close;
    [SerializeField] private Button button_createAccount;
    [SerializeField] private Button button_login;

    public void Awake()
    {
        FirebaseAuthManager.Instace.failSign += LoginFail;
        FirebaseAuthManager.Instace.successSign += LoginSuccess;
        FirebaseAuthManager.Instace.loginResult += UIManager.Instace.Dimmed;

        button_close.onClick.AddListener(CloseButton);
        button_createAccount.onClick.AddListener(OpenAccountCreateButton);
        button_login.onClick.AddListener(LoginButton);
        
    }

    public override void Open(UnityAction action = null)
    {
        base.Open(action);

        if (PlayerPrefs.HasKey("email"))
        {
            input_email.text = PlayerPrefs.GetString("email");
        }
        if (PlayerPrefs.HasKey("password"))
        {
            input_password.text = PlayerPrefs.GetString("password");
        }

    }
    public override void Close(UnityAction action = null)
    {
        base.Close(action);

    }

    public void CloseButton()
    {
        UIManager.Instace.ClosePopup(null, type);
    }

    public void OpenAccountCreateButton()
    {
        UIManager.Instace.OpenPopup<CreateAccountPopup>(null);
    }

    public void LoginButton()
    {
        UIManager.Instace.Dimmdlast();
        FirebaseAuthManager.Instace.Login(input_email.text, input_password.text);
    }

    public void LoginFail(string email, string password, string msg)
    {
        UIManager.Instace.OpenPopup<MessagePopup>(null).MsgSet(msg);
    }

    public void LoginSuccess(string email, string password, string msg)
    {
        CloseButton();

        PlayerPrefs.SetString("email", email);
        PlayerPrefs.SetString("password", password);

        SceneManager.LoadScene("LobbyScene");
    }


}
