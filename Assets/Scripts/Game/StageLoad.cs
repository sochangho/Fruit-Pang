using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLoad : MonoBehaviour
{
    private int currentStage;
    
    public int CurrentStage { get { return currentStage; } }

    public StageMapTotalDatas stageData;

    private List<Tile> tiles;

    private List<NodeInfomation> nodes;

    private int totalHeight;

    private int totalWidth;

    private JsonStageMap stageMap = new JsonStageMap();

    private string loadPath = "Assets/Resources/Data/Map";


    public void LoadStageMap()
    {
        tiles = new List<Tile>();
        nodes = new List<NodeInfomation>();

        stageData = InfoManager.Instace.GetStageDatas(PlayerPrefs.GetInt(StringKey.STAGE_LEVEL)); //stageMap.LoadGame(PlayerPrefs.GetString("GameStage"));
        currentStage = stageData.stagelevel;
        totalHeight = stageData.totalHeight;
        totalWidth = stageData.totalWidth;

        

        foreach(var  info in stageData.fruitNodeInfos)
        {
            Tile tile = new Tile(info.x,info.y,info.tileType);

            if (tile.tileType == TileType.None)
            {
                tiles.Add(tile);
                nodes.Add(null);
                continue;
            }


            NodeType nodeType;

            if(info.nodeType == NodeType.None)
            {

                var nodeTList =  NodeManager.Instace.GetNodeTypes();

               nodeType = RandomManager.RandomDraw(nodeTList);


            }
            else
            {
                nodeType = info.nodeType;
            }

            NodeInfomation node = new NodeInfomation(info.x,info.y,nodeType,info.nodeState);

            tiles.Add(tile);
            nodes.Add(node);
        }
    }


    public List<Tile> GetTiles() => tiles;

    public List<NodeInfomation> GetInfomations() => nodes;

    public JsonStageMap GetStageInfo() => stageMap;

    public int TotalW() { return totalWidth; }
    public int TotalH() { return totalHeight;  }

   
}
