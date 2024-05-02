using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class NodeInfomation
{
   public readonly int x;
   public readonly int y;
   public readonly NodeType nodeType;   
   public readonly NodeState nodeState;
   
   public NodeInfomation(int x, int y, NodeType nodeType,NodeState nodeState = NodeState.None)
   {
        this.x = x;
        this.y = y;
        this.nodeType = nodeType;
        this.nodeState = nodeState;
   }
    
}
