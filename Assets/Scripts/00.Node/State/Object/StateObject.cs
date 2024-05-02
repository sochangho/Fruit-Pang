using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateObject : MonoBehaviour
{
    [SerializeField]
    protected NodeState nodeType;

    public NodeState GetNodeState() => nodeType;
   
    public virtual void EffectObjectInteraction(int num) { }


}
