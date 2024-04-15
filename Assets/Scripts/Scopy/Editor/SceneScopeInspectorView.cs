using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(SceneScope))]
    public class SceneScopeInspectorView : UnityEditor.Editor
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
}
