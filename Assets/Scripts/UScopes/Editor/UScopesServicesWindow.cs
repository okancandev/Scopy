using UnityEditor;
using UnityEngine;

namespace Okancandev.Scopy.Editor
{
    public sealed class UScopesServicesWindow : EditorWindow
    {
        private const string MenuItemName = "Window/Analysis/Scopy Services";

        [MenuItem(MenuItemName, priority = 100)]
        public static void Open()
        {
            var window = GetWindow<UScopesServicesWindow>();
            window.titleContent = new GUIContent("Scopy Services");
        }

        private void OnGUI()
        {
            UScopesIMGUIDrawer.EditorInstance.OnGUI();
        }
    }
}