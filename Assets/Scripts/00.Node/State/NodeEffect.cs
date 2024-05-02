using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeEffect 
{
    protected Node node;
    public NodeEffect(Node node){this.node = node;}

    public abstract void EffectStart();
    public abstract void EffectEnd();
    public abstract bool EffectEndCheck();

}
