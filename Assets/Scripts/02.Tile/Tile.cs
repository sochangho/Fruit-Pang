using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,
    Node,
}


public class Tile
{
    readonly public int x;
    readonly public int y;

    readonly public TileType tileType;


    public Tile(int x, int y, TileType tileType)
    {
        this.x = x;
        this.y = y;
        this.tileType = tileType;
    }


}
