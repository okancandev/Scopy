using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Okancandev.UScopes.Editor
{
    internal class UScopesEditorTreeView : TreeView
    {
        public UScopesEditorTreeView(TreeViewState state, MultiColumnHeader header)
            : base(state, header)
        {
            showAlternatingRowBackgrounds = true;
            Reload();
        }
    
        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem { id = 0, depth = -1, displayName = "Root" };
            var allItems = new List<TreeViewItem>();
            int id = 0;
            foreach (var (owner, scope) in UScopes.DefaultInstance.Scopes)
            {
                allItems.Add(new ScopeCustomTreeViewItem(id++, owner));
                foreach (var (identifier, service) in scope.Services)
                {
                    allItems.Add(new ServiceCustomTreeViewItem(id++, identifier, service));
                }
            }
    
            SetupParentsAndChildrenFromDepths(root, allItems);
            return root;
        }
    
        protected override void RowGUI(RowGUIArgs args)
        {
            for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
            {
                if (args.item is ServiceCustomTreeViewItem scopeItem)
                {
                    CellGUI(args.GetCellRect(i), scopeItem, args.GetColumn(i), ref args);
                }
    
                if (args.item is ScopeCustomTreeViewItem serviceItem)
                {
                    base.RowGUI(args);
                }
            }
        }
    
        void CellGUI(Rect cellRect, ServiceCustomTreeViewItem item, int column, ref RowGUIArgs args)
        {
            switch (column)
            {
                case 0:
                    base.RowGUI(args);
                    break;
                case 1:
                    DrawServiceField(cellRect, item);
                    break;
                case 2:
                    if (item.Tag != null)
                    {
                        if(item.Tag is UnityEngine.Object unityObject)
                            EditorGUI.ObjectField(cellRect, unityObject, unityObject.GetType(), false);
                        else
                            EditorGUI.LabelField(cellRect, $"{item.Tag} ({item.Tag.GetType().Name})");
                    }
                    break;
                case 3:
                    if(item.Id != 0)
                        EditorGUI.LabelField(cellRect, item.Id.ToString());
                    break;
                case 4:
                    if (item.Service is IUScopesEditorExtraLabel customGUIService)
                    {
                        EditorGUI.LabelField(cellRect, customGUIService.OnUScopesEditorExtraLabel());
                    }
                    break;
            }
        }
        
        private void DrawServiceField(Rect cellRect, ServiceCustomTreeViewItem item)
        {
            if(item.Service is UnityEngine.Object unityObject)
                EditorGUI.ObjectField(cellRect, unityObject, unityObject.GetType(), false);
            else
                EditorGUI.LabelField(cellRect, $"{item.Service} ({item.Type.Name})");
        }
    }
    
    sealed class ScopeCustomTreeViewItem : TreeViewItem
    {
        public readonly object Owner;
        
        public ScopeCustomTreeViewItem(int id, object owner)
        {
            this.id = id;
            Owner = owner;
            if (Owner == UScopes.DefaultInstance.GlobalScopeKey)
            {
                displayName = "Global Scope";
                icon = EditorGUIUtility.FindTexture("d_ToolHandleGlobal@2x");
            }
            else if (Owner is Scene sceneOwner)
            {
                displayName = $"{sceneOwner.name} (Scene)";
                icon = EditorGUIUtility.FindTexture("BuildSettings.Editor");
            }
            else if (Owner is GameObject gameObjectOwner)
            {
                displayName = $"{gameObjectOwner.name} (GameObject)";
                icon = EditorGUIUtility.FindTexture("Prefab Icon");
            }
            else
            {
                displayName = $"{owner} (Custom)";
                icon = EditorGUIUtility.FindTexture("Folder Icon");
            }
            depth = 0;
        }
    }

    sealed class ServiceCustomTreeViewItem : TreeViewItem
    {
        public Type Type;
        public object Tag;
        public long Id;
        public object Service;

        public ServiceCustomTreeViewItem(int id, ServiceIdentifier identifier, object service)
        {
            this.id = id;
            displayName = identifier.Type.Name;
            depth = 1;
            Type = identifier.Type;
            Tag = identifier.Tag;
            Id = identifier.Id;
            Service = service;
        }
    }
}

