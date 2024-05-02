using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detect
{
    protected Nodes nodes; 
    public Detect(Nodes nodes)
    {
        this.nodes = nodes;
    }

    abstract public List<Node> DetectNodes();
}
