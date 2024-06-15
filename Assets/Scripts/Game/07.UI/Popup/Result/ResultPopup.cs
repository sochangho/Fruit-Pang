using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class ResultPopup : Popup
{
    [SerializeField] private GameObject activeStarOne;
    [SerializeField] private GameObject activeStarTwo;
    [SerializeField] private GameObject activeStarThree;

    [SerializeField] private ParticleSystem particleSystemOne;
    [SerializeField] private ParticleSystem particleSystemTwo;
    [SerializeField] private ParticleSystem particleSystemThree;



    [SerializeField] private Button bnt_replay;
    [SerializeField] private Button bnt_Robby;

    private GameScore gameScore;

    private ThreeMatchGame threeMatchGame;

    public void Awake()
    {
        bnt_replay.onClick.AddListener(Replay);
        bnt_Robby.onClick.AddListener(Roddy);
     
        threeMatchGame = BringOutObject.Instace.GetThreeMatchGame();
    }


    public override void Open(UnityAction action = null)
    {
        base.Open(action);
        gameScore = BringOutObject.Instace.GetThreeMatchGame().gameScore;
        bnt_replay.interactable = false;
        bnt_Robby.interactable = false;

        GameSoundManager.Instace.OnPlaySound("result", Sound.Effect);

        StartCoroutine(ResultScoreActive());

    }

    public override void Close(UnityAction action = null)
    {
        base.Close(action);
    }

    public void Replay()
    {
        UIManager.Instace.ClosePopup(null,type);

        SceneManager.LoadScene("GameScene");

    }
    

    public void Roddy()
    {
        threeMatchGame.LobbyScene();
    }

    IEnumerator ResultScoreActive()
    {
        int currentScore = gameScore.GetCurrentScore;

        int one = gameScore.GetScoreOne;
        int two = gameScore.GetScoreTwo;
        int three = gameScore.GetScoreThree;

        activeStarOne.SetActive(false);
        activeStarTwo.SetActive(false);
        activeStarThree.SetActive(false);


        bnt_replay.interactable = false;
        bnt_Robby.interactable = false;

        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

     

        if (currentScore >= one)
        {
            yield return waitForSeconds;
            activeStarOne.SetActive(true);
            particleSystemOne.Play();
            GameSoundManager.Instace.OnPlaySound("star", Sound.Effect);
        }
        else
        {
            bnt_replay.interactable = true;
            bnt_Robby.interactable = true;

            yield break;
        }


      

        if (currentScore >= two)
        {
            yield return waitForSeconds;
            activeStarTwo.SetActive(true);
            particleSystemTwo.Play();
            GameSoundManager.Instace.OnPlaySound("star", Sound.Effect);
        }
        else
        {
            bnt_replay.interactable = true;
            bnt_Robby.interactable = true;

            yield break;
        }

       

        if (currentScore >= three)
        {
            yield return waitForSeconds;
            activeStarThree.SetActive(true);
            particleSystemThree.Play();
            GameSoundManager.Instace.OnPlaySound("star", Sound.Effect);
        }

        bnt_replay.interactable = true;
        bnt_Robby.interactable = true;

    }

}
