using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    
    public void OnGameStart()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene("LobbyScene");
#elif UNITY_ANDROID && !UNITY_EDITOR
       
       UIManager.Instace.OpenPopup<LoginPopup>(null);
#endif
    }

    
}
