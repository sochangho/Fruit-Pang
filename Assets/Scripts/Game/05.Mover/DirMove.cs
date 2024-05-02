using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirMove 
{
    protected Nodes nodes;
    protected TileObjects tileObjects;

    protected TemporaryNodes temporaryNodes;

    protected List<int> refillCounts;

    protected Queue<Node>[] refillNodes;

    public int moveNodeCount;

    readonly public NodeMover.Direction direction;
    public DirMove(Nodes nodes, TileObjects tileObjects , NodeMover.Direction direction)
    {
        this.nodes = nodes;
        this.tileObjects = tileObjects;
        this.direction = direction;

    }



    abstract public void EmptyMove();

    abstract public void RefillNode();

    protected bool IsMovableTile_TemporaryNode(int x, int y)
    {
        bool isPossible = y < GlobalSetting.TotalHeight && tileObjects[x, y] != null && temporaryNodes[x, y] == null;
        return isPossible;
    }

    protected bool IsMovableTile_Node(int x, int y)
    {
        bool isPossible = y < GlobalSetting.TotalHeight && tileObjects[x, y] != null && nodes[x, y] == null;
        return isPossible;
    }


    protected bool IsExitNode(int x, int y)
    {
        return tileObjects[x, y] != null && nodes[x, y] != null;
    }

    protected bool IsWallNode(int x, int y)
    {
        return nodes[x, y] != null && !nodes[x, y].MovableNode();
    }


}
