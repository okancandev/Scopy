using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GlobalScope))]
public class GlobalScopeInspectorView : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        foreach (var (type, value) in Scopy.GlobalScope.Services)
        {
            ScopyEditorWindow.DrawServiceField(type, value);
        }

        if (GUILayout.Button("Open Editor Window"))
        {
            ScopyEditorWindow.Open();
        }
    }
}
