using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileObject : MonoBehaviour
{
   [SerializeField]
   protected int _x;
   [SerializeField]
   protected int _y;
   
   public int X { get { return _x; } }
   public int Y { get { return _y; } }

   private void SetCoordinate(int x,int y)
   {
        _x = x;
        _y = y;
   }

   public void SetTileInitPosition(int x,int y, float width, float height)
   {
        SetCoordinate(x, y);
        transform.localPosition = new Vector2(x * width, y * height);
   }
   
    public Vector2 GetPosition()
    {
        return this.transform.localPosition;
    }
   

}
