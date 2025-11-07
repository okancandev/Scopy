using Okancandev.UScopes;
using UnityEditor;
using UnityEngine;

namespace Okancandev.UScopes.Editor
{
    [CustomEditor(typeof(AutoGameObjectScopeTracker), true)]
    public class GameObjectScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var scopeTracker = (AutoGameObjectScopeTracker)target;
            
            if (scopeTracker.UScopesInstance == null)
            {
                return;
            }
        
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in scopeTracker.gameObject.Scope().Services)
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