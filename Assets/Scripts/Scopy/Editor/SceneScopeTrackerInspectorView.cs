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
            
            if (scopeTracker.ScopyManager == null)
            {
                return;
            }
            
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in scopeTracker.gameObject.SceneScope().Services)
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
