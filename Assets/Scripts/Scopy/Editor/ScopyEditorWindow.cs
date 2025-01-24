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
                if (owner is not (ScopyManager or Scene or GameObject))
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

        //sorry for super ugly method
        public static void DrawServiceField(ServiceIdentifier identifier, object service, string filter = null)
        {
            if (string.IsNullOrEmpty(filter))
            {
                if (service is Object unityObject)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(identifier.Type.FullName, unityObject, unityObject.GetType(), false);
                    if(service is IScopyEditorCustomGUI customGUIService)
                        customGUIService.OnScopyEditorGUI();
                    else
                        EditorGUILayout.LabelField(service.ToString());
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(identifier.Type.FullName);
                    if(service is IScopyEditorCustomGUI customGUIService)
                        customGUIService.OnScopyEditorGUI();
                    else
                        EditorGUILayout.LabelField(service.ToString());
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                if (service is Object unityObject)
                {
                    if (identifier.Type.FullName != null &&
                        (identifier.Type.FullName.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                         unityObject.name.Contains(filter, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.ObjectField(identifier.Type.FullName, unityObject, unityObject.GetType(), false);
                        if(service is IScopyEditorCustomGUI customGUIService)
                            customGUIService.OnScopyEditorGUI();
                        else
                            EditorGUILayout.LabelField(service.ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                }
                else
                {
                    if (identifier.Type.FullName != null &&
                        identifier.Type.FullName.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(identifier.Type.FullName);
                        if(service is IScopyEditorCustomGUI customGUIService)
                            customGUIService.OnScopyEditorGUI();
                        else
                            EditorGUILayout.LabelField(service.ToString());
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}