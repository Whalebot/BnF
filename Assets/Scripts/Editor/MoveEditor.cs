using UnityEditor;
[CustomEditor(typeof(Move))]
public class MoveEditor : Editor
{
    protected static bool showSettings = true;

    public override void OnInspectorGUI()
    {

    var ID = serializedObject.FindProperty("ID");
  
    var SPCost = serializedObject.FindProperty("SPCost");

    var combo = serializedObject.FindProperty("combo");

    showSettings = EditorGUILayout.Foldout(showSettings, "Show settings");

        if (showSettings)
        {
            EditorGUILayout.PropertyField(ID);
            EditorGUILayout.PropertyField(SPCost);
            EditorGUILayout.PropertyField(combo);
        }

serializedObject.ApplyModifiedProperties();
    }
}