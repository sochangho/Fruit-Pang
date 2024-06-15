using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using GooglePlayGames.BasicApi;
//using GooglePlayGames;

using TMPro;

public class GLogin : MonoBehaviour
{
    //public TextMeshProUGUI tmp;


    //GPlayerLoginInfo gPlayer;


    //public void Start()
    //{
    //    SignIn();
    //}

    //public void SignIn()
    //{
    //    PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    //}

    //internal void ProcessAuthentication(SignInStatus status)
    //{
    //    if (status == SignInStatus.Success)
    //    {
    //        // Continue with Play Games Services

    //        string name = PlayGamesPlatform.Instance.GetUserDisplayName();
    //        string id = PlayGamesPlatform.Instance.GetUserId();
    //        string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();


    //        gPlayer = new GPlayerLoginInfo(name, id, ImgUrl);

    //        tmp.text = "Google login Success";

    //    }
    //    else
    //    {
    //        // Disable your integration with Play Games Services or show a login button
    //        // to ask users to sign-in. Clicking it should call
    //        // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).


    //        Debug.LogError("Fail Login");

    //        tmp.text = "Google login Fail";

    //    }
    //}


}


public class GPlayerLoginInfo
{
   readonly public string name;
   readonly public string id;
   readonly public string imgUrl;

   public GPlayerLoginInfo(string name, string id, string imgUrl)
    {
        this.name = name;
        this.id = id;
        this.imgUrl = imgUrl;
    }

}

