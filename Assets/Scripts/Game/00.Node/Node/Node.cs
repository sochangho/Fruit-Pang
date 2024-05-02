using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NodeState
{
    None,
    Freeze,
}


public class Node : MonoBehaviour
{
    private float speed = 1.5f;

    protected bool isExplosionableNode = true;
    protected bool isMovableNode = true;
    
    protected int _x;
    protected int _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    private NodeMoveContoller nodeMoveContoller;

    [SerializeField]
    protected NodeType nodeType;

    [SerializeField]
    protected NodeState nodeState;

   
    private NodeEffect nodeEffect;


    private void Awake()
    {
        nodeMoveContoller = GetComponent<NodeMoveContoller>();
    }

    public void SetCoordinate(int x,int y)
    {
        _x = x;
        _y = y;
    }

    
    public void SetSwipePoistion(TileObject tileObject)
    {
        SetCoordinate(tileObject.X, tileObject.Y);
        transform.localPosition = tileObject.GetPosition(); 
    }

    public virtual void MoveNode(TileObject targetTile, DirMove dirMove)
    {
        int destinationX = targetTile.X;
        int destinationY = targetTile.Y;

        if (_x != destinationX || _y != destinationY)
        {
            dirMove.moveNodeCount++;
            SetCoordinate(destinationX, destinationY);

            if(nodeMoveContoller != null) nodeMoveContoller.MoveNode(targetTile.GetPosition(),dirMove);
        }       
    }

    public void SwapNode(TileObject targetTile, Swipe swipe)
    {
        SetCoordinate(targetTile.X, targetTile.Y);

        if (nodeMoveContoller != null)
        {
            nodeMoveContoller.SwapNode(targetTile.GetPosition(), swipe);
        }
    }


    #region
    //IEnumerator MoveNodeRoutine(Vector2 destinationPos , DirMove dirMove)
    //{
    //    float s = speed;

    //    float offset = 0.01f;



    //    while(ClosedPostion(destinationPos,dirMove,offset))
    //    {
    //        s = s + (5.5f * Time.deltaTime);

    //        transform.localPosition = Vector2.MoveTowards(transform.localPosition, destinationPos, s * Time.deltaTime);

    //        yield return null;

    //    }

    //    transform.localPosition = destinationPos;  

    //    dirMove.moveNodeCount--;

    //    if(dirMove.moveNodeCount < 0)
    //    {
    //        dirMove.moveNodeCount = 0;
    //    }

    //    yield return null;        
    //}

    //public bool ClosedPostion(Vector2 destinationPos, DirMove dirMove,float offset)
    //{
    //   if(dirMove.direction == NodeMover.Direction.Down)
    //   {
    //        return destinationPos.y + offset <= this.transform.localPosition.y;
    //    }
    //   else if (dirMove.direction == NodeMover.Direction.left)
    //   {
    //        return destinationPos.x + offset <= this.transform.localPosition.x;
    //   }
    //   else if (dirMove.direction == NodeMover.Direction.Right)
    //   {
    //        return destinationPos.x >= this.transform.localPosition.x + offset;
    //   }
    //   else
    //   {
    //        return destinationPos.y >= this.transform.localPosition.y + offset;
    //   }


    //}
    #endregion

    public void EffectStart()
    {      
        nodeEffect = null;

        switch (this.nodeState)
        {
            case NodeState.None:
                nodeEffect = null;
                break;
            case NodeState.Freeze:
                nodeEffect = new FreezeEffect(this);
                break;
        }

        if(nodeEffect != null)
        {
            nodeEffect.EffectStart();
        }       
    }

    public void EffectEnd()
    {
        if(nodeEffect == null)
        {
            return;
        }

        if (nodeEffect.EffectEndCheck())
        {
            nodeEffect.EffectEnd();
        }
    }



    public bool EqualsNodeType(NodeType nodeType)
    {
      
         if(NodeManager.Instace.GetTypeInt(this.nodeType) == NodeManager.Instace.GetTypeInt(nodeType))
         {
            return true;
         }  
        return false;
    }

    public NodeType GetNodeType() => nodeType;
    public void SetNodeState(NodeState nodeState) => this.nodeState = nodeState; 
    public virtual void ExplosionNode() { }
    public virtual bool MovableNode() => isMovableNode;
    public virtual bool ExplosionableNode() => isExplosionableNode;
    public void SetMovalble(bool isMovable) => isMovableNode = isMovable;
    public void SetExplosion(bool isExplosion) => isExplosionableNode = isExplosion;
    public NodeMoveContoller.MoveState GetMoveState() => nodeMoveContoller.moveState;
    public void SetMoveState(NodeMoveContoller.MoveState moveState) => nodeMoveContoller.moveState = moveState;

}
