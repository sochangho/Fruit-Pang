using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ItemNode
{

    public override void ExplosionNode()
    {
        base.ExplosionNode();

    }

    public override List<Coordinate> ItemDetect(Nodes nodes = null) { 
        List<Coordinate> coordinates = new List<Coordinate>();

        for(int x = -1; x < 2; ++x)
        {
            for(int y = -1; y < 2; ++y)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int cx = _x + x;
                int cy = _y + y;


                if (cx < 0 || cx >= GlobalSetting.TotalWidth)
                {
                    continue;
                }

                if (cy < 0 || cy >= GlobalSetting.TotalHeight)
                {
                    continue;
                }


                Coordinate coordinate = new Coordinate();
                coordinate.x = cx;
                coordinate.y = cy;

                coordinates.Add(coordinate);
            }


        }


        return coordinates;
    }
}
