using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

using UnityEditor;

public class StageSizePart : FruitPangEditorPart
{
    public int totalWidth = 0;
    public int totalHeight = 0;

    private int saveTotalWidth = 0;
    private int saveTotalHeight = 0;

    public int TotalWidth { get { return saveTotalWidth; }}
    public int TotalHeight { get { return saveTotalHeight; } }

    List<FruitNodeInfo> fruitNodeInfos = new List<FruitNodeInfo>();

    TileNodeEditorPart tileNodeEditorPart;

    public StageSizePart(TileNodeEditorPart tileNodeEditorPart)
    {
        this.tileNodeEditorPart = tileNodeEditorPart;
    }



    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);
    }

    protected override void PartOnGUI()
    {
        EditorGUILayout.BeginVertical();
        totalWidth = EditorGUILayout.IntField("Total Width", totalWidth);
        totalHeight  = EditorGUILayout.IntField("Total Height", totalHeight);

        EditorGUILayout.Space();

        if (GUILayout.Button("Create"))
            TileNodeCreate();
        EditorGUILayout.EndVertical();
    }

    public void TileNodeCreate()
    {
        saveTotalWidth = totalWidth;
        saveTotalHeight = totalHeight;

        fruitNodeInfos.Clear();
        for(int index = 0; index < saveTotalWidth * saveTotalHeight; ++index)
        {
            int x, y;
            (x,y) = UtilCoordinate.IndexToCoordinate(index,saveTotalWidth,saveTotalHeight);
            fruitNodeInfos.Add(new FruitNodeInfo(x,y));
        }
        tileNodeEditorPart.NodeCreate(fruitNodeInfos);
    }
    

}

#endif