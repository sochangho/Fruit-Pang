

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class FruitPangEditor : EditorWindow
{

    public StagePart stagePart;
    public StageSizePart stageSizePart;
    public TileNodeEditorPart tileNodeEditorPart;
    public FruitNodeInfoEditorPart fruitNodeInfoEditor;

    public bool isOpen = false;

    // private JsonStageMap jsonStageMap = new JsonStageMap();

    [MenuItem("FuritPang/MapEditor")]
    private static void Init()
    {
        EditorWindow editorWindow = GetWindow(typeof(FruitPangEditor));

        editorWindow.Show();

    }


    private void OnGUI()
    {
        PartInit();
        stagePart.FruitPangOnGUI();
        stageSizePart.FruitPangOnGUI();
        tileNodeEditorPart.FruitPangOnGUI();
        fruitNodeInfoEditor.FruitPangOnGUI();

        GUILayout.BeginArea(new Rect(20, 730, 200, 50));
        SaveStage();
        GUILayout.EndArea();

    }

    public void SaveStage()
    {

        if (GUILayout.Button("Save"))
        {

            if (stagePart.CurrentStage > 0)
            {
                //int curStage = stagePart.CurrentStage;
                //int totalW = stageSizePart.TotalWidth;
                //int totalH = stageSizePart.totalHeight;

                //var list = tileNodeEditorPart.NodeExtract();

                //jsonStageMap.Save(curStage.ToString(), totalW, totalH, list);

            }
            else
            {
                Debug.LogError("Stage None");
            }
        }
    }


    public void PartInit()
    {

        if (fruitNodeInfoEditor == null)
        {
            fruitNodeInfoEditor = new FruitNodeInfoEditorPart();
            fruitNodeInfoEditor.InitAreaRect(20 + 500, 40, 400, 680);
        }

        if (tileNodeEditorPart == null)
        {
            tileNodeEditorPart = new TileNodeEditorPart(this);
            tileNodeEditorPart.InitAreaRect(20, 220, 500, 500);
        }

        if (stageSizePart == null)
        {
            stageSizePart = new StageSizePart(tileNodeEditorPart);
            stageSizePart.InitAreaRect(20, 120, 500, 100);
        }


        if (stagePart == null)
        {
            stagePart = new StagePart();
            stagePart.InitAreaRect(20, 40, 500, 80);
        }

        OpenFruitMapEditor();
    }

    private void OpenFruitMapEditor()
    {
        if (!isOpen)
        {
            Debug.Log("한번만 호출");

            var files = Resources.LoadAll("Data/Map");

            if (files.Length == 0)
            {
                stagePart.AddStage(1);
                stagePart.CurrentStage = 1;

                stageSizePart.totalWidth = 5;
                stageSizePart.totalHeight = 5;

                stageSizePart.TileNodeCreate();

                var n = tileNodeEditorPart[0, 0];

                fruitNodeInfoEditor.SetInfo(n);
            }
            else
            {
                Debug.Log(files[0].name);



            }

            isOpen = true;
        }

    }






}






#endif