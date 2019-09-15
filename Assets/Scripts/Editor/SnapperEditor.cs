using UnityEditor;
[CustomEditor(typeof(Snapper))]
public class SnapperEditor : Editor
{
    protected static bool ShowOffsetSettings = true;

    public override void OnInspectorGUI()
    {
        var offsetProperty = serializedObject.FindProperty("Offset");
        var potatoProperty = serializedObject.FindProperty("potato");

        ShowOffsetSettings = EditorGUILayout.Foldout(ShowOffsetSettings, "Offset Settings");
      
        if (ShowOffsetSettings)
        {
            EditorGUILayout.PropertyField(offsetProperty);

            EditorGUILayout.PropertyField(potatoProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}