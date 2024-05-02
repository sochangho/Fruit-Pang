using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR

using UnityEditor;

public class TileNodeEditorPart : FruitPangEditorPart
{

    private List<FruitNodeInfo> fruitNodeInfos;
    private FruitPangEditor fruitPangEditor;

    private float sizeW = 50;
    private float sizeH = 50; 

    public TileNodeEditorPart(FruitPangEditor fruitPangEditor)
    {
        this.fruitPangEditor = fruitPangEditor;
    }

    private Vector2 scrollPosition = new Vector2();
    



    public FruitNodeInfo this[int x, int y]
    {

        get
        {

            if (x >= fruitPangEditor.stageSizePart.TotalWidth || y >= fruitPangEditor.stageSizePart.TotalHeight || x < 0 || y < 0)
            {

                throw new System.IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, fruitPangEditor.stageSizePart.TotalWidth, fruitPangEditor.stageSizePart.TotalHeight);
         
            var f= fruitNodeInfos[index];

            return f;
        }

    }

  

    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);

    }

    
    protected override void PartOnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandWidth(true), GUILayout.Height(450));
      

        EditorGUILayout.BeginVertical();

        for (int y = fruitPangEditor.stageSizePart.TotalHeight - 1; y >= 0; --y)
        {

            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < fruitPangEditor.stageSizePart.TotalWidth; ++x)
            {


                if (GUILayout.Button($"{this[x,y].x},{this[x,y].y}", GUILayout.Width(sizeW), GUILayout.Height(sizeH)))
                {

                }
               
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

       EditorGUILayout.EndScrollView();
     
    }

    public void NodeCreate(List<FruitNodeInfo> fruitNodeInfos)
    {
        this.fruitNodeInfos = fruitNodeInfos; 
    }
}


#endif