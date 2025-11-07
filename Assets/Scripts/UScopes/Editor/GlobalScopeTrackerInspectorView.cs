using Okancandev.UScopes;
using UnityEditor;
using UnityEngine;

namespace Okancandev.UScopes.Editor
{
    [CustomEditor(typeof(AutoGlobalScopeTracker), true)]
    public class GlobalScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var scopeTracker = (AutoGlobalScopeTracker)target;

            if (scopeTracker.UScopesInstance == null)
            {
                return;
            }
            
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in UScopes.Global().Services)
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