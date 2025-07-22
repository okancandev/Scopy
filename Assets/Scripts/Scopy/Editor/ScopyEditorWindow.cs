using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using System.Collections.Generic;

namespace Okancandev.Scopy.Editor
{
    public class ScopyEditorWindow : EditorWindow
    {
        private const string MenuItemName = "Window/Analysis/Scopy Editor";

        [MenuItem(MenuItemName, priority = 101)]
        public static void ShowWindow()
        {
            GetWindow<ScopyEditorWindow>("Scopy Editor");
        }
    
        private ScopyEditorTreeView _scopyEditorTreeView;
        private TreeViewState _treeViewState;
        private MultiColumnHeaderState _headerState;
    
        void OnEnable()
        {
            EditorApplication.update += AutoUpdateTree;
            
            if (_treeViewState == null)
                _treeViewState = new TreeViewState();
    
            var header = CreateHeader();
            _scopyEditorTreeView = new ScopyEditorTreeView(_treeViewState, header);
        }

        private void OnDisable()
        {
            EditorApplication.update -= AutoUpdateTree;
        }

        private void AutoUpdateTree()
        {
            _scopyEditorTreeView.Reload();
            //Repaint();
        }
    
        void OnGUI()
        {
            _scopyEditorTreeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }
        
        MultiColumnHeader CreateHeader()
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Registered Type"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Service"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Tag"),
                    width = 200,
                    minWidth = 200,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Id"),
                    width = 25,
                    minWidth = 25,
                    autoResize = true
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Extra"),
                    width = 250,
                    minWidth = 50,
                    autoResize = true
                }
            };
    
            _headerState = new MultiColumnHeaderState(columns);
            return new MultiColumnHeader(_headerState);
        }
    }
}