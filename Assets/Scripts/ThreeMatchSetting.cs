using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ThreeMatchSetting
{
    
    public TileObject tileObject;
   
    public Transform boardTransform;

    public int totalWidth; //��ü ����
    public int totalHeight; //��ü ����

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
