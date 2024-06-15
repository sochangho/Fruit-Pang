using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeEffect : IDestoryObjectEvent
{
    protected Node node;
    public NodeEffect(Node node){this.node = node;}

    public abstract void EffectStart();
    public virtual void EffectEnd() 
    { 
        DestoryObjectEvent(); 
    
    }
    public abstract bool EffectEndCheck();

    public void DestoryObjectEvent()
    {
        var gameGoals = BringOutObject.Instace.GetThreeMatchGame().gameGoals;
        var gameScore = BringOutObject.Instace.GetThreeMatchGame().gameScore;
        gameGoals.GoalNodeStateUpdate(node.GetNodeState());
        gameScore.UpdateScore_NodeState(node.GetNodeState());
    }
}
