using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Okancandev.UScopes
{
    public class UScopesIMGUIDrawer
    {
#if UNITY_EDITOR
        internal static readonly UScopesIMGUIDrawer EditorInstance = new UScopesIMGUIDrawer();
#endif

        private Vector2 _scrollPosition;
        private string _filter = "";

        private string DrawSearchBar()
        {
#if UNITY_EDITOR
            if (this == EditorInstance)
                return UnityEditor.EditorGUILayout.TextField(_filter, UnityEditor.EditorStyles.toolbarSearchField);
#endif
            return GUILayout.TextField(_filter);
        }

        public void OnGUI()
        {
            if (UScopes._defaultInstance == null)
            {
                return;
            }

            _filter = DrawSearchBar();
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            var scopes = UScopes.DefaultInstance.Scopes;

            foreach (var (owner, scope) in scopes)
            {
                if (owner == UScopes.DefaultInstance.GlobalScopeKey)
                {
                    GUILayout.Label(" Global Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            foreach (var (owner, scope) in scopes)
            {
                if (owner is Scene sceneOwner)
                {
                    GUILayout.Label($" {sceneOwner.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            foreach (var (owner, scope) in scopes)
            {
                if (owner is GameObject gameObject)
                {
                    GUILayout.Label($" {gameObject.name} Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            foreach (var (owner, scope) in scopes)
            {
                if (owner is not (UScopesInstance or Scene or GameObject))
                {
                    GUILayout.Label($" {owner} Custom Scope");
                    DrawServices(scope.Services, _filter);
                }
            }

            GUILayout.EndScrollView();
        }

        private void DrawServices(IReadOnlyDictionary<ServiceIdentifier, object> globalScopeServices,
            string filter = null)
        {
            foreach (var (identifier, service) in globalScopeServices)
            {
                DrawServiceField(identifier, service, filter);
            }
        }

        public void DrawServiceField(ServiceIdentifier identifier, object service, string filter = null)
        {
            string identifierString = identifier.ToString();
            string name = service.ToString();
            if (!string.IsNullOrWhiteSpace(filter))
            {
                foreach (var filterWord in filter.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (identifierString.Contains(filterWord, StringComparison.InvariantCultureIgnoreCase))
                        goto drawField;

                    if (name.Contains(filterWord, StringComparison.InvariantCultureIgnoreCase))
                        goto drawField;
                }

                return;
            }

            drawField:
            GUILayout.BeginHorizontal();
            DrawLine(identifierString, name, service);
            GUILayout.EndHorizontal();
        }

        private void DrawLine(string identifierString, string name, object service)
        {
#if UNITY_EDITOR
            if (this == EditorInstance && service is Object unityObject)
            {
                UnityEditor.EditorGUILayout.ObjectField(identifierString, unityObject, unityObject.GetType(), false);
            }
            else
#endif
            {
                GUILayout.Label(identifierString);
                GUILayout.Label(TruncateString(name, 36));
            }
            if (service is IScopyEditorExtraLabel extraLabelService)
                GUILayout.Label(extraLabelService.OnScopyEditorExtraLabel());
            if (service is IScopyEditorCustomGUI customGUIService)
                customGUIService.OnScopyEditorGUI();
        }

        private string TruncateString(string value, int maxLength, string truncationSuffix = "…")
        {
            return value?.Length > maxLength
                ? value.Substring(0, maxLength) + truncationSuffix
                : value;
        }
    }
}