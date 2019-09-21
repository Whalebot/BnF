using UnityEditor;
[CustomEditor(typeof(Move))]
public class MoveEditor : Editor
{
    protected static bool showSettings = true;
    protected static bool frameData = true;
    protected static bool movement = true;
    protected static bool moveProperties = true;
    protected static bool movementType = true;



    public override void OnInspectorGUI()
    {

        var ID = serializedObject.FindProperty("ID");
        var SpCost = serializedObject.FindProperty("SpCost");
        var combo = serializedObject.FindProperty("combo");

        var startUp = serializedObject.FindProperty("startUp");
        var active = serializedObject.FindProperty("active");
        var recovery = serializedObject.FindProperty("recovery");

        var forward1 = serializedObject.FindProperty("forward1");
        var up1 = serializedObject.FindProperty("up1");
        var duration1 = serializedObject.FindProperty("duration1");

        var forward2 = serializedObject.FindProperty("forward2");
        var up2 = serializedObject.FindProperty("up2");
        var duration2 = serializedObject.FindProperty("duration2");

        var forward3 = serializedObject.FindProperty("forward3");
        var up3 = serializedObject.FindProperty("up3");
        var duration3 = serializedObject.FindProperty("duration3");


        var startupSound = serializedObject.FindProperty("startupSound");
        var jumpCancelable = serializedObject.FindProperty("jumpCancelable");
        var specialCancelable = serializedObject.FindProperty("specialCancelable");

        var landCancel = serializedObject.FindProperty("landCancel");
        var attackCancelable = serializedObject.FindProperty("attackCancelable");
        var landCancelRecovery = serializedObject.FindProperty("landCancelRecovery");
        var landAttackFrames = serializedObject.FindProperty("landAttackFrames");
        var iFrames = serializedObject.FindProperty("iFrames");
        var invul = serializedObject.FindProperty("invul");
        var noClip = serializedObject.FindProperty("noClip");
        var isChargeAttack = serializedObject.FindProperty("isChargeAttack");
        var justFrameTiming = serializedObject.FindProperty("justFrameTiming");

        var canMove = serializedObject.FindProperty("canMove");
        var keepVel = serializedObject.FindProperty("keepVel");
        var keepVerticalVel = serializedObject.FindProperty("keepVerticalVel");
        var keepHorizontalVel = serializedObject.FindProperty("keepHorizontalVel");
        var interpolate = serializedObject.FindProperty("interpolate");
        var isHoming = serializedObject.FindProperty("isHoming");
        var spin = serializedObject.FindProperty("spin");


        showSettings = EditorGUILayout.Foldout(showSettings, "Show settings");

        if (showSettings)
        {
            EditorGUILayout.PropertyField(ID);
            EditorGUILayout.PropertyField(SpCost);
            EditorGUILayout.PropertyField(combo);
        }


        frameData = EditorGUILayout.Foldout(frameData, "Show frame data");
        if (frameData)
        {
            EditorGUILayout.PropertyField(startUp);
            EditorGUILayout.PropertyField(active);
            EditorGUILayout.PropertyField(recovery);
        }

        movement = EditorGUILayout.Foldout(movement, "Show movement data");
        if (movement)
        {
            EditorGUILayout.PropertyField(forward1);
            EditorGUILayout.PropertyField(up1);
            EditorGUILayout.PropertyField(duration1);
            EditorGUILayout.PropertyField(forward2);
            EditorGUILayout.PropertyField(up2);
            EditorGUILayout.PropertyField(duration2);
            EditorGUILayout.PropertyField(forward3);
            EditorGUILayout.PropertyField(up3);
            EditorGUILayout.PropertyField(duration3);
        }

        moveProperties = EditorGUILayout.Foldout(moveProperties, "Show move property data");
        if (moveProperties)
        {
            EditorGUILayout.PropertyField(startupSound);
            EditorGUILayout.PropertyField(jumpCancelable);
            EditorGUILayout.PropertyField(specialCancelable);
            EditorGUILayout.PropertyField(landCancel);
            EditorGUILayout.PropertyField(attackCancelable);
            EditorGUILayout.PropertyField(landCancelRecovery);
            EditorGUILayout.PropertyField(landAttackFrames);
            EditorGUILayout.PropertyField(iFrames);
            EditorGUILayout.PropertyField(invul);
            EditorGUILayout.PropertyField(noClip);
            EditorGUILayout.PropertyField(isChargeAttack);
            EditorGUILayout.PropertyField(justFrameTiming);
        }

        movementType = EditorGUILayout.Foldout(movementType, "Show movement type");
        if (movementType)
        {
            EditorGUILayout.PropertyField(canMove);
            EditorGUILayout.PropertyField(keepVerticalVel);
            EditorGUILayout.PropertyField(keepHorizontalVel);
            EditorGUILayout.PropertyField(interpolate);
            EditorGUILayout.PropertyField(isHoming);
            EditorGUILayout.PropertyField(spin);

        }

        serializedObject.ApplyModifiedProperties();
    }
}