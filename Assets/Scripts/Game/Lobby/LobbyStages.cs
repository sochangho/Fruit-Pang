using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using System;
public class LobbyStages : MonoBehaviour
{
    [SerializeField]
    private Transform content;

    [SerializeField]
    private StageButton stageButton;

    [SerializeField]
    private TextMeshProUGUI tmp_test;

    List<StageButton> stageButtons = new List<StageButton>();

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        var datas = InfoManager.Instace.dic_stageDatas;


        List<KeyValuePair<int, StageMapTotalDatas>> stageMapTotalDatas = new List<KeyValuePair<int, StageMapTotalDatas>>();

        foreach(var data in datas)
        {
            stageMapTotalDatas.Add(new KeyValuePair<int,StageMapTotalDatas>(data.Key,data.Value));
        }

        stageMapTotalDatas.Sort(new Comparison<KeyValuePair<int, StageMapTotalDatas>>((n1, n2) => n1.Key.CompareTo(n2.Key)));


        foreach( var data in stageMapTotalDatas)
        {
            int stagelevel = data.Key;
            var clone = Instantiate(stageButton, content);
            clone.SetStageButton(stagelevel);
            stageButtons.Add(clone);
        }


        StageSetting();
    }

    public void StageSetting()
    {

#if UNITY_ANDROID && !UNITY_EDITOR

        var data = FirebaseDBManager.Instace.userData;
           
        int level = data.level;

        tmp_test.text = $"Level : {level}";

       for(int index = 0; index < stageButtons.Count; ++index)
       {
            stageButtons[index].StageActive(level);
       }
#endif       
    }


    public void OnOpenOption()
    {
        UIManager.Instace.OpenPopup<OptionPopup>(null);

    }

}
