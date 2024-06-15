using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

public sealed class FruitNodeInfoEditorPart : FruitPangEditorPart
{

    private FruitEditorNodeButton button;

   

    public void SetInfo(FruitEditorNodeButton button)
    {
        this.button = button;
    }

    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);
      
    }

    protected override void PartOnGUI()
    {
         if(button == null)
         {
            return;
         }

        EditorGUILayout.BeginVertical();

               
        button.nodeInfo.tileType = (TileType)EditorGUILayout.EnumPopup("Tile Type ", button.nodeInfo.tileType);

        if (button.nodeInfo.tileType != TileType.None)
        {
            button.nodeInfo.nodeType = (NodeType)EditorGUILayout.EnumPopup("NodeType", button.nodeInfo.nodeType);

            button.nodeInfo.nodeState = (NodeState)EditorGUILayout.EnumPopup("Node State", button.nodeInfo.nodeState);
        }

        button.ButtonGUISetting();

        EditorGUILayout.EndVertical();

   
    }

}

#endif