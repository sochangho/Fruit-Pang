using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;


public class ScoreEditorPart : FruitPangEditorPart
{

    public int scoreOne;
    public int scoreTwo;
    public int scoreThree;



    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);
    }

    protected override void PartOnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Fruit Pang Score", EditorStyles.boldLabel);
        scoreOne = EditorGUILayout.IntField("Score One", scoreOne);
        scoreTwo = EditorGUILayout.IntField("Score Two", scoreTwo);
        scoreThree = EditorGUILayout.IntField("Score Three", scoreThree);
        EditorGUILayout.EndVertical();
    }

}


#endif
