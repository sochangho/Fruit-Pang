using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzle Score Datas", menuName = "Scriptable Object Asset/Puzzle Score Datas")]
public class PuzzleScoreDatas : ScriptableObject
{
    [SerializeField] private int Red;
    [SerializeField] private int Blue;
    [SerializeField] private int Green;
    [SerializeField] private int Orange;
    [SerializeField] private int Yellow;

    [SerializeField] private int BombRed;
    [SerializeField] private int BombBlue;
    [SerializeField] private int BombGreen;
    [SerializeField] private int BombOrange;
    [SerializeField] private int BombYellow;


    [SerializeField] private int Random10;
    [SerializeField] private int Down;
    [SerializeField] private int Up;
    [SerializeField] private int Left;
    [SerializeField] private int Right;


    [SerializeField] private int StateFreeze;



    public int GetNodeScore(NodeType nodeType)
    {
        int core = 0;

        switch (nodeType)
        {
            case NodeType.Blue:
                core = Blue;
                break;
            case NodeType.Red:
                core = Red;
                break;
            case NodeType.Green:
                core = Green;
                break;
            case NodeType.Yellow:
                core = Yellow;
                break;
            case NodeType.Orange:
                core = Orange;
                break;
            //-----------------------------------------------------------------------
            case NodeType.BombBlue:
                core = BombBlue;
                break;
            case NodeType.BombRed:
                core = BombRed;
                break;
            case NodeType.BombGreen:
                core = BombGreen;
                break;
            case NodeType.BombOrange:
                core = BombOrange;
                break;
            case NodeType.BombYellow:
                core = BombYellow;
                break;
            //----------------------------------------------------------------------------
           
            case NodeType.RandomFruit10:
                core = Random10;
                break;
            case NodeType.UpDir:
                core = Up;
                break;
            case NodeType.DownDir:
                core = Down;
                break;
            case NodeType.RightDir:
                core = Right;
                break;
            case NodeType.LeftDir:
                core = Left;
                break;
       
        }

        return core;
    }


    public int GetStateScore(NodeState nodeState)
    {
        int score = 0;


        if (nodeState == NodeState.Freeze)
        {
            score = StateFreeze;
        }
        return score;
    }


}
