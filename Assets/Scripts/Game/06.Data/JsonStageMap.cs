using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class JsonStageMap
{
 


    public void Save(string path, string stage, int totalWidth, int totalHeight, List<FruitNodeInfo> fruitNodeInfo)
    {
       string jsonData  =  StageToJson(totalWidth, totalHeight, fruitNodeInfo);

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


    public string StageToJson(int totalWidth,int totalHeight, List<FruitNodeInfo> fruitNodeInfos)
    {
        StageMapTotalDatas stageMapTotalDatas = new StageMapTotalDatas();

        stageMapTotalDatas.totalHeight = totalHeight;
        stageMapTotalDatas.totalWidth = totalWidth;
        stageMapTotalDatas.fruitNodeInfos = fruitNodeInfos;

        string datas = JsonUtility.ToJson(stageMapTotalDatas);

        return datas;
    }

    public StageMapTotalDatas JsonToStageMapData(string data)
    {
        return JsonUtility.FromJson<StageMapTotalDatas>(data);
    } 
    
}
