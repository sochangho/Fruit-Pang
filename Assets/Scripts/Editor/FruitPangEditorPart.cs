using System.Collections;
using System.Collections.Generic;
using UnityEngine;



#if UNITY_EDITOR
using UnityEditor;

public abstract class FruitPangEditorPart 
{

    protected Rect rect;


    public void FruitPangOnGUI()
    {
        GUILayout.BeginArea(rect, GUI.skin.window);
        EditorGUILayout.BeginHorizontal();

        PartOnGUI();

        EditorGUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public virtual void InitAreaRect(float x, float y, float width, float Height)
    {
        rect = new Rect(x, y, width, Height);
    }
    protected abstract void PartOnGUI();
    


}

#endif
