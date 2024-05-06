using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FruitNodeInfo
{
    public int x;
    public int y;
    public TileType tileType = TileType.Node;
    public NodeType nodeType;
    public NodeState nodeState = NodeState.None;

    public FruitNodeInfo(int x, int y)
    {
        this.x = x;
        this.y = y;

        tileType = TileType.Node;
        nodeType = NodeType.None;
        nodeState = NodeState.None;

    }


    public void SetInfomation(int x, int y, TileType tileType, NodeType nodeType, NodeState nodeState)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
        this.nodeType = nodeType;
        this.nodeState = nodeState;
    }


}
