using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(AutoGlobalScopeTracker), true)]
    public class GlobalScopeTrackerInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var scopeTracker = (AutoGlobalScopeTracker)target;

            if (scopeTracker.ScopyManager == null)
            {
                return;
            }
            
            GUILayout.Label("Runtime Info");
            foreach (var (type, value) in Scopy.GlobalScope().Services)
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