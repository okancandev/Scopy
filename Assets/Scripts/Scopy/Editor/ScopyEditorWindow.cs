using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Okancandev.Scopy.Editor
{
    public sealed class ScopyEditorWindow : EditorWindow
    {
        private const string MenuItemName = "Window/Analysis/Scopy Services";

        private Vector2 _scrollPosition;
        private string _filter = "";

        [MenuItem(MenuItemName, priority = 100)]
        public static void Open()
        {
            var window = GetWindow<ScopyEditorWindow>();
            window.titleContent = new GUIContent("Scopy Services");
        }

        private void OnGUI()
        {
            _filter = EditorGUILayout.TextField(_filter, EditorStyles.toolbarSearchField);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            GUILayout.Label("Global Scope");
            if (Scopy.GlobalScope != null)
            {
                DrawServices(Scopy.GlobalScope.Services, _filter);
            }

            if (Scopy.SceneScopes != null)
            {
                foreach (var (scene, scope) in Scopy.SceneScopes)
                {
                    GUILayout.Label($"{scene.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            if (Scopy.GameObjectScopes != null)
            {
                foreach (var (gameObject, scope) in Scopy.GameObjectScopes)
                {
                    GUILayout.Label($"{gameObject.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            GUILayout.EndScrollView();
        }

        private static void DrawServices(IReadOnlyDictionary<Type, object> globalScopeServices, string filter = null)
        {
            foreach (var (type, service) in globalScopeServices)
            {
                DrawServiceField(type, service, filter);
            }
        }

        //sorry for super ugly method
        public static void DrawServiceField(Type type, object service, string filter = null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                if (service is Object unityObject)
                {
                    EditorGUILayout.ObjectField(type.FullName, unityObject, unityObject.GetType(), false);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(type.FullName);
                    EditorGUILayout.LabelField(service.ToString());
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                if (service is Object unityObject)
                {
                    if (type.FullName != null &&
                        (type.FullName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                         unityObject.name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                        EditorGUILayout.ObjectField(type.FullName, unityObject, unityObject.GetType(), false);
                }
                else
                {
                    if (type.FullName != null &&
                        type.FullName.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(type.FullName);
                        EditorGUILayout.LabelField(service.ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}