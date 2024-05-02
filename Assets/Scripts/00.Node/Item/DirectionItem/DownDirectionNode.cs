using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownDirectionNode : OneSwapItemNode
{
    public override void ExplosionNode()
    {
        base.ExplosionNode();

        NodeManager.Instace.GetThreeMatchGame().
            ChangeDropDirection(NodeMover.Direction.Down);
    }
}
