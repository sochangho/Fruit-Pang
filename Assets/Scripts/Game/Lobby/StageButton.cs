using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    private string stageName;

    [SerializeField]
    private Text stageText;
    [SerializeField]
    private Button button;




    public void SetStageButton(string stageName)
    {
        button.onClick.AddListener(EnterStage);

        this.stageName = stageName;
        stageText.text = $"Stage : {stageName}";

    }

    public void EnterStage()
    {
        int stage = int.Parse(stageName);

        PlayerPrefs.SetInt("GameStage", stage);

        SceneManager.LoadScene("GameScene");
    }



}
