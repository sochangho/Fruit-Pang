using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ThreeMatchSetting
{
    
    public TileObject tileObject;
   
    public Transform boardTransform;

    public int totalWidth; //전체 넓이
    public int totalHeight; //전체 높이

    public float intervalX = 1;
    public float intervalY = 1;



}




public class GlobalSetting
{
    static public int TotalWidth { get; set; }
    static public int TotalHeight { get; set; }

    static public float IntervalX { get; set; }
    static public float IntervalY { get; set; }


  


}
