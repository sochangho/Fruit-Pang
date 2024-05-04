using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

public class TileNodeEditorPart : FruitPangEditorPart
{

   
    private List<FruitEditorNodeButton> fruitEditorNodeButtons = new List<FruitEditorNodeButton>();

    private FruitPangEditor fruitPangEditor;

    private PuzzleMapTextureDatas puzzleMapTextureDatas;


    private float sizeW = 50;
    private float sizeH = 50;

    private Vector2 scrollPosition = new Vector2();

 
    public TileNodeEditorPart(FruitPangEditor fruitPangEditor)
    {
        this.fruitPangEditor = fruitPangEditor;
        sizeH = 50;
        sizeW = 50;
    }

  
  
    public FruitEditorNodeButton this[int x, int y]
    {

        get
        {

            if (x >= fruitPangEditor.stageSizePart.TotalWidth || y >= fruitPangEditor.stageSizePart.TotalHeight || x < 0 || y < 0)
            {

                throw new System.IndexOutOfRangeException();
            }


            int index = UtilCoordinate.CoordinateToindex(x, y, fruitPangEditor.stageSizePart.TotalWidth, fruitPangEditor.stageSizePart.TotalHeight);
         
            var f= fruitEditorNodeButtons[index];

            return f;
        }

    }

  

    public override void InitAreaRect(float x, float y, float width, float Height)
    {
        base.InitAreaRect(x, y, width, Height);

        LoadTextureData();
    }

    
    protected override void PartOnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandWidth(true), GUILayout.Height(450));
      

        EditorGUILayout.BeginVertical();

        for (int y = fruitPangEditor.stageSizePart.TotalHeight - 1; y >= 0; --y)
        {

            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < fruitPangEditor.stageSizePart.TotalWidth; ++x)
            {
                this[x,y].Button();
               
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

       EditorGUILayout.EndScrollView();


       

    }

    public void NodeCreate(List<FruitNodeInfo> fruitNodeInfos)
    {
        fruitEditorNodeButtons.Clear();

        foreach(var f in fruitNodeInfos)
        {
            FruitEditorNodeButton fruitEditorNodeButton = new FruitEditorNodeButton(f, fruitPangEditor.fruitNodeInfoEditor,this);
            fruitEditorNodeButtons.Add(fruitEditorNodeButton);
        }
    }

    private void LoadTextureData()
    {
        puzzleMapTextureDatas = EditorGUIUtility.Load("PuzzleMapTextureDatas.asset") as PuzzleMapTextureDatas;

    }

    public PuzzleMapTextureDatas GetTextureData()
    {
        return puzzleMapTextureDatas;
    }

    public List<FruitNodeInfo> NodeExtract()
    {
        List<FruitNodeInfo> fruitNodeInfos = new List<FruitNodeInfo>();

       foreach(var f in fruitEditorNodeButtons)
        {
            fruitEditorNodeButtons.Add(f);
        }

        return fruitNodeInfos;
    }

}


public class FruitEditorNodeButton
{
    public FruitNodeInfo nodeInfo;
       
    private FruitNodeInfoEditorPart infoEditor;

    private TileNodeEditorPart tileNodeEditorPart;

    private GUIStyle currentGUIStyle;

    private GUIStyle buttonStyle;

    private GUIStyle statebuttonStyle;

    private GUIContent content;

    public FruitEditorNodeButton(FruitNodeInfo nodeInfo, FruitNodeInfoEditorPart infoEditor, TileNodeEditorPart tileNodeEditorPart)
    {
        this.nodeInfo = nodeInfo;
        this.infoEditor = infoEditor;
        this.tileNodeEditorPart = tileNodeEditorPart;


        buttonStyle = new GUIStyle(GUI.skin.button);         
        buttonStyle.normal.background = null; // 버튼의 기본 배경 이미지 비활성화
        buttonStyle.padding = new RectOffset(5, 5, 5, 5); // 패딩 제거

        statebuttonStyle = new GUIStyle();
        statebuttonStyle.normal.background = null;
        statebuttonStyle.padding = new RectOffset(5, 5, 5, 5);


        content = new GUIContent();

        ButtonGUISetting();
    }

    public void Button()
    {

        


        if (GUILayout.Button(content, currentGUIStyle, GUILayout.Width(50), GUILayout.Height(50)))
        {            
            infoEditor.SetInfo(this);
        }

     
    }

    public void ButtonGUISetting()
    {

        if(nodeInfo.tileType == TileType.None)
        {
            SetBackGroundImage(null);
            SetImage(GetTextureTileType(TileType.None));

            return;
        }

        if(nodeInfo.nodeState == NodeState.None)
        {

            SetBackGroundImage(null);
            SetImage(GetTextureNode(nodeInfo.nodeType));
        }
        else
        {
            SetBackGroundImage(GetTextureStateType(nodeInfo.nodeState));
            SetImage(GetTextureNode(nodeInfo.nodeType));
        }

    }


    public Texture2D GetTextureNode(NodeType nodeType) => tileNodeEditorPart.GetTextureData().GetNodeTexture2D(nodeType);

    public Texture2D GetTextureTileType(TileType tileType) => tileNodeEditorPart.GetTextureData().GetTileTexture2D(tileType);

    public Texture2D GetTextureStateType(NodeState nodeState) => tileNodeEditorPart.GetTextureData().GetStateTexture2D(nodeState);

    public void SetBackGroundImage(Texture2D texture2D)
    {
        if(texture2D == null)
        {
            currentGUIStyle = buttonStyle;

        }
        else
        {
            currentGUIStyle = statebuttonStyle;
        }

        currentGUIStyle.normal.background = texture2D;
    }
    public void SetImage(Texture2D texture2D)
    {
        content.image = texture2D;
    }


}



#endif