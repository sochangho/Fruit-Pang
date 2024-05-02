using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemNode : TouchableNode
{

    public override void ExplosionNode()
    {
        base.ExplosionNode();
    }

    public abstract List<Coordinate> ItemDetect(Nodes nodes = null);


}
