using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (Scopy._defaultInstance == null)
            {
                return;
            }
            
            _filter = EditorGUILayout.TextField(_filter, EditorStyles.toolbarSearchField);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            var scopes = Scopy.DefaultInstance.Scopes;
            
            foreach (var (owner, scope) in scopes)
            {
                if (owner == Scopy.DefaultInstance.GlobalScopeKey)
                {
                    GUILayout.Label("Global Scope");
                    DrawServices(scope.Services, _filter);
                }
            }
            
            foreach (var (owner, scope) in scopes)
            {
                if (owner is Scene sceneOwner)
                {
                    GUILayout.Label($"{sceneOwner.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            foreach (var (owner, scope) in scopes)
            {
                if (owner is GameObject gameObject)
                {
                    GUILayout.Label($"{gameObject.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }
            
            foreach (var (owner, scope) in scopes)
            {
                if (owner is not (ScopyInstance or Scene or GameObject))
                {
                    GUILayout.Label($"{owner} Custom Scope");
                    DrawServices(scope.Services, _filter);
                }
            }
            GUILayout.EndScrollView();
        }

        private static void DrawServices(IReadOnlyDictionary<ServiceIdentifier, object> globalScopeServices, string filter = null)
        {
            foreach (var (identifier, service) in globalScopeServices)
            {
                DrawServiceField(identifier, service, filter);
            }
        }
        
        public static void DrawServiceField(ServiceIdentifier identifier, object service, string filter = null)
        {
            string identifierString = identifier.ToString();
            string name = service.ToString();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                foreach (var filterWord in filter.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if(identifierString.Contains(filterWord, StringComparison.InvariantCultureIgnoreCase))
                        goto drawField;
                    
                    if(name.Contains(filterWord, StringComparison.InvariantCultureIgnoreCase))
                        goto drawField;
                }
                return;
            }
            
            drawField:
            EditorGUILayout.BeginHorizontal();
            if (service is Object unityObject)
            {
                EditorGUILayout.ObjectField(identifierString, unityObject, unityObject.GetType(), false);
            }
            else
            {
                EditorGUILayout.LabelField(identifierString);
                EditorGUILayout.LabelField(name.Substring(0, 20));
            }
            if(service is IScopyEditorCustomGUI customGUIService)
                customGUIService.OnScopyEditorGUI();
            EditorGUILayout.EndHorizontal();
        }
    }
}