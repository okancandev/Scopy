using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneScope))]
public class SceneScopeInspectorView : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SceneScope sceneScope = (SceneScope)target;
        
        foreach (var (type, value) in sceneScope.gameObject.GetSceneScope().Services)
        {
            ScopyEditorWindow.DrawServiceField(type, value);
        }

        if (GUILayout.Button("Open Editor Window"))
        {
            ScopyEditorWindow.Open();
        }
    }
}
