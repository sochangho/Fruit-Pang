using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Node의 Move 확인(할수 있는지 없는지)과 Node의 Move 함수 호출 
/// </summary>





public class NodeMover
{
    #region
    //private Nodes nodes;

    //private TileObjects tileObjects;

    //private TemporaryNodes temporaryNodes;

    //public int moveNodeCount = 0;


    //private List<int> refillCounts = new List<int>();

    //private Queue<Node>[] refillNodes = new Queue<Node>[GlobalSetting.TotalWidth];
    #endregion

    public enum Direction
    {
        Down,
        Up,
        left,
        Right,
    }


    private DownDirMove downDirMove;
    private LeftDirMove leftDirMove;
    private RightDirMove rightDirMove;
    private UpDirMove upDirMove;

    public DirMove dirMove;

    


    public NodeMover(Nodes nodes, TileObjects tileObjects )
    {
        #region
        //this.nodes = nodes;        
        //this.tileObjects = tileObjects;

        //for(int i = 0; i < GlobalSetting.TotalWidth; ++i)
        //{
        //    refillNodes[i] = new Queue<Node>();
        //}
        //
        #endregion

        downDirMove = new DownDirMove(nodes, tileObjects,Direction.Down);
        leftDirMove = new LeftDirMove(nodes,tileObjects,Direction.left);
        rightDirMove = new RightDirMove(nodes, tileObjects,Direction.Right);
        upDirMove = new UpDirMove(nodes, tileObjects,Direction.Up);


        ChangeDropDirection(Direction.Down);
    }

    public void ChangeDropDirection(Direction direction)
    {
        Debug.Log($"Change {direction}");

        if(direction == Direction.Down)
        {
            dirMove = downDirMove;
        }
        else if(direction == Direction.Up)
        {
            dirMove = upDirMove;
        }
        else if(direction == Direction.left)
        {
            dirMove = leftDirMove;
        }
        else
        {
            dirMove = rightDirMove;
        }

    }


    public void CheckEmptyAndMove()
    {
        dirMove.EmptyMove();
        #region 
        //moveNodeCount = 0;

        //if(temporaryNodes == null)
        //{
        //    temporaryNodes = new TemporaryNodes();
        //}

        //temporaryNodes.Setting(nodes.GetNodes());



        //Queue<Node> remainNodes = new Queue<Node>(); 

        //for(int x = 0; x < GlobalSetting.TotalWidth; ++x)
        //{

        //    remainNodes.Clear();

        //    for(int y = 0; y < GlobalSetting.TotalHeight; ++y)
        //    {

        //        if (temporaryNodes[x,y] != null)
        //        {                    
        //            remainNodes.Enqueue(temporaryNodes[x, y]);
        //            temporaryNodes[x,y] = null;
        //        }

        //    }


        //    int j = 0;
        //    while (remainNodes.Count > 0)
        //    {

        //        if (IsMovableTile_TemporaryNode(x,j))
        //        {
        //            Node n = remainNodes.Dequeue();

        //            if (!n.MovableNode()) // 벽?? 장애물 일 경우
        //            {
        //                j = n.Y;
        //            }

        //            temporaryNodes[x, j] = n;
        //        }


        //        j++;
        //    }

        //}


        //nodes.Setting(temporaryNodes.GetNodes());

        //List<Node> list = nodes.GetNodes();



        //for (int i = 0; i < list.Count; ++i)
        //{
        //    if (list[i] != null)
        //    {
        //        int x;
        //        int y;

        //        (x, y) = UtilCoordinate.IndexToCoordinate(i, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);

        //        list[i].MoveNode(tileObjects[x, y], this);
        //    }

        //}
        #endregion

    }

    public void RefillRandomNode()
    {

        dirMove.RefillNode();

        #region

        //moveNodeCount = 0;

        //refillCounts.Clear();

        //for(int i = 0; i < GlobalSetting.TotalWidth; ++i)
        //{
        //    refillNodes[i].Clear();
        //}



        //List<NodeType> nodeTypes = NodeManager.Instace.GetNodeTypes();

        //// 빈타일 Check
        //for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        //{
        //    refillCounts.Add(0);
        //    for(int y = 0; y < GlobalSetting.TotalHeight; ++y)
        //    {

        //        if (IsWallNode(x,y))
        //        {                   
        //            refillCounts[x] = 0;
        //            continue;
        //        }

        //        if (IsMovableTile_Node(x, y))
        //        {                    
        //            refillCounts[x]++;
        //        }
        //    }
        //}

        //int c = 0;
        //for (int i = 0; i < GlobalSetting.TotalWidth; ++i)
        //{
        //    c += refillCounts[i];
        //}



        //float intervalX = GlobalSetting.IntervalX;
        //float intervalY = GlobalSetting.IntervalY;


        ////타일 생성
        //for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        //{            
        //    Vector2 pos = new Vector2(x * intervalX, GlobalSetting.TotalHeight * intervalY);

        //    for (int i = 0; i < refillCounts[x]; ++i)
        //    {

        //        pos.y += intervalY;

        //        NodeType t;




        //        t = RandomManager.RandomDraw<NodeType>(nodeTypes);

        //        var clone = nodes.CreateNode(t);

        //        clone.transform.localPosition = pos;

        //        clone.SetCoordinate(-1, -1);

        //        refillNodes[x].Enqueue(clone);
        //    }
        //}




        ////채우기

        //for (int x = 0; x < GlobalSetting.TotalWidth; ++x)
        //{
        //    int y = GlobalSetting.TotalHeight - 1;

        //    for(; y >= 0; --y)
        //    {
        //        if (IsExitNode(x,y))
        //        {
        //            break;
        //        }
        //    }

        //    y++;

        //    while (refillNodes[x].Count > 0)
        //    {
        //       //var n =  refillNodes[x].Dequeue();

        //        if (IsMovableTile_Node(x, y))
        //        {
        //            var n = refillNodes[x].Dequeue();
        //            nodes[x, y] = n;
        //        }
        //        y++;
        //    }

        //}

        ////이동
        //List<Node> list = nodes.GetNodes();

        //for (int i = 0; i < list.Count; ++i)
        //{
        //    if (list[i] != null)
        //    {
        //        int x;
        //        int y;

        //        (x, y) = UtilCoordinate.IndexToCoordinate(i, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);

        //        list[i].MoveNode(tileObjects[x, y], this);
        //    }

        //}
        #endregion

    }

    #region
    //private bool IsMovableTile_TemporaryNode(int x,int y)
    //{
    //    bool isPossible = y < GlobalSetting.TotalHeight && tileObjects[x, y] != null && temporaryNodes[x, y] == null;

    //    return isPossible;
    //}

    //private bool IsMovableTile_Node(int x, int y)
    //{
    //    bool isPossible = y < GlobalSetting.TotalHeight && tileObjects[x, y] != null && nodes[x, y] == null;

    //    return isPossible;
    //}


    //private bool IsExitNode(int x, int y)
    //{
    //    return tileObjects[x, y] != null && nodes[x, y] != null;
    //}

    //private bool IsWallNode(int x, int y)
    //{
    //    return nodes[x, y] != null && !nodes[x, y].MovableNode();
    //}
    #endregion
}


