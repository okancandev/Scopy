using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(AutoGameObjectScopeTracker), true)]
    public class GameObjectScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var scopeTracker = (AutoGameObjectScopeTracker)target;
            
            if (scopeTracker.ScopyInstance == null)
            {
                return;
            }
        
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in scopeTracker.gameObject.Scope().Services)
            {
                ScopyIMGUIDrawer.EditorInstance.DrawServiceField(type, value);
            }

            if (GUILayout.Button("Open Editor Window"))
            {
                ScopyServicesWindow.Open();
            }
        }
    }
}