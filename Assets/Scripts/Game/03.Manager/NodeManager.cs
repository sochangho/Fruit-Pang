using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeType
{
    Blue = 0,
    Red = 1,
    Green = 2,
    Orange = 3,
    Yellow = 4,

    BombBlue = 5,
    BombRed = 6,
    BombGreen = 7,
    BombOrange = 8,
    BombYellow = 9,

    Wall,    

    RandomFruit10,
    
    UpDir,
    DownDir,
    RightDir,
    LeftDir,


    None,
}


public enum EffectType
{

    Blue = 0,
    Red = 1,
    Green = 2,
    Orange = 3,
    Yellow = 4,
    Ice,
}



public class NodeManager : Singleton<NodeManager>
{
    private List<NodeType> nodeTypes;

    private List<NodeType> oneSwapItems;


    [SerializeField]
    private Node nodeBlue;

    [SerializeField]
    private Node nodeRed;
    
    [SerializeField]
    private Node nodeGreen;

    [SerializeField]
    private Node nodeYellow;

    [SerializeField]
    private Node nodeOrange;

    [SerializeField]
    private Bomb bombBlue;
    [SerializeField]
    private Bomb bombRed;
    [SerializeField]
    private Bomb bombGreen;
    [SerializeField]
    private Bomb bombYellow;
    [SerializeField]
    private Bomb bombOrange;

    [SerializeField]
    private Node nodeWall;

    [SerializeField]
    private RandomExplosionNode randomExplosionNode10;

    [SerializeField]
    private UpDirectionNode upDirectionNode;
    [SerializeField]
    private DownDirectionNode downDirectionNode;
    [SerializeField]
    private LeftDirectionNode leftDirectionNode;
    [SerializeField]
    private RightDirectionNode rightDirectionNode;



    [SerializeField]
    private StateObject Freeze;

    [SerializeField]
    private EffectObject redEffect;

    [SerializeField]
    private EffectObject greenEffect;

    [SerializeField]
    private EffectObject blueEffect;

    [SerializeField]
    private EffectObject orangeEffect;

    [SerializeField]
    private EffectObject yellowEffect;

    [SerializeField]
    private EffectObject iceEffect;

    [SerializeField]
    private ThreeMatchGame threeMatchGame; 
    
    protected override void Awake()
    {
        base.Awake();

    }

    public List<NodeType> GetAllNodeTypes()
    {
        if(nodeTypes == null)
        {
            nodeTypes = new List<NodeType>() 
            {
              NodeType.Blue,
              NodeType.Red,
              NodeType.Green,
              NodeType.Yellow,
              NodeType.Orange,
              NodeType.Wall,
            };
        }

        return nodeTypes;
    }

    public List<NodeType> GetNodeTypes()
    {
           List<NodeType> types = new List<NodeType>()
            {
              NodeType.Blue,
              NodeType.Red,
              NodeType.Green,
              NodeType.Yellow,
              NodeType.Orange,
              
            };

        return types;
    }

    public List<NodeType> GetOneSwapItems()
    {
        if (oneSwapItems == null)
        {
            oneSwapItems = new List<NodeType>()
            {
                NodeType.RandomFruit10,
                NodeType.UpDir,
                NodeType.DownDir,
                NodeType.LeftDir,
                NodeType.RightDir,
            };
        }

        return oneSwapItems;
    }



    public Node SelectNode(NodeType nodeType)
    {
        Node node = null;

        switch (nodeType)
        {
            case NodeType.Blue:
                node = nodeBlue;
                break;
            case NodeType.Red:
                node = nodeRed;
                break;
            case NodeType.Green:
                node = nodeGreen;
                break;
            case NodeType.Yellow:
                node = nodeYellow;
                break;
            case NodeType.Orange:
                node = nodeOrange;
                break;
//-----------------------------------------------------------------------
            case NodeType.BombBlue:
                node = bombBlue;
                break;
            case NodeType.BombRed:
                node = bombRed;
                break;
            case NodeType.BombGreen:
                node = bombGreen;
                break;
            case NodeType.BombOrange:
                node = bombOrange;
                break;
            case NodeType.BombYellow:
                node = bombYellow;
                break;
//----------------------------------------------------------------------------
            case NodeType.Wall:
                node = nodeWall;
                break;
            case NodeType.RandomFruit10:
                node = randomExplosionNode10;
                break;
            case NodeType.UpDir:
                node = upDirectionNode;
                break;

            case NodeType.DownDir:
                node = downDirectionNode;
                break;

            case NodeType.RightDir:
                node = rightDirectionNode;
                break;

            case NodeType.LeftDir:
                node = downDirectionNode;
                break;
        }

        return node;
    }

    public int GetTypeInt(NodeType nodeType)
    {
        switch (nodeType)
        {
            case NodeType.BombBlue:
                return 0;                
            case NodeType.BombRed:
                return 1;
            case NodeType.BombGreen:
                return 2;
            case NodeType.BombOrange:
                return 3;
            case NodeType.BombYellow:
                return 4;
        }

        return (int)nodeType;
    }

    public NodeType GetColorTypeToBomb(NodeType nodeType) 
    {
        switch (nodeType)
        {
            case NodeType.Blue:
                return NodeType.BombBlue;
            case NodeType.Red:
                return NodeType.BombRed;
            case NodeType.Green:
                return NodeType.BombGreen;
            case NodeType.Orange:
                return NodeType.BombOrange;
            case NodeType.Yellow:
                return NodeType.BombYellow;
        }


        return NodeType.BombBlue;
    }

    public StateObject SelectStateObject(NodeState nodeState)
    {
        StateObject effectObj = null;

        switch (nodeState)
        {
            case NodeState.None:
                effectObj = null;
                break;
            case NodeState.Freeze:
                effectObj = Freeze;
                break;
        }


        return effectObj;
    }
    
    
    public EffectObject SelectEffectObject(EffectType effectType) 
    {
        EffectObject effectObject = null ;

        switch (effectType)
        {
            case EffectType.Blue:
                effectObject = blueEffect;
                break;
            case EffectType.Green:
                effectObject = greenEffect;
                break;
            case EffectType.Red:
                effectObject = redEffect;
                break;
            case EffectType.Orange:
                effectObject = orangeEffect;
                break;
            case EffectType.Yellow:
                effectObject = yellowEffect;
                break;
            case EffectType.Ice:
                effectObject = iceEffect;
                break;
        }



        return effectObject;
 
    }

    public EffectObject GetColorEffect(NodeType nodeType)
    {

        EffectObject effectObject = null;

        EffectType effectType = EffectType.Blue;


        if(nodeType == NodeType.Blue || nodeType == NodeType.BombBlue)
        {
            effectType = EffectType.Blue;
        }
        else if (nodeType == NodeType.Green || nodeType == NodeType.BombGreen)
        {
            effectType = EffectType.Green;
        }
        else if (nodeType == NodeType.Red || nodeType == NodeType.BombRed)
        {
            effectType = EffectType.Red;
        }
        else if (nodeType == NodeType.Orange || nodeType == NodeType.BombOrange)
        {
            effectType = EffectType.Orange;
        }
        else if (nodeType == NodeType.Yellow || nodeType == NodeType.BombYellow)
        {
            effectType = EffectType.Yellow;
        }

        effectObject = ObjectPoolingManager.Instace.PopEffectObject(effectType);

        return effectObject;
    }


    public ThreeMatchGame GetThreeMatchGame() => threeMatchGame;


}
