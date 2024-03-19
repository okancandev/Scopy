using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

[CustomEditor(typeof(GlobalScope))]
public class ScopeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Custom Inspector GUI");
        
        GlobalScope globalScope = (GlobalScope)target;
        /*foreach (var (type, value) in Scopy._globalScope.GetEnumeration())
        {
            EditorGUI.BeginDisabledGroup(true);
            if (value is Object unityObject)
            {
                EditorGUILayout.ObjectField(type.FullName, unityObject, unityObject.GetType(), false);
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(type.FullName);
                EditorGUILayout.LabelField(value.ToString());
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndDisabledGroup();
        }*/
    }
}
