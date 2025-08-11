using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
//[CustomEditor(typeof(AnimatorTransitionBase))]
//public class AnimatorTransitionBaseEditor : Editor
//{
//    SerializedProperty durationProp;
//    void OnEnable()
//    {
//        durationProp = serializedObject.FindProperty("m_TransitionDuration");
//    }
//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        durationProp.floatValue = 0f;
//        EditorGUILayout.PropertyField(durationProp); 
//        serializedObject.ApplyModifiedProperties();
//    }
//}
[CustomEditor(typeof(AnimatorStateTransition))]
public class AnimatorTransitionBaseEditor : Editor
{
    string Du { get; } = "m_TransitionDuration";
    string Exite { get; } = "m_ExitTime";
    SerializedProperty duration;
    SerializedProperty ExiteTime;
    void OnEnable()
    {
        ExiteTime = serializedObject.FindProperty(Exite);
        duration = serializedObject.FindProperty(Du);
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        duration.floatValue = 0f;
        ExiteTime.floatValue = 0.999f;
        EditorGUILayout.PropertyField(ExiteTime);
        EditorGUILayout.PropertyField(duration);
        serializedObject.ApplyModifiedProperties();
    }
}