using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyStages : MonoBehaviour
{
    [SerializeField]
    private Transform content;

    [SerializeField]
    private StageButton stageButton;


    List<StageButton> stageButtons = new List<StageButton>();

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
       var files =   Resources.LoadAll("Data/Map");

       for(int i = 0; i < files.Length; ++i)
       {
            string stageName = files[i].name;

            var clone = Instantiate(stageButton, content);

            clone.SetStageButton(stageName);

            stageButtons.Add(clone);
       }


    }



}
