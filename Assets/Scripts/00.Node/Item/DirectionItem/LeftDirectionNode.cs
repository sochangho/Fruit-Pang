using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDirectionNode : OneSwapItemNode
{
    public override void ExplosionNode()
    {
        base.ExplosionNode();

        NodeManager.Instace.GetThreeMatchGame().
            ChangeDropDirection(NodeMover.Direction.left);
    }
}
