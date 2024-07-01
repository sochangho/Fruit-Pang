using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalsUI : MonoBehaviour
{
  
    [SerializeField] private GoalNodeTypeElementUI nodeTypeUI;
    [SerializeField] private GoalStateTypeElementUI stateTypeUI;

    private List<GoalNodeTypeElementUI> nodeTypes = new List<GoalNodeTypeElementUI>();
    private List<GoalStateTypeElementUI> stateTypes = new List<GoalStateTypeElementUI>();


    public void Setting(List<GoalNodeTypeElement> gne , List<GoalStateElement> gse)
    {
        foreach(var e in gne)
        {
            var clone = Instantiate(nodeTypeUI, this.transform);
            var data = GameDatasManager.Instace.puzzleTextureDatas;
            var sprite = data.GetNodeTypeSprite(e.nodeType);              
            clone.Setting(e.nodeType,sprite,e.count.ToString());
            nodeTypes.Add(clone);
        }

        foreach(var e in gse)
        {
            var clone = Instantiate(stateTypeUI, this.transform);
            var data = GameDatasManager.Instace.puzzleTextureDatas;
            var sprite = data.GetNodeStateSprite(e.nodeState);
            clone.Setting(e.nodeState, sprite, e.count.ToString());
            stateTypes.Add(clone);
        }
    }
    
    public void NodeStateUpdate(NodeState nodeState, int count)
    {
       var t =   stateTypes.Find(x => x.GetNodeState() == nodeState);

        if (t == null) return;

        t.UpdateUI(count);
    }

    public void NodeTypeUpdate(NodeType nodeType, int count)
    {
        var t = nodeTypes.Find(x => x.GetNodeType() == nodeType);

        if (t == null) return;

        t.UpdateUI(count);

    }
    

}
