using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(GlobalScope))]
    public class GlobalScopeInspectorView : UnityEditor.Editor
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
}