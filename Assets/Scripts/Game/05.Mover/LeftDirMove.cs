using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDirMove : DirMove
{

    public LeftDirMove(Nodes nodes, TileObjects tileObjects,NodeMover.Direction direction) :
       base(nodes, tileObjects,direction)
    {
        refillCounts = new List<int>();
        refillNodes = new Queue<Node>[GlobalSetting.TotalHeight];

        for (int i = 0; i < GlobalSetting.TotalHeight; ++i)
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

        for (int y = 0; y < GlobalSetting.TotalHeight; ++y)
        {

            remainNodes.Clear();

            for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
            {

                if (temporaryNodes[x, y] != null)
                {
                    remainNodes.Enqueue(temporaryNodes[x, y]);
                    temporaryNodes[x, y] = null;
                }

            }


            int j = 0;
            while (remainNodes.Count > 0)
            {

                if (IsMovableTile_TemporaryNode(j, y))
                {
                    Node n = remainNodes.Dequeue();

                    if (!n.MovableNode()) // ��?? ��ֹ� �� ���
                    {
                        j = n.X;
                    }

                    temporaryNodes[j, y] = n;
                }


                j++;
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

        for (int i = 0; i < GlobalSetting.TotalHeight; ++i)
        {
            refillNodes[i].Clear();
        }



        List<NodeType> nodeTypes = NodeManager.Instace.GetNodeTypes();

        // ��Ÿ�� Check
        for (int y = 0; y < GlobalSetting.TotalHeight; ++y)
        {
            refillCounts.Add(0);
            for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
            {

                if (IsWallNode(x, y))
                {
                    refillCounts[y] = 0;
                    continue;
                }

                if (IsMovableTile_Node(x, y))
                {
                    refillCounts[y]++;
                }
            }
        }

        //int c = 0;
        //for (int i = 0; i < GlobalSetting.TotalHeight; ++i)
        //{
        //    c += refillCounts[i];
        //}



        float intervalX = GlobalSetting.IntervalX;
        float intervalY = GlobalSetting.IntervalY;


        //Ÿ�� ����
        for (int y = 0; y < GlobalSetting.TotalHeight; ++y)
        {
            Vector2 pos = new Vector2(GlobalSetting.TotalWidth * intervalX, y * intervalY);

            for (int i = 0; i < refillCounts[y]; ++i)
            {

                pos.x += intervalX;

                NodeType t;




                t = RandomManager.RandomDraw<NodeType>(nodeTypes);

                var clone = nodes.CreateNode(t);

                clone.transform.localPosition = pos;

                clone.SetCoordinate(-1, -1);

                refillNodes[y].Enqueue(clone);
            }
        }




        //ä���

        for (int y = 0; y < GlobalSetting.TotalHeight; ++y)
        {
            int x = GlobalSetting.TotalWidth - 1;

            for (; x >= 0; --x)
            {
                if (IsExitNode(x, y))
                {
                    break;
                }
            }

            x++;

            while (refillNodes[y].Count > 0)
            {
                //var n =  refillNodes[x].Dequeue();

                if (IsMovableTile_Node(x, y))
                {
                    var n = refillNodes[y].Dequeue();
                    nodes[x, y] = n;
                }
                x++;
            }

        }

        //�̵�
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
