using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Swaper
{
    
    private Nodes nodes;
    private Tiles tiles;
    private TileObjects tileObjects;
    private ThreeMatchSetting threeMatchSetting;
    private TemporaryNodes temporaryNodes;

    public Node SwapingNode { get; private set; }
    public Node SwapingNodeOp { get; private set; }


    public Swaper(Nodes nodes, Tiles tiles, TileObjects tileObjects, ThreeMatchSetting threeMatchSetting)
    {
        this.nodes = nodes;
        this.tiles = tiles;
        this.tileObjects = tileObjects;
        this.threeMatchSetting = threeMatchSetting;

        temporaryNodes = new TemporaryNodes();
    }



    public bool SwapCheckTwoWay(Node node, Swipe swipe)
    {
    
        bool check = false;
        bool checkOp = false;

        NodeType nodeType = node.GetNodeType();

        int x = node.X;
        int y = node.Y;

        int m_x = 0;
        int m_y = 0;

        (m_x, m_y) = UtilCoordinate.SwapDirToCoordinate(swipe);

        int op_x = x + m_x;
        int op_y = y + m_y;

       
        OneSwapItemNode oneSwapItemNode1 = nodes[x, y] as OneSwapItemNode;
        OneSwapItemNode oneSwapItemNode2 = nodes[op_x,op_y] as OneSwapItemNode;
        if (oneSwapItemNode1 != null || oneSwapItemNode2 != null)
        {
            return true;
        }



        if (SwapableNode(x,y) == false || SwapableNode(op_x,op_y) == false)
        {
            return false;
        }

       


        temporaryNodes.Setting(nodes.GetNodes());
        temporaryNodes.Swap(x, y, op_x, op_y);

        NodeType n1 = temporaryNodes[x, y].GetNodeType();
        NodeType n2 = temporaryNodes[op_x, op_y].GetNodeType();

        check = SwapCheck(n1, x, y);
        checkOp = SwapCheck(n2, op_x, op_y);

        return (check || checkOp);
    }


    private bool SwapWidthCheck(NodeType currentNodeType, int x, int y)
    {
        bool rightCheck = false;
        bool leftCheck = false;
        bool centerCheck = false;



        int rightCheckCount = 0;

        for (int i = 1; i < 3; ++i)
        {
            int next_x = x + i;
            if (next_x < threeMatchSetting.totalWidth)
            {
                Node checkNode = temporaryNodes[next_x, y];
                if (Satisfied3MatchPuzzleNode(checkNode, currentNodeType))
                { rightCheckCount++; }
            }
        }

        int leftCheckCount = 0;

        for (int i = 1; i < 3; ++i)
        {
            int next_x = x - i;

            if (next_x >= 0)
            {
                Node checkNode = temporaryNodes[next_x, y];
                if (Satisfied3MatchPuzzleNode(checkNode, currentNodeType))
                { leftCheckCount++; }
            }

        }

        int centerCheckCount = 0;
        for (int i = -1; i < 2; ++i)
        {
            if (i == 0)
            {
                continue;
            }
            int next_x = x + i;

            if (next_x >= 0 && next_x < threeMatchSetting.totalWidth)
            {
                Node checkNode = temporaryNodes[next_x, y];
                if (Satisfied3MatchPuzzleNode(checkNode, currentNodeType))
                { centerCheckCount++; }
            }

        }

        if (rightCheckCount >= 2)
        {
            rightCheck = true;
        }

        if (leftCheckCount >= 2)
        {
            leftCheck = true;
        }

        if (centerCheckCount >= 2)
        {
            centerCheck = true;
        }

       

        return (rightCheck || leftCheck || centerCheck);
    }


    private bool SwapCheck(NodeType currentNodeType, int x, int y)
    {
        return (SwapHeightCheck(currentNodeType, x, y) || SwapWidthCheck(currentNodeType, x, y));
    }

    private bool SwapHeightCheck(NodeType currentNodeType, int x, int y)
    {

        bool upCheck = false;
        bool downCheck = false;
        bool centerCheck = false;



        int upCheckCount = 0;

        for (int i = 1; i < 3; ++i)
        {
            int next_y = y + i;
            if (next_y < threeMatchSetting.totalHeight)
            {
                Node checkNode = temporaryNodes[x, next_y];
                if (Satisfied3MatchPuzzleNode(checkNode,currentNodeType))
                { upCheckCount++; }
            }
        }

        int downCheckCount = 0;

        for (int i = 1; i < 3; ++i)
        {
            int next_y = y - i;

            if (next_y >= 0)
            {
                Node checkNode = temporaryNodes[x, next_y];
                if (Satisfied3MatchPuzzleNode(checkNode, currentNodeType))
                {
                    downCheckCount++;
                }
            }

        }

        int centerCheckCount = 0;
        for (int i = -1; i < 2; ++i)
        {
            if (i == 0)
            {
                continue;
            }
            int next_y = y + i;
            if (next_y >= 0 && next_y < threeMatchSetting.totalHeight)
            {
                Node checkNode = temporaryNodes[x, next_y];
                if (Satisfied3MatchPuzzleNode(checkNode, currentNodeType))
                { centerCheckCount++; }
            }

        }

        if (upCheckCount >= 2)
        {
            upCheck = true;
        }

        if (downCheckCount >= 2)
        {
            downCheck = true;
        }

        if (centerCheckCount >= 2)
        {
            centerCheck = true;
        }

        Debug.Log($"Swap WidthCheck  <color>{currentNodeType}</color> " +
          $" , up : {upCheckCount}, down : {downCheckCount}, center : {centerCheckCount} ");

        return (upCheck || downCheck || centerCheck);
    }



    public void Swap(Swipe swipe, int x, int y)
    {
        int current_x = x;
        int current_y = y;

        int gx = 0;
        int gy = 0;


        (gx, gy) = UtilCoordinate.SwapDirToCoordinate(swipe);


        int next_x = current_x + gx;
        int next_y = current_y + gy;


        if (next_x >= 0 && next_x < threeMatchSetting.totalWidth &&
            next_y >= 0 && next_y < threeMatchSetting.totalHeight)
        {
            if (tiles[current_x, current_y].tileType == TileType.None)
            {
                return;
            }
            if (tiles[next_x, next_y].tileType == TileType.None)
            {
                return;
            }

            List<Node> nlist = nodes.GetNodes();

            int index1 = UtilCoordinate.CoordinateToindex(current_x, current_y, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);
            int index2 = UtilCoordinate.CoordinateToindex(next_x, next_y, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

            Node node1 = nlist[index1];
            Node node2 = nlist[index2];

            Node tempNode = node2;

            nlist[index2] = node1;
            nlist[index1] = tempNode;

            //nodes[current_x, current_y].SetSwipePoistion(tileObjects[current_x, current_y]);
            //nodes[next_x, next_y].SetSwipePoistion(tileObjects[next_x, next_y]);

            nodes[current_x, current_y].SwapNode(tileObjects[current_x, current_y], TouchEvaluator.Opposite(swipe));
            nodes[next_x, next_y].SwapNode(tileObjects[next_x, next_y], swipe);

            SwapingNodeOp = nodes[current_x, current_y];
            SwapingNode = nodes[next_x, next_y];


            OneSwapItemNode itemN1 = nodes[current_x, current_y] as OneSwapItemNode;
            OneSwapItemNode itemN2 = nodes[next_x, next_y] as OneSwapItemNode;

            if (itemN1 != null)
            {
                itemN1.Swap();
            }
            if(itemN2 != null)
            {
                itemN2.Swap();
            }

        }

    }

    



    private bool SwapableNode(int x,int y)
    {      
        return (nodes[x, y] != null && nodes[x,y].MovableNode() && nodes[x,y].ExplosionableNode());
    }

    private bool Satisfied3MatchPuzzleNode(Node node, NodeType nodetype)
    {
        return node != null && node.EqualsNodeType(nodetype) && node.ExplosionableNode();
    }
}
