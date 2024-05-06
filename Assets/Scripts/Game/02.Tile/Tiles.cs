using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tiles
{


    private ThreeMatchSetting threeMatchSetting;
    private StageLoad stageLoad;

    private List<Tile> tileList = new List<Tile>();
 
    public Tile this[int x, int y]
    {

        get
        {
             
            if(x >= threeMatchSetting.totalWidth || y >= threeMatchSetting.totalHeight || x < 0 || y < 0)
            {

                throw new IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

            Tile t = tileList[index];

            return t;
        }

    }




    public Tiles(ThreeMatchSetting threeMatchSetting , StageLoad stageLoad)
    {
        this.threeMatchSetting = threeMatchSetting;
        this.stageLoad = stageLoad;
    }


    public void InitGame()
    {
        InitTileInfomation();
    
    }


    private void InitTileInfomation()
    {
        //int totalCount = threeMatchSetting.totalWidth * threeMatchSetting.totalHeight; // 전체 타일 개수

        //for (int i = 0; i < totalCount; ++i)
        //{


        //    int index = UtilCoordinate.CoordinateToindex(5, 5, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);

        //    if (index == i)
        //    {
        //        CreateTileElementInfomation(i, TileType.None);
        //    }
        //    else
        //    {
        //        CreateTileElementInfomation(i, TileType.Node);
        //    }
        //}

        tileList = stageLoad.GetTiles();

    }



    private void CreateTileElementInfomation(int index, TileType tileType)
    {
        int y;
        int x;

        (x, y) = UtilCoordinate.IndexToCoordinate(index, threeMatchSetting.totalWidth, threeMatchSetting.totalHeight);
        Debug.Log($" {index} 생성 위치 : { x },{ y }");
        Tile tile = new Tile(x, y, tileType);

        tileList.Add(tile);
    }




    public List<Tile> GetTiles()
    {
        return tileList;
    }


}
