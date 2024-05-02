using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDetect : Detect
{

    public VerticalDetect(Nodes nodes) : base(nodes)
    {}

    public override List<Node> DetectNodes()
    {

        List<Node> nodeList = new List<Node>();
        List<Node> nl = new List<Node>();

        int x = 0;
        int y = 0;



        for (; x < GlobalSetting.TotalWidth; ++x)
        {
            y = 0;

            Node currentNode = null;

            while (y >= 0 && y < GlobalSetting.TotalHeight)
            {
                currentNode = nodes[x, y];

                #region ¼öÁ¤ Àü
                if (currentNode == null)
                {
                    if (nl.Count > 2)
                    {
                        nodeList.AddRange(nl);
                    }
                    nl.Clear();

                    y++;
                    continue;
                }


                if (nl.Count == 0)
                {
                    nl.Add(currentNode);
                }
                else
                {
                    if (!nl[nl.Count - 1].EqualsNodeType(currentNode.GetNodeType()))
                    {
                        if (nl.Count > 2)
                        {
                            nodeList.AddRange(nl);
                        }
                        nl.Clear();
                    }

                    nl.Add(currentNode);
                }
                #endregion
                y++;
            }

            if (nl.Count > 2)
            {
                nodeList.AddRange(nl);
            }
            nl.Clear();
        }

        return nodeList;
    }



}
