using UnityEditor;
[CustomEditor(typeof(Movelist))]
public class MovelistEditor : Editor
{
    Movelist movelist;
    protected static bool showGroundAttacks;
    protected static bool showJumpAttacks;
    protected static bool showDash;
    public override void OnInspectorGUI()
    {
        movelist = Selection.activeGameObject.GetComponent<Movelist>();

        serializedObject.Update();


        showDash = EditorGUILayout.Foldout(showDash, "Show dash properties");
        SerializedProperty prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                if (showDash)
                {
                    if (prop.name.Contains("dash"))
                        EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name));
                }
            }
            while (prop.NextVisible(false));
        }


        showGroundAttacks = EditorGUILayout.Foldout(showGroundAttacks, "Show Ground Attacks");
        prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                if (showGroundAttacks)
                {
                    if (prop.name.Contains("move") && !prop.name.Contains("C") && !prop.name.Contains("J"))
                        EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name));
                }
            }
            while (prop.NextVisible(false));
        }

        showJumpAttacks = EditorGUILayout.Foldout(showJumpAttacks, "Show Jump Attacks");
        prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                if (showJumpAttacks)
                {
                    if (prop.name.Contains("moveJ") || prop.name.Contains("moveObjectJ"))
                        EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name));
                }
            }
            while (prop.NextVisible(false));
        }


        var limitJ8A = serializedObject.FindProperty("limitJ8A");
        var chargeWeapon = serializedObject.FindProperty("chargeWeapon");
        var justFrameWeapon = serializedObject.FindProperty("justFrameWeapon");

        var moveC5A = serializedObject.FindProperty("moveC5A");
        var moveObjectC5A = serializedObject.FindProperty("moveObjectC5A");
        var moveCC5A = serializedObject.FindProperty("moveCC5A");
        var moveObjectCC5A = serializedObject.FindProperty("moveObjectCC5A");
        var moveC5AA = serializedObject.FindProperty("moveC5AA");
        var moveObjectC5AA = serializedObject.FindProperty("moveObjectC5AA");
        var moveCC5AA = serializedObject.FindProperty("moveCC5AA");
        var moveObjectCC5AA = serializedObject.FindProperty("moveObjectCC5AA");
        var moveC5AAA = serializedObject.FindProperty("moveC5AAA");
        var moveObjectC5AAA = serializedObject.FindProperty("moveObjectCC5AA");
        var moveCC5AAA = serializedObject.FindProperty("moveCC5AAA");
        var moveObjectCC5AAA = serializedObject.FindProperty("moveObjectCC5AAA");


        var moveC8A = serializedObject.FindProperty("moveC8A");
        var moveObjectC8A = serializedObject.FindProperty("moveObjectC8A");
        var moveC8AA = serializedObject.FindProperty("moveC8AA");
        var moveObjectC8AA = serializedObject.FindProperty("moveObjectC8AA");
        var moveC2A = serializedObject.FindProperty("moveC2A");
        var moveObjectC2A = serializedObject.FindProperty("moveObjectC2A");
        var moveC2AA = serializedObject.FindProperty("moveC2AA");
        var moveObjectC2AA = serializedObject.FindProperty("moveObjectC2AA");




        EditorGUILayout.PropertyField(limitJ8A);
        EditorGUILayout.PropertyField(chargeWeapon);
        EditorGUILayout.PropertyField(justFrameWeapon);
        if (movelist.chargeWeapon || movelist.justFrameWeapon)
        {

            EditorGUILayout.PropertyField(moveC5A);
            EditorGUILayout.PropertyField(moveObjectC5A);
            EditorGUILayout.PropertyField(moveC5AA);
            EditorGUILayout.PropertyField(moveObjectC5AA);
            EditorGUILayout.PropertyField(moveCC5A);
            EditorGUILayout.PropertyField(moveObjectCC5A);
            EditorGUILayout.PropertyField(moveCC5AA);
            EditorGUILayout.PropertyField(moveObjectCC5AA);
            EditorGUILayout.PropertyField(moveC5AAA);
            EditorGUILayout.PropertyField(moveObjectC5AAA);
            EditorGUILayout.PropertyField(moveCC5AAA);
            EditorGUILayout.PropertyField(moveObjectCC5AAA);

            EditorGUILayout.PropertyField(moveC8A);
            EditorGUILayout.PropertyField(moveC8AA);
            EditorGUILayout.PropertyField(moveC2A);
            EditorGUILayout.PropertyField(moveC2AA);

            EditorGUILayout.PropertyField(moveObjectC8A);
            EditorGUILayout.PropertyField(moveObjectC8AA);
            EditorGUILayout.PropertyField(moveObjectC2A);
            EditorGUILayout.PropertyField(moveObjectC2AA);



        }
        serializedObject.ApplyModifiedProperties();
    }
}
