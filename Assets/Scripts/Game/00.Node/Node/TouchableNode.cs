using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableNode : Node
{
    public override void ExplosionNode()
    {
        ObjectPoolingManager.Instace.PushNode(this);
    
    }
        
}
