using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Swipe
{
    NA    = -1,
    RIGHT = 0,
    UP    = 1,
    LEFT  = 2,
    DOWN  = 3,
}





public static class TouchEvaluator { 
    //UP : 45~ 135, LEFT : 135 ~ 225, DOWN : 225 ~ 315, RIGHT : 0 ~ 45, 0~315

    public static Swipe EValSwipeDir(Vector2 vtStart, Vector2 vtEnd)
    {
        float angle = EvalDragAngle(vtStart, vtEnd);


        if(angle < 0)
        {
            return Swipe.NA;
        }

        int swipe = (((int)angle + 45) % 360) / 90;

        switch (swipe)
        {
            case 0: return Swipe.RIGHT;
            case 1: return Swipe.UP;
            case 2: return Swipe.LEFT;
            case 3: return Swipe.DOWN;
        }




        return Swipe.NA;
    }


    public static float EvalDragAngle(Vector2 vtStart, Vector2 vtEnd)
    {
        Vector2 dragDirection = vtEnd - vtStart;

        if (dragDirection.magnitude <= 0.2f)
            return -1f;

        float aimAngle = Mathf.Atan2(dragDirection.y, dragDirection.x);
        if(aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        return aimAngle * Mathf.Rad2Deg;
    }

   public static Swipe Opposite(Swipe swipe)
   {
        switch (swipe)
        {
            case Swipe.RIGHT:                
                return Swipe.LEFT;
            case Swipe.UP:
                return Swipe.DOWN;
            case Swipe.LEFT:
                return Swipe.RIGHT;                                
            case Swipe.DOWN:
                return Swipe.UP;
        }

        return Swipe.NA;
    }


}
