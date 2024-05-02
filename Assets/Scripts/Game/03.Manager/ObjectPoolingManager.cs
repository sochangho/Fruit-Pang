using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{   
    private Dictionary<int, PoolingObject> dicPoolingObjects = 
        new Dictionary<int, PoolingObject>();

    private Dictionary<int, PoolingStateObject> dicStateObjects = 
        new Dictionary<int, PoolingStateObject>();

    private Dictionary<int, PoolingEffectObject> dicEffectObjects = 
        new Dictionary<int, PoolingEffectObject>();


    [SerializeField]
    private Transform boardTansform;

    protected override void Awake()
    {
        base.Awake();
        //InitGame();
    }
    
    public void InitGame()
    {
        int len = Enum.GetNames(typeof(NodeType)).Length;

        for(int t = 0; t < len-1; ++t)
        {
            NodeType nodeType = (NodeType)t;
            dicPoolingObjects.Add(t, new PoolingObject(nodeType, boardTansform));
        }

        foreach(var d in dicPoolingObjects)
        {
            for(int i = 0; i < 20; ++i)
            {
                d.Value.Create();
            }
        }

        len = Enum.GetNames(typeof(NodeState)).Length;

        for(int i = 1; i < len; ++i)
        {
            NodeState nodeState = (NodeState)i;
            dicStateObjects.Add(i, new PoolingStateObject(nodeState , boardTansform));
        }


        foreach(var d in dicStateObjects)
        {
            for(int i = 0; i < 10; ++i)
            {
                d.Value.Create();
            }
        }


        len = Enum.GetNames(typeof(EffectType)).Length;

        for (int i = 0; i < len; ++i)
        {
            EffectType et = (EffectType)i;
            dicEffectObjects.Add(i, new PoolingEffectObject(et, boardTansform));
        }


        foreach (var d in dicEffectObjects)
        {
            for (int i = 0; i < 10; ++i)
            {
                d.Value.Create();
            }
        }

    }
//-----------------------------------------------------------------
    public Node PopNode(NodeType nodeType)
    {
        int t = (int)nodeType;

        return  dicPoolingObjects[t].Pop();
    }

    public void PushNode(Node node)
    {
        int t = (int)node.GetNodeType();
        dicPoolingObjects[t].Push(node);

    }
//-----------------------------------------------------------------
    public StateObject PopStateObject(NodeState nodeState)
    {
        int t = (int)nodeState;
        return dicStateObjects[t].Pop();
    }

    public void PushStateObject(StateObject stateObject)
    {
        int t = (int)stateObject.GetNodeState();
        dicStateObjects[t].Push(stateObject);
    }

//-----------------------------------------------------------------
    public EffectObject PopEffectObject(EffectType effectType)
    {
        int t = (int)effectType;
        return dicEffectObjects[t].Pop();
    }

    public void PushEffectObject(EffectObject effectObject)
    {
        int t = (int)effectObject.GetEffectType();
        dicEffectObjects[t].Push(effectObject);
    }
//-----------------------------------------------------------------

}



public class PoolingObject
{
    private NodeType nodeType;

    private Transform boardTransform;

    private Stack<Node> objects = new Stack<Node>();


    public PoolingObject(NodeType nodeType, Transform boardTransform)
    {
        this.nodeType = nodeType;
        this.boardTransform = boardTransform;
    }



    public Node Pop()
    {

        if(objects.Count == 0)
        {
           var n  =  NodeManager.Instace.SelectNode(nodeType);
           var clone =  GameObject.Instantiate(n, boardTransform);
           clone.gameObject.SetActive(true);
           return clone;
        }


        var node = objects.Pop();

        node.gameObject.SetActive(true);

        return node;
    }

    public void Push(Node node)
    {
        node.gameObject.SetActive(false);
        objects.Push(node);
    }

    public void Create()
    {
        var n = NodeManager.Instace.SelectNode(nodeType);
        
        var clone = GameObject.Instantiate(n, boardTransform);
        
        clone.gameObject.SetActive(false);

        objects.Push(clone);
    }

}
public class PoolingStateObject
{

    NodeState nodeState;

    Transform boardTransform;

    Stack<StateObject> effectObjects = new Stack<StateObject>();

    public PoolingStateObject(NodeState nodeState ,Transform boardTransform)
    {
        this.nodeState = nodeState;
        this.boardTransform = boardTransform;
    }

    public StateObject Pop()
    {

        if (effectObjects.Count == 0)
        {
            var n = NodeManager.Instace.SelectStateObject(nodeState);
            var clone = GameObject.Instantiate(n, boardTransform);
            clone.gameObject.SetActive(true);
            return clone;
        }


        var node = effectObjects.Pop();

        node.gameObject.SetActive(true);

        return node;
    }

    public void Push(StateObject effectObject)
    {
        effectObject.gameObject.SetActive(false);
        effectObjects.Push(effectObject);
    }

    public void Create()
    {
        var n = NodeManager.Instace.SelectStateObject(nodeState);

        var clone = GameObject.Instantiate(n, boardTransform);

        clone.gameObject.SetActive(false);

        effectObjects.Push(clone);
    }

}
public class PoolingEffectObject
{

    EffectType  effectType;

    Transform boardTransform;

    Stack<EffectObject> effectObjects = new Stack<EffectObject>();

    public PoolingEffectObject(EffectType effectType, Transform boardTransform)
    {
        this.effectType = effectType;
        this.boardTransform = boardTransform;
    }

    public EffectObject Pop()
    {

        if (effectObjects.Count == 0)
        {
            var n = NodeManager.Instace.SelectEffectObject(effectType);
            var clone = GameObject.Instantiate(n, boardTransform);
            clone.gameObject.SetActive(true);
            return clone;
        }


        var node = effectObjects.Pop();

        node.gameObject.SetActive(true);

        return node;
    }

    public void Push(EffectObject effectObject)
    {
        effectObject.gameObject.SetActive(false);
        effectObjects.Push(effectObject);
    }

    public void Create()
    {
        var n = NodeManager.Instace.SelectEffectObject(effectType);

        var clone = GameObject.Instantiate(n, boardTransform);

        clone.gameObject.SetActive(false);

        effectObjects.Push(clone);
    }

}



