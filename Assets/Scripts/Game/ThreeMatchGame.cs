using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ThreeMatchGame : MonoBehaviour
{

    public ThreeMatchSetting threeMatchSetting;
    
    private Tiles tiles;
    private TileObjects tileObjects;
    private NodeInfos nodeInfos;
    private Nodes nodes;
    private Swaper swaper;
    private ItemHelper itemHelper;
    

    private InputManager inputManager;

    private bool bTouchDown; //입력상태 터치 플래그 
    private Node bNode;
    private Vector2 clickPos;

    private Coordinate clickCoordinate = new Coordinate();


    private HorizonDetect horizonDetect;
    private VerticalDetect verticalDetect;


    private NodeMover nodeMover;


    private bool isTouchable = true;


    public StageLoad stageLoad;


    List<Node> deleteNodeList = new List<Node>();

    List<Node> h_filter = new List<Node>();  // Wall 제거
    List<Node> v_filter = new List<Node>(); // Wall 제거

    List<Node> duplicationNodes = new List<Node>(); // 중복노드






    private void Awake()
    {
        ObjectPoolingManager.Instace.InitGame();

        stageLoad.LoadStageMap();

        threeMatchSetting.totalHeight = stageLoad.TotalH();
        threeMatchSetting.totalWidth = stageLoad.TotalW();

        Debug.Log($"높이 : {threeMatchSetting.totalHeight} ");
        Debug.Log($"넓이 : {threeMatchSetting.totalWidth} ");


        tiles = new Tiles(threeMatchSetting, stageLoad);
        
        tileObjects = new TileObjects(threeMatchSetting, tiles);
        
        nodeInfos = new NodeInfos();

        nodes = new Nodes(threeMatchSetting, tileObjects, nodeInfos);

        swaper = new Swaper(nodes, tiles, tileObjects, threeMatchSetting);

        itemHelper = new ItemHelper();
        
        GlobalSetting.TotalWidth = threeMatchSetting.totalWidth;
        GlobalSetting.TotalHeight = threeMatchSetting.totalHeight;
        GlobalSetting.IntervalX = threeMatchSetting.intervalX;
        GlobalSetting.IntervalY = threeMatchSetting.intervalY;
              
        InitGame();
    }


    private void Update()
    {
        if (isTouchable)
        {
            OnInputHandler();

            if (Input.GetKeyDown(KeyCode.W))
            {
                nodeMover.ChangeDropDirection(NodeMover.Direction.Up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                nodeMover.ChangeDropDirection(NodeMover.Direction.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                nodeMover.ChangeDropDirection(NodeMover.Direction.Right);
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                nodeMover.ChangeDropDirection(NodeMover.Direction.Down);
            }


        }
    }



    private void InitGame()
    {
        tiles.InitGame();

        tileObjects.InitGame();

        //nodeInfos.InfoTestLoad(tiles); // Test;
        nodeInfos.NodeInfosSet(stageLoad.GetInfomations());

        nodes.InitGame();

        itemHelper.InitGame();

        inputManager = new InputManager(this.gameObject.transform);

        horizonDetect = new HorizonDetect(nodes);

        verticalDetect = new VerticalDetect(nodes);

        nodeMover = new NodeMover(nodes, tileObjects );

        

        FocusBoardAtCenter();


        GameStartExplosionDetect();
    }



    
    private void FocusBoardAtCenter()
    {

        float x =  ((threeMatchSetting.totalWidth - 1) * threeMatchSetting.intervalX ) / 2 ;

        float y =  ((threeMatchSetting.totalHeight - 1) * threeMatchSetting.intervalY ) / 2 ;

        Vector2 pos = threeMatchSetting.boardTransform.position;

        Vector2 boardPos = new Vector2(pos.x - x, pos.y - y);

        threeMatchSetting.boardTransform.position = boardPos;

        Camera.main.orthographicSize = (threeMatchSetting.totalWidth / 2f);

    }


    private bool GetNode(Vector2 point, ref Coordinate clickCoordinate)
    {

        Node node;

        RaycastHit2D hit = Physics2D.Raycast(point, transform.forward, 15);

        if (hit)
        {
            node = hit.transform.GetComponent<Node>();

            if (node != null && node.MovableNode())
            {
               //Debug.Log($"Node {node.X},{node.Y}");

                clickCoordinate.x = node.X;
                clickCoordinate.y = node.Y;
                return true;
            }

        }

        clickCoordinate.x = -1;
        clickCoordinate.y = -1;

        return false;
    }





    private void OnInputHandler()
    {
        if (!bTouchDown && inputManager.IsTouchDown)
        {


            Vector2 point = inputManager.touch2WorldPostion;


            if (GetNode(point,ref clickCoordinate))
            {
                clickPos = point;
               
                bTouchDown = true;
            }

        }
        else if (bTouchDown && inputManager.IsTouchUp)
        {

            if (clickCoordinate.x >= 0 && clickCoordinate.y >= 0)
            {


                Vector2 point = inputManager.touch2WorldPostion;

                Swipe swipeDir = inputManager.EvalSwipeDir(clickPos, point);

               

                if (swaper.SwapCheckTwoWay(nodes[clickCoordinate.x, clickCoordinate.y] , swipeDir))
                {
                    swaper.Swap(swipeDir, clickCoordinate.x, clickCoordinate.y);

                    StartCoroutine(DelaySwapComplete());
                }
               

            }

            clickCoordinate.x = -1;
            clickCoordinate.y = -1;

            bTouchDown = false;
        }


    }



    private void SetStopTouchable() => isTouchable = false;
    private void SetStartTouchable() => isTouchable = true;
    

    private void StartExplosion()
    {
       var eList =  ExplosionNodeDetect();

       StartCoroutine(ExplosionRoutine(eList));
    }

    private void GameStartExplosionDetect()
    {
        SetStopTouchable();
        StartExplosion();
    }

    IEnumerator DelaySwapComplete()
    {
        SetStopTouchable();

        WaitUntil waitUntil = new WaitUntil(
            ()=> {
               return swaper.SwapingNode != null && swaper.SwapingNodeOp != null &&
             swaper.SwapingNode.GetMoveState() == NodeMoveContoller.MoveState.Complete &&
             swaper.SwapingNodeOp.GetMoveState() == NodeMoveContoller.MoveState.Complete;
                });

        yield return waitUntil;

        swaper.SwapingNode.SetMoveState(NodeMoveContoller.MoveState.NoMove);
        swaper.SwapingNodeOp.SetMoveState(NodeMoveContoller.MoveState.NoMove);

        StartExplosion();

    }



    IEnumerator ExplosionRoutine(List<Node> explosionNodes)
    {
        

        if (explosionNodes.Count > 0)
        {
            ExplosionNode(explosionNodes);

            DetectForItemCreate();

            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(EmptyMoveRoutine());

            StartCoroutine(RefillMoveRoutine());

        }
        else
        {

            SetStartTouchable();
        }


    }

    IEnumerator EmptyMoveRoutine()
    {
      
         nodeMover.CheckEmptyAndMove();

         WaitUntil waitUntil = new WaitUntil(() => nodeMover.dirMove.moveNodeCount <= 0 );

         yield return waitUntil;

            
    }


    IEnumerator RefillMoveRoutine()
    {

        nodeMover.RefillRandomNode();

        WaitUntil waitUntil = new WaitUntil(() => nodeMover.dirMove.moveNodeCount <= 0);

        yield return waitUntil;


        StartExplosion();

    }


    #region Exlosion

    public List<Node> ExplosionNodeDetect()
    {
        List<Node> h_nodeList = horizonDetect.DetectNodes();
        List<Node> v_nodeList = verticalDetect.DetectNodes();

        deleteNodeList.Clear();
        h_filter.Clear();  // Wall 제거
        v_filter.Clear(); // Wall 제거
        duplicationNodes.Clear(); // 중복노드


        foreach(var h in h_nodeList)
        {
            if (h.ExplosionableNode())
            {
                h_filter.Add(h);
            }
        }


        foreach(var v in v_nodeList)
        {
            if (v.ExplosionableNode())
            {
                v_filter.Add(v);
            }
        }

        foreach(var h in h_filter)
        {
            deleteNodeList.Add(h);
        }


        foreach(var v in v_filter)
        {
            var find = deleteNodeList.Find(n => n.X == v.X && n.Y == v.Y);

            if(find == null)
            {
                deleteNodeList.Add(v);
            }
            else
            {
                duplicationNodes.Add(v);
            }            
        }

        var totalNode = nodes.GetNodes();

        foreach(var t in totalNode)
        {
            OneSwapItemNode oneSwapItemNode = t as OneSwapItemNode;

            if(oneSwapItemNode != null && oneSwapItemNode.IsSwap())
            {
                var find = deleteNodeList.Find(n => n.X == t.X && n.Y == t.Y);

                if(find == null)
                {
                    deleteNodeList.Add(t);
                }

            }

        }


        List<Node> itemEffectNodes = new List<Node>();

        foreach (var d in deleteNodeList)
        {
            ItemNode itemNode = d as ItemNode;

            if (itemNode != null)
            {
                var list = itemNode.ItemDetect(nodes);

                if(list == null)
                {
                    continue;
                }

                for (int i = 0; i < list.Count; ++i)
                {
                    int x = list[i].x;
                    int y = list[i].y;

                    if (tileObjects[x, y] != null && nodes[x, y] != null && nodes[x, y].ExplosionableNode())
                    {
                        itemEffectNodes.Add(nodes[x, y]);
                    }
                }

            }

        }

        foreach (var n in itemEffectNodes)
        {
            var find = deleteNodeList.Find(d => d.X == n.X && d.Y == n.Y);

            if (find == null)
            {
                deleteNodeList.Add(n);
            }

        }



        List<Node> effectedNodes = new List<Node>();

        foreach(var n in deleteNodeList)
        {
            if(n.X+1 >= 0 && n.X+1 < GlobalSetting.TotalWidth)
            {
                int x = n.X+1;
                int y = n.Y;

                if(nodes[x,y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }
                
            }
            if (n.X - 1 >= 0 && n.X - 1 < GlobalSetting.TotalWidth)
            {
                int x = n.X -1;
                int y = n.Y;

                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }

            }
            if (n.Y + 1 >= 0 && n.Y + 1 < GlobalSetting.TotalHeight)
            {
                int x = n.X;
                int y = n.Y+1;
                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }

            }
            if(n.Y - 1 >= 0 && n.Y - 1 < GlobalSetting.TotalHeight)
            {
                int x = n.X;
                int y = n.Y-1;
                if (nodes[x, y] != null)
                {
                    var f = effectedNodes.Find(e => e.X == x && e.Y == y);

                    if (f == null)
                    {
                        effectedNodes.Add(nodes[x, y]);
                    }
                }
            }

        }


        foreach(var e in effectedNodes)
        {
            e.EffectEnd();
        }


        return deleteNodeList;
    }

    private void DetectForItemCreate()
    {
       var infos = itemHelper.DetectNode(h_filter, v_filter, duplicationNodes);

        foreach (var i in infos) {

            var clone = nodes.CreateNode(i.nodeType);
            clone.SetCoordinate(i.x, i.y);
            clone.transform.localPosition = tileObjects[i.x, i.y].GetPosition();
            nodes[i.x, i.y] = clone;

        }

    }

    public void ExplosionNode(List<Node> explosionNodes)
    {
        foreach(var  e in explosionNodes)
        {
            Explosion(e);
        }
    }


    public void Explosion(Node node)
    {
        nodes.ExplosionNode(node);
    }
    #endregion

    public void ChangeDropDirection(NodeMover.Direction direction)
    {
        nodeMover.ChangeDropDirection(direction);
    }


    public void LobbyScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }

}


public struct Coordinate
{
    public int x;
    public int y;
}