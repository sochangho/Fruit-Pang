using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : Singleton<InfoManager>
{
    readonly public Dictionary<int, StageMapTotalDatas> dic_stageDatas = new Dictionary<int, StageMapTotalDatas>();

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this.gameObject);

        LoadData();
    }

    public void LoadData()
    {
        StageDatasLoad();
    }

    private void StageDatasLoad()
    {
       TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Data/Map");
       
       for(int i = 0; i < textAssets.Length; ++i)
       {
           string jsonData = textAssets[i].ToString();
           StageMapTotalDatas stageMapTotalDatas = JsonUtility.FromJson<StageMapTotalDatas>(jsonData);
           dic_stageDatas.Add(stageMapTotalDatas.stagelevel, stageMapTotalDatas);
       }
    }

  
    public StageMapTotalDatas GetStageDatas(int level)
    {
        if (!dic_stageDatas.ContainsKey(level))
        {
            Debug.Log("Error"); 
        }


        return dic_stageDatas[level];
    } 

   
}
