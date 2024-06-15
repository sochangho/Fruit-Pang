using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageMapTotalDatas
{
    public int stagelevel;
    public int totalWidth;
    public int totalHeight;

    public ScoreData scoreData;

    public List<FruitNodeInfo> fruitNodeInfos;
    public List<GoalNodeTypeElement> goalEditorNodeTypeElements;
    public List<GoalStateElement> goalEditorStateElements;

  
}



[System.Serializable]
public class ScoreData
{
    public int scoreOne;
    public int scoreTwo;
    public int scoreThree;

}


