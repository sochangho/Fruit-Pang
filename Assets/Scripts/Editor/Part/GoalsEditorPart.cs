using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
public class GoalsEditorPart : FruitPangEditorPart
{

    public List<GoalNodeTypeElement> goalEditorNodeTypeElements;
    public List<GoalStateElement> goalEditorStateElements ;

    private Vector2 scrollPosition = new Vector2();

    public void Setting(List<GoalNodeTypeElement> gene, List<GoalStateElement> gese)
    {
        if(gene == null || gene.Count == 0)
        {
            goalEditorNodeTypeElements = new List<GoalNodeTypeElement>();

            int len = System.Enum.GetNames(typeof(NodeType)).Length;
            for (int i = 0; i < len - 1; i++)
            {
                NodeType t = (NodeType)i;
                goalEditorNodeTypeElements.Add(new GoalNodeTypeElement() { nodeType = t, count = 0 });
            }
        }
        else
        {

            goalEditorNodeTypeElements = gene;

        }

        if(gese == null || gese.Count == 0)
        {

            goalEditorStateElements = new List<GoalStateElement>();

            int len = System.Enum.GetNames(typeof(NodeState)).Length;

            for (int i = 1; i < len; i++)
            {
                NodeState t = (NodeState)i;
                goalEditorStateElements.Add(new GoalStateElement() { nodeState = t, count = 0 });
            }


           
        }
        else
        {
            goalEditorStateElements = gese;
        }


    }


    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);

    
    }




    protected override void PartOnGUI()
    {
        EditorGUILayout.BeginVertical();


        GUILayout.Label("Fruit Pang Goals", EditorStyles.boldLabel);

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandWidth(true), GUILayout.Height(130));
        EditorGUILayout.BeginVertical();

        if(goalEditorNodeTypeElements != null)
        {
            for(int i = 0; i < goalEditorNodeTypeElements.Count; i++)
            {
                var element = goalEditorNodeTypeElements[i];
                element.count = EditorGUILayout.IntField(element.nodeType.ToString(), element.count);
            }
        }

        if(goalEditorStateElements != null)
        {
            for(int i =0; i < goalEditorStateElements.Count; i++)
            {
                var element = goalEditorStateElements[i];
                element.count = EditorGUILayout.IntField(element.nodeState.ToString(), element.count);
            }
        }


        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}



#endif