using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoals
{
    private List<GoalNodeTypeElement> goalNodeTypeElements = new List<GoalNodeTypeElement>();
    
    private List<GoalStateElement> goalStateElements = new List<GoalStateElement>();


    public event System.Action<List<GoalNodeTypeElement>, List<GoalStateElement>> goalSettingEvent;
    public event System.Action<NodeType , int> GoalNodeTypeEvent;
    public event System.Action<NodeState, int> GoalStateTypeEvent;
    public event System.Action CompleteGame;

    private bool isComplete = false;

    public void SettingGoals(List<GoalNodeTypeElement> goalNodeTypeElements, List<GoalStateElement> goalStateElements)
    {          
        foreach(var e in goalNodeTypeElements)
        {
            if(e.count > 0)
            {
                GoalNodeTypeElement goal = new GoalNodeTypeElement();
                goal.nodeType = e.nodeType;
                goal.count = e.count;

                this.goalNodeTypeElements.Add(goal);
            }
        }
        foreach(var e in goalStateElements)
        {
            if(e.count > 0)
            {
                GoalStateElement goal = new GoalStateElement();
                goal.nodeState = e.nodeState;
                goal.count = e.count;

                this.goalStateElements.Add(goal);
            }
        }

        goalSettingEvent?.Invoke(this.goalNodeTypeElements, this.goalStateElements);
    }  

    
    public void GoalNodeTypeUpdate(NodeType nodeType)
    {
       var element =  goalNodeTypeElements.Find(x => x.nodeType == nodeType);

       if (element == null) return;


       element.count--;

       if(element.count < 0)
       {
            element.count = 0;
       }

       GoalNodeTypeEvent?.Invoke(element.nodeType,element.count);

        if (GoalsCompleteCompare() && !isComplete)
        {
            CompleteGame?.Invoke();
            isComplete = true;
        }


    }

    public void GoalNodeStateUpdate(NodeState nodeState)
    {
        var element = goalStateElements.Find(x => x.nodeState == nodeState);

        if (element == null) return;

        element.count--;

        if (element.count < 0)
        {
            element.count = 0;
        }

       GoalStateTypeEvent?.Invoke(element.nodeState, element.count);

        if (GoalsCompleteCompare() && !isComplete)
        {
            CompleteGame?.Invoke();
            isComplete = true;
        }
    }


    public bool GoalsCompleteCompare()
    {

        int totalCount = 0;

        foreach(var e in goalNodeTypeElements)
        {
            totalCount += e.count;
        }

        foreach (var e in goalStateElements)
        {
            totalCount += e.count;
        }

      
        return totalCount == 0;
    }


}
