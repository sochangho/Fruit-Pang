using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "PuzzleMapTextureDatas", menuName = "Scriptable Object Asset/PuzzleMapTextureDatas")]
public class PuzzleMapTextureDatas : ScriptableObject
{
    [SerializeField] private Texture2D Red;
    [SerializeField] private Texture2D Blue;
    [SerializeField] private Texture2D Green;
    [SerializeField] private Texture2D Orange;
    [SerializeField] private Texture2D Yellow;

    [SerializeField] private Texture2D BombRed;
    [SerializeField] private Texture2D BombBlue;
    [SerializeField] private Texture2D BombGreen;
    [SerializeField] private Texture2D BombOrange;
    [SerializeField] private Texture2D BombYellow;


    [SerializeField] private Texture2D Random10;
    [SerializeField] private Texture2D Down;
    [SerializeField] private Texture2D Up;
    [SerializeField] private Texture2D Left;
    [SerializeField] private Texture2D Right;

    [SerializeField] private Texture2D Wall;
    [SerializeField] private Texture2D NodeNone;

    // Tile
    [SerializeField] private Texture2D TileNone;

    //State
    [SerializeField] private Texture2D StateFreeze;



    public Texture2D GetNodeTexture2D(NodeType nodeType)
    {
        Texture2D texture2D = null;

        switch (nodeType)
        {
            case NodeType.Blue:
                texture2D = Blue;                
                break;
            case NodeType.Red:
                texture2D = Red;
                break;
            case NodeType.Green:
                texture2D = Green;
                break;
            case NodeType.Yellow:
                texture2D = Yellow;
                break;
            case NodeType.Orange:
                texture2D = Orange;
                break;
            //-----------------------------------------------------------------------
            case NodeType.BombBlue:
                texture2D = BombBlue;
                break;
            case NodeType.BombRed:
                texture2D = BombRed;
                break;
            case NodeType.BombGreen:
                texture2D = BombGreen;
                break;
            case NodeType.BombOrange:
                texture2D = BombOrange;
                break;
            case NodeType.BombYellow:
                texture2D = BombYellow;
                break;
            //----------------------------------------------------------------------------
            case NodeType.Wall:
                texture2D = Wall;
                break;
            case NodeType.RandomFruit10:
                texture2D = Random10;
                break;
            case NodeType.UpDir:
                texture2D = Up;
                break;
            case NodeType.DownDir:
                texture2D = Down;
                break;
            case NodeType.RightDir:
                texture2D = Right;
                break;
            case NodeType.LeftDir:
                texture2D = Left;
                break;
            case NodeType.None:
                texture2D = NodeNone;
                break;
        }

        return texture2D;
    }

    public Sprite GetNodeTypeSprite(NodeType nodeType)
    {
        Texture2D texture = GetNodeTexture2D(nodeType);


        Sprite sprite =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }

    public Sprite GetNodeStateSprite(NodeState nodeState)
    {
        Texture2D texture = GetStateTexture2D(nodeState);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        return sprite;
    }


    public Texture2D GetTileTexture2D(TileType tileType)
    {
        Texture2D texture2D = null;

        if (tileType == TileType.None)
        {
            texture2D = TileNone;
        }
        else
        {
            texture2D = null;
        }

        return texture2D;
    }

    public Texture2D GetStateTexture2D(NodeState nodeState)
    {
        Texture2D texture2D = null;

       
        if (nodeState == NodeState.Freeze)
        {
            texture2D = StateFreeze;
        }
        return texture2D;
    }

}
