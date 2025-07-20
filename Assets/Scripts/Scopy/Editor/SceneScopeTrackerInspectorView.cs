using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(AutoSceneScopeTracker), true)]
    public class SceneScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AutoSceneScopeTracker scopeTracker = (AutoSceneScopeTracker)target;
            
            if (scopeTracker.ScopyInstance == null)
            {
                return;
            }
            
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in scopeTracker.gameObject.SceneScope().Services)
            {
                ScopyIMGUIDrawer.EditorInstance.DrawServiceField(type, value);
            }
    
            if (GUILayout.Button("Open Editor Window"))
            {
                ScopyEditorWindow.Open();
            }
        }
    }
}
