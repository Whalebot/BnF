/*using UnityEngine;
using UnityEditor;

public class MovelistEditor : EditorWindow
{
    /*
    public string status = "Give me cancer";
    public bool showDash = true;
    public bool showSpecial = true;
    [MenuItem("CustomShit/Movelist inspector")]
    static void Init()
    {
        UnityEditor.EditorWindow window = GetWindow(typeof(MovelistEditor));
        window.position = new Rect(0, 0, 150, 60);
        window.Show();
    }

    private void OnGUI()
    {
        showDash = EditorGUI.Foldout(new Rect(3, 3, position.width - 6, 100), showDash, status);
        showSpecial = EditorGUI.Foldout(new Rect(3, 23, position.width - 6, 100), showSpecial, status);
        if (showDash)
            if (Selection.activeTransform)
                if (Selection.activeGameObject.GetComponent<Movelist>() != null)
                {

                    Movelist movelist = Selection.activeGameObject.GetComponent<Movelist>();
                    movelist.dashDuration = EditorGUI.IntField(new Rect(3, 25, position.width - 6, 20), "Dash", movelist.dashDuration);
                    movelist.dashSpeed = EditorGUI.IntField(new Rect(3, 45, position.width - 6, 20), "Dash speed", movelist.dashDuration);
                    status = Selection.activeTransform.name;
                }
        if (showSpecial)
            if (Selection.activeTransform)
                if (Selection.activeGameObject.GetComponent<Movelist>() != null)
                {

                    Movelist movelist = Selection.activeGameObject.GetComponent<Movelist>();
                    movelist.dashDuration = EditorGUI.IntField(new Rect(3, 25, position.width - 6, 20), "Dash", movelist.dashDuration);
                    movelist.dashSpeed = EditorGUI.IntField(new Rect(3, 45, position.width - 6, 20), "Dash speed", movelist.dashDuration);
                    status = Selection.activeTransform.name;
                }


        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            showDash = false;
        }
    }

    /*
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //   DrawDefaultInspector();
        Movelist movelist = (Movelist)target;
      
    }
    
}*/
