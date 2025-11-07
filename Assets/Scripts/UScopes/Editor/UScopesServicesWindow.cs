using UnityEditor;
using UnityEngine;

namespace Okancandev.UScopes.Editor
{
    public sealed class UScopesServicesWindow : EditorWindow
    {
        private const string MenuItemName = "Window/Analysis/UScopes Services";

        [MenuItem(MenuItemName, priority = 100)]
        public static void Open()
        {
            var window = GetWindow<UScopesServicesWindow>();
            window.titleContent = new GUIContent("UScopes Services");
        }

        private void OnGUI()
        {
            UScopesIMGUIDrawer.EditorInstance.OnGUI();
        }
    }
}