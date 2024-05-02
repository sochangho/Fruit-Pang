using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Nodes
{
    private TileObjects tileObjects;

    private NodeInfos nodeInfos;

    private ThreeMatchSetting threeMatchSetting;

    private List<Node> nodeList = new List<Node>();

    public Node this[int x,int y]
    {
        get
        {

            if (x >= threeMatchSetting.totalWidth || y >= threeMatchSetting.totalHeight || x < 0 || y < 0)
            {

                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

            Node n = nodeList[index];

            return n;
        }
        set
        {
            if (x >= GlobalSetting.TotalWidth || y >= GlobalSetting.TotalHeight || x < 0 || y < 0)
            {

                Debug.Log($"ÁÂÇ¥ : {x} , {y}");
                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);


            nodeList[index] = value;

        }
    }


    public Nodes(ThreeMatchSetting threeMatchSetting, TileObjects tileObjects, NodeInfos nodeInfos )
    {
        this.tileObjects = tileObjects;
        this.nodeInfos = nodeInfos;
        this.threeMatchSetting = threeMatchSetting;
    } 

    public void InitGame()
    {
        InitNodes();
    }

    public void InitNodes()
    {
        var infoList = nodeInfos.GetNodeInfomations();

        foreach(var info in infoList)
        {

            if (info == null)
            {
                 nodeList.Add(null);
            }
            else
            {

                var t = info.nodeType;
                var clone = CreateNode(t);
                clone.SetCoordinate(info.x, info.y);
                clone.transform.localPosition = tileObjects[info.x, info.y].GetPosition();
                
                clone.SetNodeState(info.nodeState);
                clone.EffectStart();
                
                nodeList.Add(clone);
            }
        }
    }



    public Node CreateNode(NodeType nodeType)
    {
        return ObjectPoolingManager.Instace.PopNode(nodeType);
    }



    public List<Node> GetNodes()
    {
        return nodeList;
    }

    public void ExplosionNode(Node node)
    {
        node.ExplosionNode();
        int index =  UtilCoordinate.CoordinateToindex(node.X, node.Y,threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);
        try {
            nodeList[index] = null;
        }
        catch
        {
            int x, y;
            (x,y) =  UtilCoordinate.IndexToCoordinate(index, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

            Debug.Log($"Dsdsdsdsdsdsd {x},{y}");
        }
    }

    public void Setting(List<Node> nodes)
    {
        nodeList.Clear();
        nodeList.AddRange(nodes);
    }

  

}

//³ëµå º¹»ç ¿ëµµ

public class TemporaryNodes
{
    private List<Node> nodeList = new  List<Node>();
 
    public Node this[int x, int y]
    {
        get
        {

            if (x >= GlobalSetting.TotalWidth || y >= GlobalSetting.TotalHeight || x < 0 || y < 0)
            {

                Debug.Log($"ÁÂÇ¥ : {x} , {y}");
                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);



            Node n = nodeList[index];

        

            return n;
        }
        set
        {
            if (x >= GlobalSetting.TotalWidth || y >= GlobalSetting.TotalHeight || x < 0 || y < 0)
            {

                Debug.Log($"ÁÂÇ¥ : {x} , {y}");
                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);


            nodeList[index] = value;

        }



    }

 

    public void Setting(List<Node> nodeList)
    {


        this.nodeList.Clear();

        this.nodeList.AddRange(nodeList);

    }

    public void Swap(int x1, int y1, int x2,int y2)
    {
        if(x1 < 0 && x1 >= GlobalSetting.TotalWidth
            || y1 < 0 && y1 >= GlobalSetting.TotalHeight ||
            x2 < 0 && x2 >= GlobalSetting.TotalWidth
            || y2 < 0 && y2 >= GlobalSetting.TotalHeight
            )
        {
            throw new IndexOutOfRangeException();
        }

        int index1 = UtilCoordinate.CoordinateToindex(x1, y1, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);
        int index2 = UtilCoordinate.CoordinateToindex(x2, y2, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);


        Node node1 = nodeList[index1];
        Node node2 = nodeList[index2];

        Node temp = node2;

        nodeList[index2] = node1;
        nodeList[index1] = temp;

    }

    public List<Node> GetNodes()
    {
        return nodeList;
    }

   

}