using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDirectionNode : OneSwapItemNode
{
    public override void ExplosionNode()
    {
        base.ExplosionNode();

        NodeManager.Instace.GetThreeMatchGame().
            ChangeDropDirection(NodeMover.Direction.Right);
    }
}
