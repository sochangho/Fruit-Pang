using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;



public class FruitNodeButton : FruitPangEditorPart
{
    private TileNodeEditorPart tileNodeEditorPart;

    private FruitNodeInfo nodeInfo;
    
    public FruitNodeButton(FruitNodeInfo fruitNodeInfo,TileNodeEditorPart tileNodeEditorPart)
    {
        nodeInfo = fruitNodeInfo;
        this.tileNodeEditorPart = tileNodeEditorPart; 
    }

    protected override void PartOnGUI()
    {

        Debug.Log($"Draw {nodeInfo.x}{nodeInfo.y}");

        if(GUILayout.Button("", GUILayout.Width(50), GUILayout.Height(50)))
        {
            
        }
        
    }
}

#endif