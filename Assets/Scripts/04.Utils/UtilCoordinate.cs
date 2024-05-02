using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilCoordinate
{
    static public int CoordinateToindex(int x,int y,int totalWidth, int totalHeight)
    {
        if(x >= totalWidth || y >= totalHeight)
        {
            Debug.LogError("¹üÀ§¹þ¾î³²");
            return 0;
        }

        int index = totalWidth * y + x;

       
        return totalWidth * y + x;
    }

    static public (int,int) IndexToCoordinate(int index, int totalWidth, int totalHeight)
    {

        if (index >= totalWidth * totalHeight)
        {
            Debug.LogError("¹üÀ§¹þ¾î³²");
            return (0,0);
        }
        
        int y = index / totalWidth;
        int x = index % totalWidth;

        return (x, y);

    }

    static public (int,int) SwapDirToCoordinate(Swipe swipe)
    {
        int x = 0;
        int y = 0;

        switch (swipe)
        {
            case Swipe.RIGHT:
                x = 1;
                y = 0;
                break;
            case Swipe.UP:
                x = 0;
                y = 1;
                break;
            case Swipe.LEFT:
                x = -1;
                y = 0;
                break;
            case Swipe.DOWN:
                x = 0;
                y = -1;
                break;
            case Swipe.NA:
                x = 0;
                y = 0;
                break;
        }
        return (x, y);
    }



  
}
