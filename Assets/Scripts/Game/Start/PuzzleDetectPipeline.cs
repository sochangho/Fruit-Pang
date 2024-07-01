using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDetectPipeline
{

    private Nodes nodes;
    private TileObjects tileObjects;

    private HorizonDetect horizonDetect;
    private VerticalDetect verticalDetect;

    private List<Node> deleteNodeList;

    private List<Node> h_filter;  // Wall 제거
    private List<Node> v_filter; // Wall 제거

    private List<Node> duplicationNodes; // 중복노드


    private ItemHelper itemHelper;


     

    public PuzzleDetectPipeline(Nodes nodes, TileObjects tileObjects)
    {
        this.nodes = nodes;
        this.tileObjects = tileObjects;

        horizonDetect = new HorizonDetect(nodes);
        verticalDetect = new VerticalDetect(nodes);

        deleteNodeList = new List<Node>();

        h_filter = new List<Node>();
        v_filter = new List<Node>();

        duplicationNodes = new List<Node>();

        itemHelper = new ItemHelper();
        itemHelper.InitGame();

    }


    public List<Node> DetectExposionNodes()
    {
        List<Node> h_nodeList = horizonDetect.DetectNodes();
        List<Node> v_nodeList = verticalDetect.DetectNodes();

        deleteNodeList.Clear();

        h_filter.Clear();  // Wall 제거
        v_filter.Clear(); // Wall 제거
        
        duplicationNodes.Clear(); // 중복노드

        VerticalAndHorizonDetect(h_nodeList, v_nodeList);

        ItemNodeDetect();
        
        ItemEffectNodeDetect();
        
        AroundNodeStateDetect();

        return deleteNodeList;
    }

    private void VerticalAndHorizonDetect(List<Node> h_nodeList, List<Node> v_nodeList)
    {
        foreach (var h in h_nodeList)
        {
            if (h.ExplosionableNode())
            {
                h_filter.Add(h);
            }
        }


        foreach (var v in v_nodeList)
        {
            if (v.ExplosionableNode())
            {
                v_filter.Add(v);
            }
        }


        foreach (var h in h_filter)
        {
            deleteNodeList.Add(h);
        }


        foreach (var v in v_filter)
        {
            var find = deleteNodeList.Find(n => n.X == v.X && n.Y == v.Y);

            if (find == null)
            {
                deleteNodeList.Add(v);
            }
            else
            {
                duplicationNodes.Add(v);
            }
        }

    }

    private void ItemNodeDetect()
    {
        var totalNode = nodes.GetNodes();

        foreach (var t in totalNode)
        {
            OneSwapItemNode oneSwapItemNode = t as OneSwapItemNode;

            if (oneSwapItemNode != null && oneSwapItemNode.IsSwap())
            {
                var find = deleteNodeList.Find(n => n.X == t.X && n.Y == t.Y);

                if (find == null)
                {
                    deleteNodeList.Add(t);
                }

            }

        }
    }
    private void ItemEffectNodeDetect()
    {
        List<Node> itemEffectNodes = new List<Node>();

        foreach (var d in deleteNodeList)
        {
            ItemNode itemNode = d as ItemNode;

            if (itemNode != null)
            {
                var list = itemNode.ItemDetect(nodes);

                if (list == null)
                {
                    continue;
                }

                for (int i = 0; i < list.Count; ++i)
                {
                    int x = list[i].x;
                    int y = list[i].y;

                    if (tileObjects[x, y] != null && nodes[x, y] != null && nodes[x, y].ExplosionableNode())
                    {
                        itemEffectNodes.Add(nodes[x, y]);
                    }
                }

            }

        }


        foreach (var n in itemEffectNodes)
        {
            var find = deleteNodeList.Find(d => d.X == n.X && d.Y == n.Y);

            if (find == null)
            {
                deleteNodeList.Add(n);
            }

        }

    }
    private void AroundNodeStateDetect()
    {
        List<Node> effectedNodes = new List<Node>();

        foreach (var n in deleteNodeList)
        {
            if (n.X + 1 >= 0 && n.X + 1 < GlobalSetting.TotalWidth)
            {
                int x = n.X + 1;
                int y = n.Y;

                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }

            }
            if (n.X - 1 >= 0 && n.X - 1 < GlobalSetting.TotalWidth)
            {
                int x = n.X - 1;
                int y = n.Y;

                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }

            }
            if (n.Y + 1 >= 0 && n.Y + 1 < GlobalSetting.TotalHeight)
            {
                int x = n.X;
                int y = n.Y + 1;
                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }

            }
            if (n.Y - 1 >= 0 && n.Y - 1 < GlobalSetting.TotalHeight)
            {
                int x = n.X;
                int y = n.Y - 1;
                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }
            }

        }


        foreach (var e in effectedNodes)
        {
            e.EffectEnd();
        }


    }


    public void DetectForItemCreate()
    {
        var infos = itemHelper.DetectNode(h_filter, v_filter, duplicationNodes);

        foreach (var i in infos)
        {

            var clone = nodes.CreateNode(i.nodeType);
            clone.SetCoordinate(i.x, i.y);
            clone.transform.localPosition = tileObjects[i.x, i.y].GetPosition();
            nodes[i.x, i.y] = clone;

        }

    }


}
