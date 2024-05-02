using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedNode
{
    private  Node node;

    private List<DetectedNode> detectedNodes = new List<DetectedNode>();

    


    public List<Node> GetNodes()
    {
        List<Node> nodes = new List<Node>(); 

        for(int i = 0; i < detectedNodes.Count; ++i)
        {
             nodes.AddRange(detectedNodes[i].GetNodes());
        }

        nodes.Add(node);

        return nodes;
    }


}
