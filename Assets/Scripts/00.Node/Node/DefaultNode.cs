using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DefaultNode : TouchableNode
{
    public override void ExplosionNode()
    {
        base.ExplosionNode();
        Effect();
    }
    public void Effect()
    {
        var clone = NodeManager.Instace.GetColorEffect(nodeType);
        clone.transform.localPosition = this.transform.localPosition;
    }


}
