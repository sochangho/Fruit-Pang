using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileObjects
{
    private ThreeMatchSetting threeMatchSetting;
    private Tiles tiles;
    private List<TileObject> tileObjectList = new List<TileObject>(); // ���ӳ��� ��ü Ÿ�� 

    public TileObject this[int x, int y]
    {
        get
        {

            if (x >= threeMatchSetting.totalWidth || y >= threeMatchSetting.totalHeight || x < 0 || y < 0)
            {
                Debug.Log($"<color=red>��ü : {threeMatchSetting.totalWidth}, {threeMatchSetting.totalHeight} // {x},{y}</color>");

                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

            TileObject to = tileObjectList[index];

            return to;
        }
    }


    public TileObjects(ThreeMatchSetting threeMatchSetting, Tiles tiles)
    {
        this.threeMatchSetting = threeMatchSetting;
        this.tiles = tiles;
    }


    public void InitGame()
    {
        InitTileObject();
    }



    private void InitTileObject()
    {
        var tileList = tiles.GetTiles();

        foreach (var t in tileList)
        {
            CreateTileObjectElement(t);
        }
    }


    private void CreateTileObjectElement(Tile tile)
    {

        int x = tile.x;
        int y = tile.y;

        TileObject tileObjectClone = null;
        if (tile.tileType != TileType.None)
        {
            tileObjectClone = GameObject.Instantiate(threeMatchSetting.tileObject, threeMatchSetting.boardTransform);
            tileObjectClone.SetTileInitPosition(x, y, threeMatchSetting.intervalX, threeMatchSetting.intervalY);
        }

        tileObjectList.Add(tileObjectClone);
    }

    public List<TileObject> GetTileObjects()
    {
        return tileObjectList;
    }

}
