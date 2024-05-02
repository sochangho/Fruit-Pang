using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMoveContoller : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.5f;

    private Coroutine coroutineMove = null;
    private Coroutine coroutineSwap = null;

    [HideInInspector]
    public MoveState moveState = MoveState.NoMove;

    public enum MoveState
    {
        NoMove,
        Moving,
        Complete,
    }

    public void MoveNode(Vector2 destinationPos, DirMove dirMove)
    {
        if(coroutineMove != null)
        {
            StopCoroutine(coroutineMove);
        }
        coroutineMove = StartCoroutine(MoveNodeRoutine(destinationPos, dirMove));
    }

    public void SwapNode(Vector2 targetPos, Swipe swipe )
    {
        if(coroutineSwap != null)
        {
            StopCoroutine(coroutineSwap);
        }
        coroutineSwap = StartCoroutine(MoveSwapRoutine(targetPos,swipe));
    }


    IEnumerator MoveSwapRoutine(Vector2 targetPos, Swipe swipe)
    {
        float s = 0.5f;
        float offset = 0.01f;

        moveState = MoveState.Moving;

        while (ClosedSwapPosition(targetPos,swipe,offset))
        {

            s = s + (5.5f * Time.deltaTime);

            Debug.Log("움직이는중");

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPos, s * Time.deltaTime);

            yield return null;

        }
        Debug.Log("완료");

        moveState = MoveState.Complete;

        transform.localPosition = targetPos;
    }



    IEnumerator MoveNodeRoutine(Vector2 destinationPos, DirMove dirMove)
    {
        float s = speed;

        float offset = 0.01f;



        while (ClosedPosition(destinationPos, dirMove, offset))
        {
            s = s + (5.5f * Time.deltaTime);

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, destinationPos, s * Time.deltaTime);

            yield return null;

        }

        transform.localPosition = destinationPos;

        dirMove.moveNodeCount--;

        if (dirMove.moveNodeCount < 0)
        {
            dirMove.moveNodeCount = 0;
        }

        yield return null;
    }


    private bool ClosedPosition(Vector2 destinationPos, DirMove dirMove, float offset)
    {
        if (dirMove.direction == NodeMover.Direction.Down)
        {
            return destinationPos.y + offset <= this.transform.localPosition.y;
        }
        else if (dirMove.direction == NodeMover.Direction.left)
        {
            return destinationPos.x + offset <= this.transform.localPosition.x;
        }
        else if (dirMove.direction == NodeMover.Direction.Right)
        {
            return destinationPos.x >= this.transform.localPosition.x + offset;
        }
        else
        {
            return destinationPos.y >= this.transform.localPosition.y + offset;
        }
    }

    private bool ClosedSwapPosition(Vector2 targetPos, Swipe swipe,float offset)
    {
        if (swipe == Swipe.DOWN)
        {
            Debug.Log("움직이기 Down");
            return targetPos.y + offset <= this.transform.localPosition.y;
        }
        else if (swipe == Swipe.LEFT)
        {
            Debug.Log("움직이기 LEFT");
            return targetPos.x + offset <= this.transform.localPosition.x;
        }
        else if (swipe == Swipe.RIGHT)
        {
            Debug.Log("움직이기 RIGHT");
            return targetPos.x >= this.transform.localPosition.x + offset;
        }
        else
        {
            Debug.Log("움직이기 UP");
            return targetPos.y >= this.transform.localPosition.y + offset;
        }
    }


}
