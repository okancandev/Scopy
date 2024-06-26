using Okancandev.Scopy;
using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    [CustomEditor(typeof(GameObjectScope))]
    public class GameObjectScopeInspectorView : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameObjectScope gameObjectScope = (GameObjectScope)target;
        
            foreach (var (type, value) in gameObjectScope.gameObject.GetScope().Services)
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