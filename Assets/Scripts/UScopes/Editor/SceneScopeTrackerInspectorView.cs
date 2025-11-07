using Okancandev.UScopes;
using UnityEditor;
using UnityEngine;

namespace Okancandev.UScopes.Editor
{
    [CustomEditor(typeof(AutoSceneScopeTracker), true)]
    public class SceneScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            AutoSceneScopeTracker scopeTracker = (AutoSceneScopeTracker)target;
            
            if (scopeTracker.UScopesInstance == null)
            {
                return;
            }
            
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in scopeTracker.gameObject.SceneScope().Services)
            {
                UScopesIMGUIDrawer.EditorInstance.DrawServiceField(type, value);
            }
    
            if (GUILayout.Button("Open Editor Window"))
            {
                UScopesServicesWindow.Open();
            }
        }
    }
}
