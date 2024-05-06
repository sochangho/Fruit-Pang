using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;
public class StagePart : FruitPangEditorPart
{
    int currentStage = 0;

    int addStage = 0;

    string[] displayedOption;

    int[] optionValues;

    public event System.Action loadEvent;


    public int CurrentStage { get { return currentStage; } set { currentStage = value; } }

    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);

        // 전체 정보로드 
        // 스테이지 갯수 및 스테이지 번호 가져오기

        displayedOption = new string[0];
        optionValues = new int[0];
    }
    protected override void PartOnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        addStage = EditorGUILayout.IntField("Total Width", addStage);
        if (GUILayout.Button("AddStage")) AddStage(addStage);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        currentStage = EditorGUILayout.IntPopup("Stage", currentStage, displayedOption, optionValues);        
        if (GUILayout.Button("Load")) 
            LoadStage(currentStage);
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    public void LoadStage(int stage)
    {
        Debug.Log("Load Stage" + stage);

        loadEvent?.Invoke();

        // 스테이지 로드 
    }

    public void InitStageRanges(int[] stages)
    {
        displayedOption = new string[stages.Length];
        optionValues = new int[stages.Length];

        for(int i = 0; i < stages.Length; ++i)
        {
            displayedOption[i] = stages[i].ToString();
        }

        for(int i = 0; i  < stages.Length; ++i)
        {
            optionValues[i] = stages[i];
        }

        currentStage = stages[0];

    }


    public void AddStage(int stage)
    {

        for(int i = 0; i < displayedOption.Length; ++i)
        {
            if(stage == optionValues[i])
            {
                Debug.LogWarning($"Exit Stage {stage}");
                return;
            }
        }


        string[] copydisPlayOption = new string[displayedOption.Length + 1];
        int[] copyOptionValues = new int[optionValues.Length + 1];

        for(int i = 0; i < displayedOption.Length; ++i)
        {
            copydisPlayOption[i] = displayedOption[i];
        }

        copydisPlayOption[displayedOption.Length] = stage.ToString();
        
        for(int i = 0; i < optionValues.Length; ++i)
        {
            copyOptionValues[i] = optionValues[i]; 

        }
        copyOptionValues[optionValues.Length] = stage;

        displayedOption = copydisPlayOption;
        optionValues = copyOptionValues;

    }

}
#endif