using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSwapItemNode : ItemNode
{
    protected bool isSwap = false;

    public void OnEnable()
    {
        isSwap = false;
    }


    public virtual void Swap()
    {
        isSwap = true;
    }

    public bool IsSwap()
    {
        return isSwap;
    }

    public override List<Coordinate> ItemDetect(Nodes nodes = null)
    {
        return null;
    }
}
