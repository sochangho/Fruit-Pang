using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDirMove : DirMove
{
    public UpDirMove(Nodes nodes, TileObjects tileObjects,NodeMover.Direction direction) :
      base(nodes, tileObjects,direction)
    {
        refillCounts = new List<int>();
        refillNodes = new Queue<Node>[GlobalSetting.TotalWidth];

        for (int i = 0; i < GlobalSetting.TotalWidth; ++i)
        {
            refillNodes[i] = new Queue<Node>();
        }

    }



    public override void EmptyMove()
    {
        moveNodeCount = 0;

        if (temporaryNodes == null)
        {
            temporaryNodes = new TemporaryNodes();
        }

        temporaryNodes.Setting(nodes.GetNodes());



        Queue<Node> remainNodes = new Queue<Node>();

        for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        {

            remainNodes.Clear();

            for (int y = GlobalSetting.TotalHeight-1; y >= 0; --y)
            {

                if (temporaryNodes[x, y] != null)
                {
                    remainNodes.Enqueue(temporaryNodes[x, y]);
                    temporaryNodes[x, y] = null;
                }

            }



            int j = GlobalSetting.TotalHeight - 1;
            while (remainNodes.Count > 0)
            {

                if (IsMovableTile_TemporaryNode(x, j))
                {
                    Node n = remainNodes.Dequeue();

                    if (!n.MovableNode()) // 寒?? 厘局拱 老 版快
                    {
                        j = n.Y;
                    }

                    temporaryNodes[x, j] = n;
                }


                j--;
            }

        }


        nodes.Setting(temporaryNodes.GetNodes());

        List<Node> list = nodes.GetNodes();



        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i] != null)
            {
                int x;
                int y;

                (x, y) = UtilCoordinate.IndexToCoordinate(i, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);

                list[i].MoveNode(tileObjects[x, y], this);
            }

        }

    }

    public override void RefillNode()
    {
        moveNodeCount = 0;

        refillCounts.Clear();

        for (int i = 0; i < GlobalSetting.TotalWidth; ++i)
        {
            refillNodes[i].Clear();
        }



        List<NodeType> nodeTypes = NodeManager.Instace.GetNodeTypes();

        // 后鸥老 Check
        for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        {
            refillCounts.Add(0);
            for (int y =GlobalSetting.TotalHeight-1; y>=0; --y)
            {

                if (IsWallNode(x, y))
                {
                    refillCounts[x] = 0;
                    continue;
                }

                if (IsMovableTile_Node(x, y))
                {
                    refillCounts[x]++;
                }
            }
        }

        //int c = 0;
        //for (int i = 0; i < GlobalSetting.TotalWidth; ++i)
        //{
        //    c += refillCounts[i];
        //}



        float intervalX = GlobalSetting.IntervalX;
        float intervalY = GlobalSetting.IntervalY;


        //鸥老 积己
        for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        {
            Vector2 pos = new Vector2(x * intervalX, -intervalY);

            for (int i = 0; i < refillCounts[x]; ++i)
            {

                pos.y -= intervalY;

                NodeType t;




                t = RandomManager.RandomDraw<NodeType>(nodeTypes);

                var clone = nodes.CreateNode(t);

                clone.transform.localPosition = pos;

                clone.SetCoordinate(-1, -1);

                refillNodes[x].Enqueue(clone);
            }
        }




        //盲快扁

        for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        {
            int y = 0;

            for (; y < GlobalSetting.TotalHeight; ++y)
            {
                if (IsExitNode(x, y))
                {
                    break;
                }
            }

            y--;

            while (refillNodes[x].Count > 0)
            {
                //var n =  refillNodes[x].Dequeue();

                if (IsMovableTile_Node(x, y))
                {
                    var n = refillNodes[x].Dequeue();
                    nodes[x, y] = n;
                }
                y--;
            }

        }

        //捞悼
        List<Node> list = nodes.GetNodes();

        for (int i = 0; i < list.Count; ++i)
        {
            if (list[i] != null)
            {
                int x;
                int y;

                (x, y) = UtilCoordinate.IndexToCoordinate(i, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);

                list[i].MoveNode(tileObjects[x, y], this);
            }

        }

    }


}
