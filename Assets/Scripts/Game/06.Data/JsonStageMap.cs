using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class JsonStageMap
{
 


    public void Save(string path,int level, string stage, int totalWidth, int totalHeight,
        List<FruitNodeInfo> fruitNodeInfo,
        List<GoalNodeTypeElement> gene, List<GoalStateElement> gese , ScoreData scoreData)
    {
       string jsonData  =  StageToJson(level, totalWidth, totalHeight, fruitNodeInfo,gene,gese , scoreData);

       FileStream fileStream = new FileStream($"{path}/{stage}.json", FileMode.Create); 
       
       byte[] data = Encoding.UTF8.GetBytes(jsonData); 
       
       fileStream.Write(data, 0, data.Length); 
        
       fileStream.Close();    
    }


    public StageMapTotalDatas Load(string path ,string stage)
    {
        string loadPath = $"{path}/{stage}.json";
        FileStream fileStream = new FileStream(loadPath, FileMode.Open);

        byte[] data = new byte[fileStream.Length]; fileStream.Read(data, 0, data.Length); 
        fileStream.Close();
        
        string jsonData = Encoding.UTF8.GetString(data);

       

        return JsonToStageMapData(jsonData);
    }

    //public StageMapTotalDatas LoadGame(string stage)
    //{

    //    TextAsset textAsset =  Resources.Load<TextAsset>($"Data/Map/{stage}");

    //    string jsonData = textAsset.ToString();

    //    return  JsonToStageMapData(jsonData);
    //}



    public string StageToJson(int stagelevel, int totalWidth,int totalHeight,
        List<FruitNodeInfo> fruitNodeInfos ,
        List<GoalNodeTypeElement> gene , List<GoalStateElement> gese , ScoreData scoreData)
    {
        StageMapTotalDatas stageMapTotalDatas = new StageMapTotalDatas();

        stageMapTotalDatas.stagelevel = stagelevel;
        stageMapTotalDatas.totalHeight = totalHeight;
        stageMapTotalDatas.totalWidth = totalWidth;
        stageMapTotalDatas.fruitNodeInfos = fruitNodeInfos;
        stageMapTotalDatas.goalEditorNodeTypeElements = gene;
        stageMapTotalDatas.goalEditorStateElements = gese;
        stageMapTotalDatas.scoreData = scoreData;


        string datas = JsonUtility.ToJson(stageMapTotalDatas);

        return datas;
    }

    public StageMapTotalDatas JsonToStageMapData(string data)
    {
        return JsonUtility.FromJson<StageMapTotalDatas>(data);
    } 
    
}
