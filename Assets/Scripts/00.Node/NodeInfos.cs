using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 초기 세팅
/// </summary>
public class NodeInfos
{
   
    private List<NodeInfomation> nodeInfomationList = new List<NodeInfomation>();

    public void InfoTestLoad(Tiles tiles)
    {
        var tList = tiles.GetTiles();

        List<NodeType> nodeTypes = NodeManager.Instace.GetNodeTypes();


        foreach (var t in tList)
        {
            var type =  RandomManager.RandomDraw<NodeType>(nodeTypes);

            if(t.tileType != TileType.None)
            {
                if (t.y == 4)
                {
                    nodeInfomationList.Add(new NodeInfomation(t.x, t.y, type,NodeState.Freeze));
                }
                else
                {
                    nodeInfomationList.Add(new NodeInfomation(t.x, t.y, type));
                }
            }
            else
            {
               nodeInfomationList.Add(null);
            }
  
        }

        for (int i = 0; i < GlobalSetting.TotalWidth; ++i)
        {
            int index;
            index = UtilCoordinate.
                CoordinateToindex(i, 6, GlobalSetting.TotalWidth, GlobalSetting.TotalHeight);

            nodeInfomationList[index] = new NodeInfomation(i, 6, NodeType.Wall);
        }
    }

    public List<NodeInfomation> GetNodeInfomations()
    {
        return nodeInfomationList;
    }

}
