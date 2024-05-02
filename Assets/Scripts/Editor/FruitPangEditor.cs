

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class FruitPangEditor : EditorWindow
{

    public StagePart stagePart;
    public StageSizePart stageSizePart;
    public TileNodeEditorPart tileNodeEditorPart;

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
    }



    public void PartInit()
    {

        if(tileNodeEditorPart == null)
        {
            tileNodeEditorPart = new TileNodeEditorPart(this);
            tileNodeEditorPart.InitAreaRect(20,220,500,500);
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

        
    }





}
#endif