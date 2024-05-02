using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHelper 
{

    private List<ThreeMatchPuzzles> threeMatchPuzzles_V =
        new List<ThreeMatchPuzzles>();
    private List<ThreeMatchPuzzles> threeMatchPuzzles_H = 
        new List<ThreeMatchPuzzles>();
    
    private Queue<NodeType>[] items;


    public void InitGame()
    {
        items = new Queue<NodeType>[GlobalSetting.TotalWidth];

        for(int i = 0; i < GlobalSetting.TotalWidth; i++)
        {
            items[i] = new Queue<NodeType>();
        }
    }

    public List<GameRunTimeItemInfo> DetectNode(List<Node> nodesVertical,List<Node> nodesHorizontal, List<Node> duplicationNodes )
    {

        Debug.Log($"<color=green> DetectNode V : {nodesVertical.Count} , H {nodesHorizontal.Count} <color>");

        VerticalSet(nodesVertical);
        HorizonSet(nodesHorizontal);


        List<GameRunTimeItemInfo> info = new List<GameRunTimeItemInfo>();


        foreach (var d in duplicationNodes)
        {
            int findH = threeMatchPuzzles_H.FindIndex(h => h.GetNode(d.X, d.Y) != null);
            int findV = threeMatchPuzzles_V.FindIndex(v => v.GetNode(d.X, d.Y) != null);

            if (findH != -1 && findV != -1)
            {
                threeMatchPuzzles_H.RemoveAt(findH);
                threeMatchPuzzles_V.RemoveAt(findV);
                // T, L, 십자 모양 탐색 완료 아이템 추가


                var list = NodeManager.Instace.GetOneSwapItems();
                var t = RandomManager.RandomDraw(list);

                GameRunTimeItemInfo gameRunTimeItemInfo = new GameRunTimeItemInfo();
                gameRunTimeItemInfo.x = d.X;
                gameRunTimeItemInfo.y = d.Y;
                gameRunTimeItemInfo.nodeType = t;

                info.Add(gameRunTimeItemInfo);
            }
        }

        foreach (var t in threeMatchPuzzles_H)
        {
            Debug.Log($"<color=green> threeMatchPuzzles_H {t.Count()} </color>");

            if(t.Count() > 3)
            {
                // 아이템 추가
                int x,y;
                (x, y) = t.GetRandomCoordinate();
                NodeType nodeType = t.GetNodeType();                
                var bombType = NodeManager.Instace.GetColorTypeToBomb(nodeType);

                GameRunTimeItemInfo gameRunTimeItemInfo = new GameRunTimeItemInfo();
                gameRunTimeItemInfo.x = x;
                gameRunTimeItemInfo.y = y;
                gameRunTimeItemInfo.nodeType = bombType;

                info.Add(gameRunTimeItemInfo);
            }
        }

        foreach(var t in threeMatchPuzzles_V)
        {
            Debug.Log($"<color=red> threeMatchPuzzles_V {t.Count()} </color>");

            if (t.Count() > 3)
            {
                //아이템 추가
                int x, y;

                NodeType nodeType = t.GetNodeType();
                (x, y) = t.GetRandomCoordinate();
                var bombType = NodeManager.Instace.GetColorTypeToBomb(nodeType);

                GameRunTimeItemInfo gameRunTimeItemInfo = new GameRunTimeItemInfo();
                gameRunTimeItemInfo.x = x;
                gameRunTimeItemInfo.y = y;
                gameRunTimeItemInfo.nodeType = bombType;


                info.Add(gameRunTimeItemInfo);
            }
        }

        threeMatchPuzzles_H.Clear();
        threeMatchPuzzles_V.Clear();


        return info;
    }

    public void VerticalSet(List<Node> nodesVertical)
    {
        threeMatchPuzzles_V.Clear();

        List<Node> tempNList = new List<Node>();
        tempNList.Clear();
        for (int i = 0; i < nodesVertical.Count; ++i)
        {
            if (tempNList.Count == 0)
            {
                tempNList.Add(nodesVertical[i]);
                continue;
            }

            if (tempNList[tempNList.Count - 1].EqualsNodeType(nodesVertical[i].GetNodeType())
                && (tempNList[tempNList.Count - 1].X + 1) == nodesVertical[i].X)
            {
                tempNList.Add(nodesVertical[i]);
            }
            else
            {
                threeMatchPuzzles_V.Add(new ThreeMatchPuzzles());
                threeMatchPuzzles_V[threeMatchPuzzles_V.Count - 1].AddRange(tempNList);
                tempNList.Clear();

                tempNList.Add(nodesVertical[i]);
            }
        }

        if (tempNList.Count > 0)
        {
            threeMatchPuzzles_V.Add(new ThreeMatchPuzzles());
            threeMatchPuzzles_V[threeMatchPuzzles_V.Count - 1].AddRange(tempNList);
            tempNList.Clear();
        }
    }


    public void HorizonSet(List<Node> nodesHorizontal)
    {
        threeMatchPuzzles_H.Clear();

        List<Node> tempNList = new List<Node>();
        tempNList.Clear();
        for (int i = 0; i < nodesHorizontal.Count; ++i)
        {
            if (tempNList.Count == 0)
            {
                tempNList.Add(nodesHorizontal[i]);
                continue;
            }

            if (tempNList[tempNList.Count - 1].EqualsNodeType(nodesHorizontal[i].GetNodeType())
                && (tempNList[tempNList.Count - 1].Y + 1) == nodesHorizontal[i].Y)
            {
                tempNList.Add(nodesHorizontal[i]);
            }
            else
            {
                threeMatchPuzzles_H.Add(new ThreeMatchPuzzles());
                threeMatchPuzzles_H[threeMatchPuzzles_H.Count - 1].AddRange(tempNList);
                tempNList.Clear();

                tempNList.Add(nodesHorizontal[i]);
            }
        }


        if (tempNList.Count > 0)
        {
            threeMatchPuzzles_H.Add(new ThreeMatchPuzzles());
            threeMatchPuzzles_H[threeMatchPuzzles_H.Count - 1].AddRange(tempNList);
            tempNList.Clear();
        }

    }    



}



public class ThreeMatchPuzzles
{
    List<Node> nodes = new List<Node>();

    public void AddPuzzle(Node node)
    {
        nodes.Add(node);
    } 

    public void AddRange(List<Node> list)
    {
        nodes.AddRange(list);
    }
    
    public int Count()
    {
        return nodes.Count;
    }
    
    public Node GetNode(int x, int y)
    {
        var node =  nodes.Find(n => n.X == x && n.Y == y);

        return node;
    }



    public NodeType GetNodeType()
    {
        return nodes[0].GetNodeType();
    } 

    public (int,int) GetRandomCoordinate()
    {
        var n =  RandomManager.RandomDraw(nodes);

        return (n.X, n.Y);
    } 

}


public struct GameRunTimeItemInfo
{
  public int x;
  public int y;
  public NodeType nodeType;

}
