using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonDetect : Detect
{

    public HorizonDetect(Nodes nodes ) : base(nodes)
    {}

    public override List<Node> DetectNodes()
    {
        List<Node> nodeList = new List<Node>();
        List<Node> nl = new List<Node>();

        int x = 0;
        int y = 0;


       
        for(; y < GlobalSetting.TotalHeight; ++y)
        {
           x = 0;
      
           Node currentNode = null; 
            
           while(x >= 0 && x < GlobalSetting.TotalWidth)
           {
                currentNode = nodes[x, y];

            
                if (currentNode == null)
                {
                    if (nl.Count > 2)
                    {
                        nodeList.AddRange(nl);
                    }
                    nl.Clear();

                    x++;
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

               
                x++;             
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
