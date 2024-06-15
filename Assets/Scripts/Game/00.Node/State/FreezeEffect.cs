using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : NodeEffect
{
    private int count = 2;

    private bool origineExplosionableState;
    private bool origineMovableState;

    private StateObject effectObject;

    public FreezeEffect(Node node) : base(node){}

    public override void EffectStart()
    {
        origineExplosionableState = node.ExplosionableNode();
        origineMovableState = node.MovableNode();

        node.SetExplosion(false);
        node.SetMovalble(false);

        //Freeze Object create;

        var freeze = ObjectPoolingManager.Instace.PopStateObject(NodeState.Freeze);

        effectObject = freeze;

        effectObject.transform.localPosition = node.transform.localPosition;

    }


    public override void EffectEnd()
    {
        base.EffectEnd();

        node.SetExplosion(origineExplosionableState);
        node.SetMovalble(origineMovableState);

        effectObject.StatePlaySound();

        ObjectPoolingManager.Instace.PushStateObject(effectObject);
    }

    public override bool EffectEndCheck()
    {
        count--;

        effectObject.EffectObjectInteraction(count);

        return count == 0;
    }

   
}
