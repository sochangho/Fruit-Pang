using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BlockPos
{
    public int x;
    public int y;
} 


public class ThreeMatchStageController : MonoBehaviour
{
    private InputManager inputManager;

    private bool bTouchDown; //입력상태 터치 플래그 
    private Node bNode;
    private Vector2 clickPos;

    public void InitGame()
    {
        inputManager = new InputManager(this.gameObject.transform);
       
    }


    private void Update()
    {
        OnInputHandler();
    }


 
    private Node GetNode(Vector2 point )
    {

        Node node = null;

        RaycastHit2D hit =Physics2D.Raycast(point,transform.forward,15);

        if (hit) 
        {
          node =  hit.transform.GetComponent<Node>();              
        }

        return node;
    } 


    private void OnInputHandler()
    {
        if(!bTouchDown && inputManager.IsTouchDown)
        {
          

            Vector2 point = inputManager.touch2WorldPostion;

            Node node = GetNode(point);

            if(node != null)
            {                
                clickPos = point;
                bNode = node;                
                bTouchDown = true;
            }


       
        }
        else if(bTouchDown && inputManager.IsTouchUp)
        {

          
            if (bNode != null)
            {
                

                Vector2 point = inputManager.touch2WorldPostion;

                Swipe swipeDir = inputManager.EvalSwipeDir(clickPos, point);
                               
                bNode = null;
            }
            bTouchDown = false;
        }

        
    }



}



