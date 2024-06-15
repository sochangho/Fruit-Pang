using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore
{
    private ScoreData scoreData;

    private int currentScore;

    public event System.Action<int,int> ScoreEventNodeType;

    public event System.Action<int,int> ScoreEventNodeState;



    public void Setting(ScoreData scoreData)
    {
        this.scoreData = scoreData;
        currentScore = 0;
    }
   
    

    public void UpdateScore_NodeType(NodeType nodeType)
    {
        int score = GameDatasManager.Instace.puzzleScoreDatas.GetNodeScore(nodeType);

        currentScore += score;

        if(scoreData.scoreThree < currentScore)
        {
            currentScore = scoreData.scoreThree;
        }

        ScoreEventNodeType?.Invoke(currentScore, scoreData.scoreThree);

        Debug.Log($"<color=yellow> Score : {currentScore}/{scoreData.scoreThree} </color>");

    }
    
    public void UpdateScore_NodeState(NodeState nodeState)
    {
        int score = GameDatasManager.Instace.puzzleScoreDatas.GetStateScore(nodeState);

        currentScore += score;

        if (scoreData.scoreThree < currentScore)
        {
            currentScore = scoreData.scoreThree;
        }
        ScoreEventNodeState?.Invoke(currentScore, scoreData.scoreThree);

        Debug.Log($"<color=Green> Score : {currentScore}/{scoreData.scoreThree} </color>");
    }

    public int GetCurrentScore => currentScore;
    public int GetScoreOne => scoreData.scoreOne;
    public int GetScoreTwo => scoreData.scoreTwo;
    public int GetScoreThree => scoreData.scoreThree;
}
