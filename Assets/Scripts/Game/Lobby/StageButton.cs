using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
   
    private int stageLevel;
    [SerializeField]
    private Text stageText;
    [SerializeField]
    private Button button;

    public void SetStageButton(int stageLevel)
    {
        button.onClick.AddListener(EnterStage);
        this.stageLevel = stageLevel;
        stageText.text = $"Stage : {stageLevel.ToString()}";
    }

    public void StageActive(int currentlevel)
    {
        if(currentlevel < stageLevel)
        {
            button.interactable = false;
            return;
        }
        button.interactable = true;
    }

    public void EnterStage()
    {
      
        PlayerPrefs.SetInt(StringKey.STAGE_LEVEL, stageLevel);
        SceneManager.LoadScene("GameScene");
    }

    


}
