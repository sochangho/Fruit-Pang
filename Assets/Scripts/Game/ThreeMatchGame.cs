using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    End
}


public class ThreeMatchGame : MonoBehaviour
{

    public ThreeMatchSetting threeMatchSetting;
    
    private Tiles tiles;
    private TileObjects tileObjects;
    private NodeInfos nodeInfos;
    private Nodes nodes;

    private Swaper swaper;

    private PuzzleDetectPipeline puzzleDetectPipeline; 
  

    private InputManager inputManager;

    private bool bTouchDown; //입력상태 터치 플래그 
    private Node bNode;
    private Vector2 clickPos;

    private Coordinate clickCoordinate = new Coordinate();


  

    private NodeMover nodeMover;


    private bool isTouchable = true;

    public StageLoad stageLoad;

    public GameGoals gameGoals;
    public GameScore gameScore;




    #region UI variable

    //------------------------------------------------------------

    [SerializeField] private GoalsUI goalsUI;
    [SerializeField] private ScoreProgressUI scoreProgressUI;

    //---------------------------------------------------------------

    #endregion


    #region GameState

    GameState gameState;

    public void PlayGame()
    {
        gameState = GameState.Playing;
        InitGoals();
        InitScore();
        GameStartExplosionDetect();
    }

    public void EndGame()
    {
        gameState = GameState.End;


    }


    #endregion


    private void Awake()
    {
        ObjectPoolingManager.Instace.InitGame();

        stageLoad.LoadStageMap();



        threeMatchSetting.totalHeight = stageLoad.TotalH();
        threeMatchSetting.totalWidth = stageLoad.TotalW();

        tiles = new Tiles(threeMatchSetting, stageLoad);
        
        tileObjects = new TileObjects(threeMatchSetting, tiles);
        
        nodeInfos = new NodeInfos();

        nodes = new Nodes(threeMatchSetting, tileObjects, nodeInfos);

        swaper = new Swaper(nodes, tiles, tileObjects, threeMatchSetting);

        //itemHelper = new ItemHelper();
        
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


#if UNITY_EDITOR
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
#endif

        }
    }



    private void InitGame()
    {
        tiles.InitGame();

        tileObjects.InitGame();

        
        nodeInfos.NodeInfosSet(stageLoad.GetInfomations());

        nodes.InitGame();

        inputManager = new InputManager(this.gameObject.transform);

        nodeMover = new NodeMover(nodes, tileObjects );

        puzzleDetectPipeline = new PuzzleDetectPipeline(nodes,tileObjects);

        FocusBoardAtCenter();
        PlayGame();
    }

    public void InitGoals()
    {

        gameGoals = new GameGoals();

        gameGoals.goalSettingEvent += goalsUI.Setting;
        gameGoals.GoalNodeTypeEvent += goalsUI.NodeTypeUpdate;
        gameGoals.GoalStateTypeEvent += goalsUI.NodeStateUpdate;
        gameGoals.CompleteGame += EndGame;

#if UNITY_ANDROID && !UNITY_EDITOR
        gameGoals.CompleteGame += () =>
        {
            FirebaseDBManager.Instace.UserLevelIncrease(currentGameStage: stageLoad.CurrentStage);
        };
#endif
        gameGoals.SettingGoals(stageLoad.stageData.goalEditorNodeTypeElements,
            stageLoad.stageData.goalEditorStateElements);
   
    }

    public void InitScore()
    {
        gameScore = new GameScore();
        gameScore.ScoreEventNodeType += scoreProgressUI.UpdateProgress;
        gameScore.ScoreEventNodeState += scoreProgressUI.UpdateProgress;

        gameScore.Setting(stageLoad.stageData.scoreData);

    }

   



    
    private void FocusBoardAtCenter()
    {

        float x =  ((threeMatchSetting.totalWidth - 1) * threeMatchSetting.intervalX ) / 2 ;

        float y =  ((threeMatchSetting.totalHeight - 1) * threeMatchSetting.intervalY ) / 2 ;

        Vector2 pos = threeMatchSetting.boardTransform.position;

        Vector2 boardPos = new Vector2(pos.x - x, pos.y - y);

        threeMatchSetting.boardTransform.position = boardPos;

        

        if(threeMatchSetting.totalHeight > threeMatchSetting.totalWidth)
        {
            Camera.main.orthographicSize = (threeMatchSetting.totalHeight / 2f) + 1;
        }
        else
        {
            Camera.main.orthographicSize = (threeMatchSetting.totalWidth / 2f) + 1;
        }

        

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
       var eList =  ExecuteDetectPipeline();

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

            if(gameState == GameState.End)
            {
                UIManager.Instace.OpenPopup<ResultPopup>(null);
            }

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

    public List<Node> ExecuteDetectPipeline()
    {       
        var deleteNodeList = puzzleDetectPipeline.DetectExposionNodes();

        return deleteNodeList;
    }

    private void DetectForItemCreate()
    {            
        puzzleDetectPipeline.DetectForItemCreate();
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
        gameGoals.GoalNodeTypeUpdate(node.GetNodeType());
        gameScore.UpdateScore_NodeType(node.GetNodeType());
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